import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { Experience } from '../../../models/profile.models';
import { ExperienceEditModalComponent } from './experience-edit-modal/experience-edit-modal.component';

@Component({
  selector: 'app-profile-experiences',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    ExperienceEditModalComponent
  ],
  templateUrl: './profile-experiences.component.html',
  styleUrl: './profile-experiences.component.css',
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
export class ProfileExperiencesComponent {
  @Input() experiences: Experience[] = [];
  @Input() lawyerProfileId: string | null = null;
  @Output() experienceUpdated = new EventEmitter<void>();
  @Output() experienceDeleted = new EventEmitter<string>();

  selectedExperience: Experience | null = null;
  showEditModal = false;
  showAllExperiences = false;
  readonly MAX_VISIBLE_EXPERIENCES = 2;
  
  get additionalExperiences(): Experience[] {
    return this.experiences.slice(this.MAX_VISIBLE_EXPERIENCES);
  }
  
  get initialExperiences(): Experience[] {
    return this.experiences.slice(0, this.MAX_VISIBLE_EXPERIENCES);
  }

  get visibleExperiences(): Experience[] {
    if (this.showAllExperiences || this.experiences.length <= this.MAX_VISIBLE_EXPERIENCES) {
      return this.experiences;
    }
    return this.experiences.slice(0, this.MAX_VISIBLE_EXPERIENCES);
  }

  get hasMoreExperiences(): boolean {
    return this.experiences.length > this.MAX_VISIBLE_EXPERIENCES;
  }

  toggleShowAll(): void {
    this.showAllExperiences = !this.showAllExperiences;
  }

  onEditClick(experience: Experience): void {
    this.selectedExperience = experience;
    this.showEditModal = true;
  }

  onModalClose(): void {
    this.showEditModal = false;
    this.selectedExperience = null;
  }

  onExperienceSaved(): void {
    this.experienceUpdated.emit();
  }

  onExperienceDeleted(experienceId: string): void {
    // Modal'ı kapat
    this.showEditModal = false;
    this.selectedExperience = null;
    // Parent component'e bildir (profil verilerini yeniden yüklesin)
    this.experienceDeleted.emit(experienceId);
  }

  onAddNew(): void {
    this.selectedExperience = null;
    this.showEditModal = true;
  }
}

