import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { LawyerInfo } from '../../../models/profile.models';
import { LawyerInfoEditModalComponent } from './lawyer-info-edit-modal/lawyer-info-edit-modal.component';

@Component({
  selector: 'app-profile-lawyer-info',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    LawyerInfoEditModalComponent
  ],
  templateUrl: './profile-lawyer-info.component.html',
  styleUrl: './profile-lawyer-info.component.css'
})
export class ProfileLawyerInfoComponent {
  @Input() lawyerInfo!: LawyerInfo;
  @Input() lawyerProfileId: string | null = null;
  @Input() userProfileId: string | null = null;
  @Output() lawyerInfoUpdated = new EventEmitter<void>();

  showEditModal = false;

  formatDate(dateString?: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    return date.toLocaleDateString('tr-TR', { year: 'numeric', month: 'long', day: 'numeric' });
  }

  onEditClick(): void {
    this.showEditModal = true;
  }

  onLawyerInfoSaved(): void {
    this.lawyerInfoUpdated.emit();
  }
}

