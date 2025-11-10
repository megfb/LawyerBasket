import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { Skill } from '../../../models/profile.models';

@Component({
  selector: 'app-profile-skills',
  standalone: true,
  imports: [CommonModule, CardModule, TagModule],
  templateUrl: './profile-skills.component.html',
  styleUrl: './profile-skills.component.css'
})
export class ProfileSkillsComponent {
  @Input() skills: Skill[] = [];

  getSeverity(level?: string): 'success' | 'info' | 'warn' | 'secondary' | 'contrast' | 'danger' | null {
    if (!level) return 'info';
    switch (level.toLowerCase()) {
      case 'expert':
        return 'success';
      case 'advanced':
        return 'info';
      case 'intermediate':
        return 'warn';
      case 'beginner':
        return 'secondary';
      default:
        return 'info';
    }
  }
}

