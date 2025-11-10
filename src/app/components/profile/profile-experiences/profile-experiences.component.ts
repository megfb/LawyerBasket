import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { Experience } from '../../../models/profile.models';

@Component({
  selector: 'app-profile-experiences',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './profile-experiences.component.html',
  styleUrl: './profile-experiences.component.css'
})
export class ProfileExperiencesComponent {
  @Input() experiences: Experience[] = [];
}

