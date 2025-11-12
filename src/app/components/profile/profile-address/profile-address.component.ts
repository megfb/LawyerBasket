import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { Address } from '../../../models/profile.models';
import { AddressEditModalComponent } from './address-edit-modal/address-edit-modal.component';

@Component({
  selector: 'app-profile-address',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    AddressEditModalComponent
  ],
  templateUrl: './profile-address.component.html',
  styleUrl: './profile-address.component.css'
})
export class ProfileAddressComponent {
  @Input() address!: Address;
  @Input() userProfileId: string | null = null;
  @Output() addressUpdated = new EventEmitter<void>();

  showEditModal = false;

  onEditClick(): void {
    this.showEditModal = true;
  }

  onAddressSaved(): void {
    this.addressUpdated.emit();
  }
}

