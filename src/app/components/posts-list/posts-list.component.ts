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
import { ProfileService } from '../../services/profile.service';
import { PostDto, PostLikeUserDto } from '../../models/post.models';
import { PostItemComponent } from '../post-item/post-item.component';

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
    ToastModule,
    PostItemComponent
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './posts-list.component.html',
  styleUrl: './posts-list.component.css'
})
export class PostsListComponent implements OnInit {
  private postService = inject(PostService);
  private profileService = inject(ProfileService);
  private messageService = inject(MessageService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  posts: PostDto[] = [];
  isLoadingPosts = false;
  postsError: string | null = null;
  showCreatePostModal = false;
  createPostForm!: FormGroup;
  isSubmittingPost = false;
  showLikesModal = false;
  currentPostLikes: PostLikeUserDto[] = [];
  isLoadingLikes = false;
  likesError: string | null = null;

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

  onPostDeleted(postId: string): void {
    this.posts = this.posts.filter(p => p.id !== postId);
  }

  onPostLiked(post: PostDto): void {
    this.loadPosts();
  }

  onCommentAdded(post: PostDto): void {
    this.loadPosts();
  }

  onCommentDeleted(data: { postId: string; commentId: string }): void {
    this.loadPosts();
  }

  onShowLikes(post: PostDto): void {
    this.showLikesModal = true;
    this.currentPostLikes = [];
    this.isLoadingLikes = true;
    this.likesError = null;

    // Get user IDs from post.likes array
    if (!post.likes || post.likes.length === 0) {
      this.isLoadingLikes = false;
      this.currentPostLikes = [];
      return;
    }

    const userIds = post.likes.map(like => like.userId).filter((id, index, self) => self.indexOf(id) === index); // Remove duplicates

    // Get user profiles using GetUserProfilesByIds
    this.profileService.getUserProfilesByIds(userIds).subscribe({
      next: (response) => {
        this.isLoadingLikes = false;
        if (response.isSuccess && response.data) {
          // Map user profiles to PostLikeUserDto
          this.currentPostLikes = post.likes!
            .map(like => {
              const userProfile = response.data!.find(u => u.id === like.userId);
              if (userProfile) {
                return {
                  likeId: like.id,
                  userId: like.userId,
                  postId: like.postId,
                  firstName: userProfile.firstName,
                  lastName: userProfile.lastName,
                  profileImage: undefined, // Profile image will be handled in frontend using /img/profilephoto.jpg
                  createdAt: like.createdAt
                } as PostLikeUserDto;
              }
              return null;
            })
            .filter((item): item is PostLikeUserDto => item !== null);
        } else {
          this.likesError = response.errorMessage?.join(', ') || 'Beğenen kullanıcılar yüklenirken bir hata oluştu.';
        }
      },
      error: (error) => {
        this.isLoadingLikes = false;
        console.error('Beğenen kullanıcılar yüklenirken hata oluştu:', error);
        this.likesError = error.error?.errorMessage?.join(', ') || 'Beğenen kullanıcılar yüklenirken bir hata oluştu.';
      }
    });
  }

  onCloseLikesModal(): void {
    this.showLikesModal = false;
    this.currentPostLikes = [];
    this.likesError = null;
  }

  getInitials(firstName: string, lastName: string): string {
    return (firstName.charAt(0) + lastName.charAt(0)).toUpperCase();
  }

  onUserImageError(event: Event, likeUser: PostLikeUserDto): void {
    const img = event.target as HTMLImageElement;
    const initials = this.getInitials(likeUser.firstName, likeUser.lastName);
    img.src = `data:image/svg+xml;base64,${btoa(`<svg width="40" height="40" xmlns="http://www.w3.org/2000/svg"><rect width="40" height="40" fill="#20b2aa"/><text x="50%" y="50%" font-size="16" fill="white" text-anchor="middle" dy=".3em">${initials}</text></svg>`)}`;
  }

  goBack(): void {
    this.router.navigate(['/profile']);
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

