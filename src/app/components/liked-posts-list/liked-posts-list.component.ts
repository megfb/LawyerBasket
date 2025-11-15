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
import { ProfileService } from '../../services/profile.service';
import { PostService } from '../../services/post.service';
import { AuthService } from '../../services/auth.service';
import { PostDto, CommentDto } from '../../models/post.models';

@Component({
  selector: 'app-liked-posts-list',
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
  templateUrl: './liked-posts-list.component.html',
  styleUrl: './liked-posts-list.component.css'
})
export class LikedPostsListComponent implements OnInit {
  private profileService = inject(ProfileService);
  private postService = inject(PostService);
  private authService = inject(AuthService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  likedPosts: PostDto[] = [];
  isLoadingLikedPosts = false;
  likedPostsError: string | null = null;
  expandedComments: Set<string> = new Set();
  commentForms: Map<string, FormGroup> = new Map();
  isSubmittingComment: Map<string, boolean> = new Map();

  ngOnInit(): void {
    this.loadLikedPosts();
  }

  private loadLikedPosts(): void {
    this.isLoadingLikedPosts = true;
    this.likedPostsError = null;

    this.profileService.getProfileFull().subscribe({
      next: (response) => {
        this.isLoadingLikedPosts = false;
        if (response.isSuccess && response.data?.likedPosts) {
          this.likedPosts = response.data.likedPosts;
        } else {
          this.likedPostsError = response.errorMessage?.join(', ') || 'Beğendiğiniz gönderiler yüklenirken bir hata oluştu.';
        }
      },
      error: (error) => {
        this.isLoadingLikedPosts = false;
        console.error('Beğendiğiniz gönderiler yüklenirken hata oluştu:', error);
        this.likedPostsError = error.error?.errorMessage?.join(', ') || 'Beğendiğiniz gönderiler yüklenirken bir hata oluştu.';
      }
    });
  }

  getLikesCount(post: PostDto): number {
    return post.likes?.length || 0;
  }

  isLiked(post: PostDto): boolean {
    const userId = this.authService.getUserIdFromToken();
    if (!userId || !post.likes) return false;
    return post.likes.some(like => like.userId === userId);
  }

  getLikeId(post: PostDto): string | null {
    const userId = this.authService.getUserIdFromToken();
    if (!userId || !post.likes) return null;
    const like = post.likes.find(like => like.userId === userId);
    return like?.id || null;
  }

  onToggleLike(event: Event, post: PostDto): void {
    event.stopPropagation();
    const userId = this.authService.getUserIdFromToken();
    if (!userId) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Uyarı',
        detail: 'Beğenmek için giriş yapmanız gerekiyor.'
      });
      return;
    }

    const isLiked = this.isLiked(post);
    const likeId = this.getLikeId(post);

    if (isLiked && likeId) {
      // Unlike
      this.postService.removeLike(post.id, likeId).subscribe({
        next: (response) => {
          if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
            this.loadLikedPosts();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Hata',
              detail: response?.errorMessage?.join(', ') || 'Beğeni kaldırılırken bir hata oluştu.'
            });
          }
        },
        error: (error) => {
          console.error('Beğeni kaldırılırken hata oluştu:', error);
          if (error.status === 200 || error.status === 204) {
            this.loadLikedPosts();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Hata',
              detail: error.error?.errorMessage?.join(', ') || 'Beğeni kaldırılırken bir hata oluştu.'
            });
          }
        }
      });
    } else {
      // Like
      this.postService.createLike(post.id, userId).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.loadLikedPosts();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Hata',
              detail: response.errorMessage?.join(', ') || 'Beğeni eklenirken bir hata oluştu.'
            });
          }
        },
        error: (error) => {
          console.error('Beğeni ekleme hatası:', error);
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: error.error?.errorMessage?.join(', ') || error.error?.message || 'Beğeni eklenirken bir hata oluştu.'
          });
        }
      });
    }
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
          this.loadLikedPosts();
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
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Yorum başarıyla silindi.'
          });
          this.loadLikedPosts();
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
          this.loadLikedPosts();
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
}

