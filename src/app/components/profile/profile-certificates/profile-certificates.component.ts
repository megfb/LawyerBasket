import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { Certificate } from '../../../models/profile.models';

@Component({
  selector: 'app-profile-certificates',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './profile-certificates.component.html',
  styleUrl: './profile-certificates.component.css'
})
export class ProfileCertificatesComponent {
  @Input() certificates: Certificate[] = [];
}

