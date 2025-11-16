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
import { PostService } from '../../../services/post.service';
import { ProfileService } from '../../../services/profile.service';
import { PostDto, PostLikeUserDto } from '../../../models/post.models';
import { PostItemComponent } from '../../post-item/post-item.component';

@Component({
  selector: 'app-profile-tabs',
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
  templateUrl: './profile-tabs.component.html',
  styleUrl: './profile-tabs.component.css'
})
export class ProfileTabsComponent implements OnInit {
  private postService = inject(PostService);
  private profileService = inject(ProfileService);
  private messageService = inject(MessageService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  activeTab: string = 'posts';
  posts: PostDto[] = [];
  commentedPosts: PostDto[] = [];
  likedPosts: PostDto[] = [];
  isLoadingPosts = false;
  isLoadingCommentedPosts = false;
  isLoadingLikedPosts = false;
  postsError: string | null = null;
  commentedPostsError: string | null = null;
  likedPostsError: string | null = null;
  showCreatePostModal = false;
  createPostForm!: FormGroup;
  isSubmittingPost = false;
  showLikesModal = false;
  currentPostLikes: PostLikeUserDto[] = [];
  isLoadingLikes = false;
  likesError: string | null = null;

  get displayedPosts(): PostDto[] {
    return this.posts.slice(0, 2);
  }

  get hasMorePosts(): boolean {
    return this.posts.length > 2;
  }

  get displayedCommentedPosts(): PostDto[] {
    return this.commentedPosts.slice(0, 2);
  }

  get hasMoreCommentedPosts(): boolean {
    return this.commentedPosts.length > 2;
  }

  get displayedLikedPosts(): PostDto[] {
    return this.likedPosts.slice(0, 2);
  }

  get hasMoreLikedPosts(): boolean {
    return this.likedPosts.length > 2;
  }

  ngOnInit(): void {
    this.loadPosts();
    this.initializeCreatePostForm();
  }

  private initializeCreatePostForm(): void {
    this.createPostForm = this.fb.group({
      content: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(255)]]
    });
  }

  setActiveTab(tab: string): void {
    this.activeTab = tab;
    if (tab === 'posts' && this.posts.length === 0 && !this.isLoadingPosts) {
      this.loadPosts();
    } else if (tab === 'comments' && this.commentedPosts.length === 0 && !this.isLoadingCommentedPosts) {
      this.loadCommentedPosts();
    } else if (tab === 'likes' && this.likedPosts.length === 0 && !this.isLoadingLikedPosts) {
      this.loadLikedPosts();
    }
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

  private loadCommentedPosts(): void {
    this.isLoadingCommentedPosts = true;
    this.commentedPostsError = null;

    this.profileService.getProfileFull().subscribe({
      next: (response) => {
        this.isLoadingCommentedPosts = false;
        if (response.isSuccess && response.data?.commentedPosts) {
          this.commentedPosts = response.data.commentedPosts;
        } else {
          this.commentedPostsError = response.errorMessage?.join(', ') || 'Yorum yaptığınız gönderiler yüklenirken bir hata oluştu.';
        }
      },
      error: (error) => {
        this.isLoadingCommentedPosts = false;
        console.error('Yorum yaptığınız gönderiler yüklenirken hata oluştu:', error);
        this.commentedPostsError = error.error?.errorMessage?.join(', ') || 'Yorum yaptığınız gönderiler yüklenirken bir hata oluştu.';
      }
    });
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

  onPostDeleted(postId: string): void {
    this.posts = this.posts.filter(p => p.id !== postId);
    this.commentedPosts = this.commentedPosts.filter(p => p.id !== postId);
    this.likedPosts = this.likedPosts.filter(p => p.id !== postId);
  }

  onPostLiked(post: PostDto): void {
    // Postları yeniden yükle
    this.loadPosts();
    if (this.activeTab === 'comments') {
      this.loadCommentedPosts();
    }
    if (this.activeTab === 'likes') {
      this.loadLikedPosts();
    }
  }

  onCommentAdded(post: PostDto): void {
    // Postları yeniden yükle
    this.loadPosts();
    if (this.activeTab === 'comments') {
      this.loadCommentedPosts();
    }
    if (this.activeTab === 'likes') {
      this.loadLikedPosts();
    }
  }

  onCommentDeleted(data: { postId: string; commentId: string }): void {
    // Postları yeniden yükle
    this.loadPosts();
    if (this.activeTab === 'comments') {
      this.loadCommentedPosts();
    }
    if (this.activeTab === 'likes') {
      this.loadLikedPosts();
    }
  }

  onShowMorePosts(): void {
    this.router.navigate(['/posts']);
  }

  onShowMoreCommentedPosts(): void {
    this.router.navigate(['/commented-posts']);
  }

  onShowMoreLikedPosts(): void {
    this.router.navigate(['/liked-posts']);
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
}

