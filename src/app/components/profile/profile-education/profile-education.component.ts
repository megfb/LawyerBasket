import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { Education } from '../../../models/profile.models';
import { EducationEditModalComponent } from './education-edit-modal/education-edit-modal.component';

@Component({
  selector: 'app-profile-education',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    EducationEditModalComponent
  ],
  templateUrl: './profile-education.component.html',
  styleUrl: './profile-education.component.css',
  animations: [
    trigger('slideAnimation', [
      state('collapsed', style({
        height: '0px',
        opacity: 0,
        overflow: 'hidden'
      })),
      state('expanded', style({
        height: '*',
        opacity: 1,
        overflow: 'visible'
      })),
      transition('collapsed <=> expanded', [
        animate('300ms ease-in-out')
      ])
    ])
  ]
})
export class ProfileEducationComponent {
  @Input() education: Education[] = [];
  @Input() lawyerProfileId: string | null = null;
  @Output() educationUpdated = new EventEmitter<void>();
  @Output() educationDeleted = new EventEmitter<string>();

  selectedEducation: Education | null = null;
  showEditModal = false;
  showAllEducation = false;
  readonly MAX_VISIBLE_EDUCATION = 2;
  
  get additionalEducation(): Education[] {
    return this.education.slice(this.MAX_VISIBLE_EDUCATION);
  }
  
  get initialEducation(): Education[] {
    return this.education.slice(0, this.MAX_VISIBLE_EDUCATION);
  }

  get visibleEducation(): Education[] {
    if (this.showAllEducation || this.education.length <= this.MAX_VISIBLE_EDUCATION) {
      return this.education;
    }
    return this.education.slice(0, this.MAX_VISIBLE_EDUCATION);
  }

  get hasMoreEducation(): boolean {
    return this.education.length > this.MAX_VISIBLE_EDUCATION;
  }

  toggleShowAll(): void {
    this.showAllEducation = !this.showAllEducation;
  }

  onEditClick(education: Education): void {
    this.selectedEducation = education;
    this.showEditModal = true;
  }

  onEducationSaved(): void {
    this.educationUpdated.emit();
  }

  onEducationDeleted(educationId: string): void {
    // Modal'ı kapat
    this.showEditModal = false;
    this.selectedEducation = null;
    // Parent component'e bildir (profil verilerini yeniden yüklesin)
    this.educationDeleted.emit(educationId);
  }

  onAddNew(): void {
    this.selectedEducation = null;
    this.showEditModal = true;
  }
}

