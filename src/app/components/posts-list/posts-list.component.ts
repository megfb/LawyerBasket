import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { TextareaModule } from 'primeng/textarea';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PostService } from '../../services/post.service';
import { PostDto, CommentDto } from '../../models/post.models';

@Component({
  selector: 'app-posts-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    ButtonModule,
    DialogModule,
    TextareaModule,
    ProgressSpinnerModule,
    ConfirmDialogModule,
    ToastModule
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './posts-list.component.html',
  styleUrl: './posts-list.component.css'
})
export class PostsListComponent implements OnInit {
  private postService = inject(PostService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  posts: PostDto[] = [];
  isLoadingPosts = false;
  postsError: string | null = null;
  expandedComments: Set<string> = new Set();
  showCreatePostModal = false;
  createPostForm!: FormGroup;
  isSubmittingPost = false;
  commentForms: Map<string, FormGroup> = new Map();
  isSubmittingComment: Map<string, boolean> = new Map();

  ngOnInit(): void {
    this.loadPosts();
    this.initializeCreatePostForm();
  }

  private initializeCreatePostForm(): void {
    this.createPostForm = this.fb.group({
      content: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(255)]]
    });
  }

  private loadPosts(): void {
    this.isLoadingPosts = true;
    this.postsError = null;

    this.postService.getPosts().subscribe({
      next: (response) => {
        this.isLoadingPosts = false;
        if (response.isSuccess && response.data) {
          this.posts = response.data;
        } else {
          this.postsError = response.errorMessage?.join(', ') || 'Gönderiler yüklenirken bir hata oluştu.';
        }
      },
      error: (error) => {
        this.isLoadingPosts = false;
        console.error('Gönderiler yüklenirken hata oluştu:', error);
        this.postsError = error.error?.errorMessage?.join(', ') || 'Gönderiler yüklenirken bir hata oluştu.';
      }
    });
  }

  getLikesCount(post: PostDto): number {
    return post.likes?.length || 0;
  }

  getCommentsCount(post: PostDto): number {
    return post.comments?.length || 0;
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const now = new Date();
    const diffInSeconds = Math.floor((now.getTime() - date.getTime()) / 1000);

    if (diffInSeconds < 60) {
      return 'Az önce';
    } else if (diffInSeconds < 3600) {
      const minutes = Math.floor(diffInSeconds / 60);
      return `${minutes} dakika önce`;
    } else if (diffInSeconds < 86400) {
      const hours = Math.floor(diffInSeconds / 3600);
      return `${hours} saat önce`;
    } else if (diffInSeconds < 604800) {
      const days = Math.floor(diffInSeconds / 86400);
      return `${days} gün önce`;
    } else {
      return date.toLocaleDateString('tr-TR', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
      });
    }
  }

  onDeletePost(event: Event, post: PostDto): void {
    event.stopPropagation();
    
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bu gönderiyi silmek istediğinizden emin misiniz?',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Evet, Sil',
      rejectLabel: 'İptal',
      accept: () => {
        this.deletePost(post.id);
      }
    });
  }

  private deletePost(postId: string): void {
    this.postService.removePost(postId).subscribe({
      next: (response) => {
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Gönderi başarıyla silindi.'
          });
          // Listeden kaldır
          this.posts = this.posts.filter(p => p.id !== postId);
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response?.errorMessage?.join(', ') || 'Gönderi silinirken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        console.error('Gönderi silinirken hata oluştu:', error);
        if (error.status === 200 || error.status === 204) {
          // Başarılı silme (200 OK veya 204 No Content)
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Gönderi başarıyla silindi.'
          });
          this.posts = this.posts.filter(p => p.id !== postId);
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: error.error?.errorMessage?.join(', ') || 'Gönderi silinirken bir hata oluştu.'
          });
        }
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/profile']);
  }

  onToggleComments(event: Event, post: PostDto): void {
    event.stopPropagation();
    if (this.expandedComments.has(post.id)) {
      this.expandedComments.delete(post.id);
    } else {
      this.expandedComments.add(post.id);
      this.initializeCommentForm(post.id);
    }
  }

  getCommentForm(postId: string): FormGroup {
    if (!this.commentForms.has(postId)) {
      this.initializeCommentForm(postId);
    }
    return this.commentForms.get(postId)!;
  }

  private initializeCommentForm(postId: string): void {
    if (!this.commentForms.has(postId)) {
      this.commentForms.set(postId, this.fb.group({
        text: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(255)]]
      }));
    }
  }

  isCommentsExpanded(post: PostDto): boolean {
    return this.expandedComments.has(post.id);
  }

  getComments(post: PostDto): CommentDto[] {
    return post.comments || [];
  }

  onDeleteComment(event: Event, post: PostDto, comment: CommentDto): void {
    event.stopPropagation();
    
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bu yorumu silmek istediğinizden emin misiniz?',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Evet, Sil',
      rejectLabel: 'İptal',
      accept: () => {
        this.deleteComment(post.id, comment.id);
      }
    });
  }

  private deleteComment(postId: string, commentId: string): void {
    this.postService.removeComment(postId, commentId).subscribe({
      next: (response) => {
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Yorum başarıyla silindi.'
          });
          // Postları yeniden yükle
          this.loadPosts();
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response?.errorMessage?.join(', ') || 'Yorum silinirken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        console.error('Yorum silinirken hata oluştu:', error);
        if (error.status === 200 || error.status === 204) {
          // Başarılı silme (200 OK veya 204 No Content)
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Yorum başarıyla silindi.'
          });
          this.loadPosts();
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: error.error?.errorMessage?.join(', ') || 'Yorum silinirken bir hata oluştu.'
          });
        }
      }
    });
  }

  onSubmitComment(post: PostDto): void {
    const form = this.getCommentForm(post.id);
    if (form.invalid) {
      form.markAllAsTouched();
      return;
    }

    this.isSubmittingComment.set(post.id, true);
    const text = form.get('text')?.value?.trim();

    if (!text || text.length === 0) {
      this.messageService.add({
        severity: 'error',
        summary: 'Hata',
        detail: 'Yorum içeriği boş olamaz.'
      });
      this.isSubmittingComment.set(post.id, false);
      return;
    }

    this.postService.createComment(post.id, text).subscribe({
      next: (response) => {
        this.isSubmittingComment.set(post.id, false);
        if (response.isSuccess && response.data) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Yorum başarıyla eklendi.'
          });
          form.reset();
          this.loadPosts(); // Postları yeniden yükle
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response.errorMessage?.join(', ') || 'Yorum eklenirken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        this.isSubmittingComment.set(post.id, false);
        console.error('Yorum ekleme hatası:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: error.error?.errorMessage?.join(', ') || error.error?.message || 'Yorum eklenirken bir hata oluştu.'
        });
      }
    });
  }

  isSubmittingCommentForPost(postId: string): boolean {
    return this.isSubmittingComment.get(postId) || false;
  }

  onShowCreatePostModal(): void {
    this.showCreatePostModal = true;
    this.createPostForm.reset();
  }

  onCloseCreatePostModal(): void {
    this.showCreatePostModal = false;
    this.createPostForm.reset();
  }

  onCreatePost(): void {
    if (this.createPostForm.invalid) {
      this.createPostForm.markAllAsTouched();
      return;
    }

    this.isSubmittingPost = true;
    const content = this.createPostForm.get('content')?.value?.trim();

    if (!content || content.length === 0) {
      this.messageService.add({
        severity: 'error',
        summary: 'Hata',
        detail: 'Gönderi içeriği boş olamaz.'
      });
      this.isSubmittingPost = false;
      return;
    }

    console.log('Gönderilecek içerik:', content);
    this.postService.createPost(content).subscribe({
      next: (response) => {
        this.isSubmittingPost = false;
        if (response.isSuccess && response.data) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Gönderi başarıyla oluşturuldu.'
          });
          this.onCloseCreatePostModal();
          this.loadPosts(); // Postları yeniden yükle
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response.errorMessage?.join(', ') || 'Gönderi oluşturulurken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        this.isSubmittingPost = false;
        console.error('Gönderi oluşturma hatası:', error);
        console.error('Hata detayları:', error.error);
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: error.error?.errorMessage?.join(', ') || error.error?.message || 'Gönderi oluşturulurken bir hata oluştu.'
        });
      }
    });
  }
}

