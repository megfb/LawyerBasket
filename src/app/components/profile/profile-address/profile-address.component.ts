import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { Address } from '../../../models/profile.models';

@Component({
  selector: 'app-profile-address',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './profile-address.component.html',
  styleUrl: './profile-address.component.css'
})
export class ProfileAddressComponent {
  @Input() address!: Address;
}

