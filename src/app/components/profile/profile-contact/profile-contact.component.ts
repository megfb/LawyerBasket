import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ContactInfo } from '../../../models/profile.models';
import { ContactEditModalComponent } from './contact-edit-modal/contact-edit-modal.component';

@Component({
  selector: 'app-profile-contact',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ContactEditModalComponent
  ],
  templateUrl: './profile-contact.component.html',
  styleUrl: './profile-contact.component.css'
})
export class ProfileContactComponent {
  @Input() contactInfo!: ContactInfo;
  @Input() lawyerProfileId: string | null = null;
  @Output() contactUpdated = new EventEmitter<void>();

  showEditModal = false;

  onEditClick(): void {
    this.showEditModal = true;
  }

  onContactSaved(): void {
    this.contactUpdated.emit();
  }
}

