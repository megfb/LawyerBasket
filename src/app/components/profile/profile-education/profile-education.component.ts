import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { Education } from '../../../models/profile.models';

@Component({
  selector: 'app-profile-education',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './profile-education.component.html',
  styleUrl: './profile-education.component.css'
})
export class ProfileEducationComponent {
  @Input() education: Education[] = [];
}

