import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { TextareaModule } from 'primeng/textarea';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { PostService } from '../../services/post.service';
import { ProfileService } from '../../services/profile.service';
import { AuthService } from '../../services/auth.service';
import { PostDto, CommentDto } from '../../models/post.models';
import { UserProfileDto } from '../../models/profile-api.models';

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    TextareaModule,
    ConfirmDialogModule,
    ToastModule
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.css'
})
export class PostItemComponent implements OnInit, OnChanges {
  @Input() post!: PostDto;
  @Input() showDeleteButton: boolean = true;
  @Input() showLikeModal: boolean = false;

  @Output() postDeleted = new EventEmitter<string>();
  @Output() postLiked = new EventEmitter<PostDto>();
  @Output() commentAdded = new EventEmitter<PostDto>();
  @Output() commentDeleted = new EventEmitter<{ postId: string; commentId: string }>();
  @Output() showLikes = new EventEmitter<PostDto>();

  private postService = inject(PostService);
  private profileService = inject(ProfileService);
  private authService = inject(AuthService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);
  private fb = inject(FormBuilder);

  expandedComments = false;
  commentForm!: FormGroup;
  isSubmittingComment = false;
  postAuthor: UserProfileDto | null = null;
  isLoadingAuthor = false;
  commentUsers = new Map<string, UserProfileDto>(); // commentId -> UserProfileDto
  isLoadingCommentUsers = false;
  private static userProfileCache = new Map<string, UserProfileDto>();

  ngOnInit(): void {
    this.initializeCommentForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['post']) {
      if (changes['post'].firstChange) {
        // İlk yüklemede
        if (this.post?.userId) {
          this.loadPostAuthor();
        }
      } else {
        // Post güncellendiğinde
        const wasCommentsExpanded = this.expandedComments;
        // expandedComments'i false yapma, kullanıcı yorumları açmışsa açık kalsın
        this.initializeCommentForm();
        this.loadPostAuthor();
        
        // Eğer yorumlar açıksa, yeni yorumlar için kullanıcı bilgilerini yükle
        if (wasCommentsExpanded) {
          this.loadCommentUsers();
        } else {
          // Yorumlar kapalıysa, kullanıcı bilgilerini temizle
          this.commentUsers.clear();
        }
      }
    }
  }

  private loadPostAuthor(): void {
    if (!this.post?.userId) {
      return;
    }

    // Check cache first
    const cached = PostItemComponent.userProfileCache.get(this.post.userId);
    if (cached) {
      this.postAuthor = cached;
      return;
    }

    // Load from API
    this.isLoadingAuthor = true;
    this.profileService.getUserProfilesByIds([this.post.userId]).subscribe({
      next: (response) => {
        this.isLoadingAuthor = false;
        if (response.isSuccess && response.data && response.data.length > 0) {
          this.postAuthor = response.data[0];
          // Cache the result
          PostItemComponent.userProfileCache.set(this.post.userId, this.postAuthor);
        }
      },
      error: (error) => {
        this.isLoadingAuthor = false;
        console.error('Kullanıcı bilgileri yüklenirken hata oluştu:', error);
      }
    });
  }

  getInitials(firstName: string, lastName: string): string {
    return (firstName.charAt(0) + lastName.charAt(0)).toUpperCase();
  }

  onUserImageError(event: Event): void {
    if (!this.postAuthor) return;
    const img = event.target as HTMLImageElement;
    const initials = this.getInitials(this.postAuthor.firstName, this.postAuthor.lastName);
    img.src = `data:image/svg+xml;base64,${btoa(`<svg width="40" height="40" xmlns="http://www.w3.org/2000/svg"><rect width="40" height="40" fill="#20b2aa"/><text x="50%" y="50%" font-size="16" fill="white" text-anchor="middle" dy=".3em">${initials}</text></svg>`)}`;
  }

  private initializeCommentForm(): void {
    this.commentForm = this.fb.group({
      text: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(255)]]
    });
  }

  getLikesCount(): number {
    return this.post.likes?.length || 0;
  }

  isLiked(): boolean {
    const userId = this.authService.getUserIdFromToken();
    if (!userId || !this.post.likes) return false;
    return this.post.likes.some(like => like.userId === userId);
  }

  getLikeId(): string | null {
    const userId = this.authService.getUserIdFromToken();
    if (!userId || !this.post.likes) return null;
    const like = this.post.likes.find(like => like.userId === userId);
    return like?.id || null;
  }

  onToggleLike(event: Event): void {
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

    const isLiked = this.isLiked();
    const likeId = this.getLikeId();

    if (isLiked && likeId) {
      // Unlike
      this.postService.removeLike(this.post.id, likeId).subscribe({
        next: (response) => {
          if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
            this.postLiked.emit(this.post);
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
            this.postLiked.emit(this.post);
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
      this.postService.createLike(this.post.id, userId).subscribe({
        next: (response) => {
          if (response.isSuccess && response.data) {
            this.postLiked.emit(this.post);
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

  onShowLikes(event: Event): void {
    event.stopPropagation();
    if (this.showLikeModal) {
      this.showLikes.emit(this.post);
    }
  }

  getCommentsCount(): number {
    return this.post.comments?.length || 0;
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

  onDeletePost(event: Event): void {
    event.stopPropagation();
    
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bu gönderiyi silmek istediğinizden emin misiniz?',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Evet, Sil',
      rejectLabel: 'İptal',
      accept: () => {
        this.deletePost();
      }
    });
  }

  private deletePost(): void {
    this.postService.removePost(this.post.id).subscribe({
      next: (response) => {
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Gönderi başarıyla silindi.'
          });
          this.postDeleted.emit(this.post.id);
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
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Gönderi başarıyla silindi.'
          });
          this.postDeleted.emit(this.post.id);
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

  onToggleComments(event: Event): void {
    event.stopPropagation();
    this.expandedComments = !this.expandedComments;
    if (this.expandedComments) {
      this.initializeCommentForm();
      this.loadCommentUsers();
    }
  }

  private loadCommentUsers(): void {
    const comments = this.getComments();
    if (!comments || comments.length === 0) {
      return;
    }

    // Get unique user IDs from comments
    const userIds = [...new Set(comments.map(c => c.userId))];
    
    // Filter out already cached users
    const uncachedUserIds = userIds.filter(userId => {
      const cached = PostItemComponent.userProfileCache.get(userId);
      if (cached) {
        // Add to commentUsers map for quick lookup
        comments.filter(c => c.userId === userId).forEach(c => {
          this.commentUsers.set(c.id, cached);
        });
        return false;
      }
      return true;
    });

    // If all users are cached, return
    if (uncachedUserIds.length === 0) {
      return;
    }

    // Load uncached users
    this.isLoadingCommentUsers = true;
    this.profileService.getUserProfilesByIds(uncachedUserIds).subscribe({
      next: (response) => {
        this.isLoadingCommentUsers = false;
        if (response.isSuccess && response.data) {
          // Cache and map users
          response.data.forEach(user => {
            PostItemComponent.userProfileCache.set(user.id, user);
            // Map to comments
            comments.filter(c => c.userId === user.id).forEach(c => {
              this.commentUsers.set(c.id, user);
            });
          });
        }
      },
      error: (error) => {
        this.isLoadingCommentUsers = false;
        console.error('Yorum kullanıcı bilgileri yüklenirken hata oluştu:', error);
      }
    });
  }

  getCommentUser(comment: CommentDto): UserProfileDto | null {
    return this.commentUsers.get(comment.id) || null;
  }

  getCommentInitials(comment: CommentDto): string {
    const user = this.getCommentUser(comment);
    if (!user) return '??';
    return this.getInitials(user.firstName, user.lastName);
  }

  onCommentImageError(event: Event, comment: CommentDto): void {
    const user = this.getCommentUser(comment);
    if (!user) return;
    const img = event.target as HTMLImageElement;
    const initials = this.getInitials(user.firstName, user.lastName);
    img.src = `data:image/svg+xml;base64,${btoa(`<svg width="32" height="32" xmlns="http://www.w3.org/2000/svg"><rect width="32" height="32" fill="#20b2aa"/><text x="50%" y="50%" font-size="12" fill="white" text-anchor="middle" dy=".3em">${initials}</text></svg>`)}`;
  }

  getComments(): CommentDto[] {
    return this.post.comments || [];
  }

  onDeleteComment(event: Event, comment: CommentDto): void {
    event.stopPropagation();
    
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Bu yorumu silmek istediğinizden emin misiniz?',
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Evet, Sil',
      rejectLabel: 'İptal',
      accept: () => {
        this.deleteComment(comment.id);
      }
    });
  }

  private deleteComment(commentId: string): void {
    this.postService.removeComment(this.post.id, commentId).subscribe({
      next: (response) => {
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Yorum başarıyla silindi.'
          });
          this.commentDeleted.emit({ postId: this.post.id, commentId });
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
          this.commentDeleted.emit({ postId: this.post.id, commentId });
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

  onSubmitComment(): void {
    if (this.commentForm.invalid) {
      this.commentForm.markAllAsTouched();
      return;
    }

    this.isSubmittingComment = true;
    const text = this.commentForm.get('text')?.value?.trim();

    if (!text || text.length === 0) {
      this.messageService.add({
        severity: 'error',
        summary: 'Hata',
        detail: 'Yorum içeriği boş olamaz.'
      });
      this.isSubmittingComment = false;
      return;
    }

    this.postService.createComment(this.post.id, text).subscribe({
      next: (response) => {
        this.isSubmittingComment = false;
        if (response.isSuccess && response.data) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Yorum başarıyla eklendi.'
          });
          this.commentForm.reset();
          this.commentAdded.emit(this.post);
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response.errorMessage?.join(', ') || 'Yorum eklenirken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        this.isSubmittingComment = false;
        console.error('Yorum ekleme hatası:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: error.error?.errorMessage?.join(', ') || error.error?.message || 'Yorum eklenirken bir hata oluştu.'
        });
      }
    });
  }
}

