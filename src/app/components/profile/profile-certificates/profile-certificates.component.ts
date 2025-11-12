import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { Certificate } from '../../../models/profile.models';
import { CertificateEditModalComponent } from './certificate-edit-modal/certificate-edit-modal.component';

@Component({
  selector: 'app-profile-certificates',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    CertificateEditModalComponent
  ],
  templateUrl: './profile-certificates.component.html',
  styleUrl: './profile-certificates.component.css',
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
export class ProfileCertificatesComponent {
  @Input() certificates: Certificate[] = [];
  @Input() lawyerProfileId: string | null = null;
  @Output() certificateUpdated = new EventEmitter<void>();
  @Output() certificateDeleted = new EventEmitter<string>();

  selectedCertificate: Certificate | null = null;
  showEditModal = false;
  showAllCertificates = false;
  readonly MAX_VISIBLE_CERTIFICATES = 2;
  
  get additionalCertificates(): Certificate[] {
    return this.certificates.slice(this.MAX_VISIBLE_CERTIFICATES);
  }
  
  get initialCertificates(): Certificate[] {
    return this.certificates.slice(0, this.MAX_VISIBLE_CERTIFICATES);
  }

  get visibleCertificates(): Certificate[] {
    if (this.showAllCertificates || this.certificates.length <= this.MAX_VISIBLE_CERTIFICATES) {
      return this.certificates;
    }
    return this.certificates.slice(0, this.MAX_VISIBLE_CERTIFICATES);
  }

  get hasMoreCertificates(): boolean {
    return this.certificates.length > this.MAX_VISIBLE_CERTIFICATES;
  }

  toggleShowAll(): void {
    this.showAllCertificates = !this.showAllCertificates;
  }

  onEditClick(certificate: Certificate): void {
    this.selectedCertificate = certificate;
    this.showEditModal = true;
  }

  onCertificateSaved(): void {
    this.certificateUpdated.emit();
  }

  onCertificateDeleted(certificateId: string): void {
    this.showEditModal = false;
    this.selectedCertificate = null;
    this.certificateDeleted.emit(certificateId);
  }

  onAddNew(): void {
    this.selectedCertificate = null;
    this.showEditModal = true;
  }
}

