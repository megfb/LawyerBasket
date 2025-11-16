import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ProfileSummary } from '../../../models/profile.models';
import { SummaryEditModalComponent } from './summary-edit-modal/summary-edit-modal.component';

@Component({
  selector: 'app-profile-summary',
  standalone: true,
  imports: [CommonModule, CardModule, SummaryEditModalComponent],
  templateUrl: './profile-summary.component.html',
  styleUrl: './profile-summary.component.css'
})
export class ProfileSummaryComponent {
  @Input() summary!: ProfileSummary;
  @Input() lawyerProfileId: string | null = null;
  @Input() userProfileId: string | null = null;
  @Input() friendsCount: number = 0;
  @Output() summaryUpdated = new EventEmitter<void>();
  @Output() showFriends = new EventEmitter<void>();

  showEditModal = false;

  onImageError(event: Event): void {
    const img = event.target as HTMLImageElement;
    // Fallback: Baş harfleri gösteren SVG
    const initials = this.summary.firstName.charAt(0) + this.summary.lastName.charAt(0);
    img.src = `data:image/svg+xml;base64,${btoa(`<svg width="150" height="150" xmlns="http://www.w3.org/2000/svg"><rect width="150" height="150" fill="#20b2aa"/><text x="50%" y="50%" font-size="48" fill="white" text-anchor="middle" dy=".3em">${initials}</text></svg>`)}`;
  }

  onEditClick(): void {
    this.showEditModal = true;
  }

  onSummarySaved(): void {
    this.summaryUpdated.emit();
  }

  onFriendsClick(): void {
    if (this.friendsCount > 0) {
      this.showFriends.emit();
    }
  }
}

