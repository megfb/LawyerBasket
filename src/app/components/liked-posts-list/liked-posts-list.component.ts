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
import { PostDto, PostLikeUserDto } from '../../models/post.models';
import { PostItemComponent } from '../post-item/post-item.component';

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
    ToastModule,
    PostItemComponent
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './liked-posts-list.component.html',
  styleUrl: './liked-posts-list.component.css'
})
export class LikedPostsListComponent implements OnInit {
  private profileService = inject(ProfileService);
  private router = inject(Router);

  likedPosts: PostDto[] = [];
  isLoadingLikedPosts = false;
  likedPostsError: string | null = null;
  showLikesModal = false;
  currentPostLikes: PostLikeUserDto[] = [];
  isLoadingLikes = false;
  likesError: string | null = null;

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

  goBack(): void {
    this.router.navigate(['/profile']);
  }

  onPostLiked(post: PostDto): void {
    this.loadLikedPosts();
  }

  onCommentAdded(post: PostDto): void {
    this.loadLikedPosts();
  }

  onCommentDeleted(data: { postId: string; commentId: string }): void {
    this.loadLikedPosts();
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

