import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ContactInfo } from '../../../models/profile.models';

@Component({
  selector: 'app-profile-contact',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './profile-contact.component.html',
  styleUrl: './profile-contact.component.css'
})
export class ProfileContactComponent {
  @Input() contactInfo!: ContactInfo;
}

