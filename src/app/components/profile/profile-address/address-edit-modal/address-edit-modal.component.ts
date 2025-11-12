import { Component, EventEmitter, Input, OnInit, OnChanges, OnDestroy, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { SelectModule } from 'primeng/select';
import { Address } from '../../../../models/profile.models';
import { AddressService } from '../../../../services/address.service';

interface CityOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-address-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    InputTextModule,
    ButtonModule,
    MessageModule,
    SelectModule
  ],
  templateUrl: './address-edit-modal.component.html',
  styleUrl: './address-edit-modal.component.css'
})
export class AddressEditModalComponent implements OnInit, OnChanges, OnDestroy {
  private fb = inject(FormBuilder);
  private addressService = inject(AddressService);
  private mutationObserver?: MutationObserver;

  @Input() visible: boolean = false;
  @Input() address: Address | null = null;
  @Input() userProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();

  addressForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;

  // Şehir listesi - backend'den gelecek veya hardcode edilebilir
  cities: CityOption[] = [
    { id: 'b1e2c3d4-0001-4f5a-8c9b-1a2b3c4d5e6f', name: 'İstanbul' },
    { id: 'b1e2c3d4-0002-4f5a-8c9b-1a2b3c4d5e6f', name: 'Ankara' },
    { id: 'b1e2c3d4-0003-4f5a-8c9b-1a2b3c4d5e6f', name: 'İzmir' },
    { id: 'b1e2c3d4-0004-4f5a-8c9b-1a2b3c4d5e6f', name: 'Bursa' },
    { id: 'b1e2c3d4-0005-4f5a-8c9b-1a2b3c4d5e6f', name: 'Antalya' }
  ];

  get isEditMode(): boolean {
    return !!this.address?.id;
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnChanges(): void {
    if (this.visible) {
      this.initializeForm();
      // Select açıkken scroll'u engelle
      setTimeout(() => {
        this.setupScrollPrevention();
      }, 200);
    } else {
      this.removeScrollPrevention();
    }
  }

  ngOnDestroy(): void {
    this.removeScrollPrevention();
  }

  private setupScrollPrevention(): void {
    // Select açıkken modal içindeki scroll'u engelle
    const checkSelect = () => {
      const selectPanel = document.querySelector('.p-select-panel') || 
                         document.querySelector('.p-overlay-select-panel');
      const dialogMask = document.querySelector('.p-dialog-mask');
      
      if (selectPanel && dialogMask) {
        dialogMask.classList.add('select-open');
      } else if (dialogMask) {
        dialogMask.classList.remove('select-open');
      }
    };

    // MutationObserver ile Select panel'inin açılıp kapanmasını izle
    this.mutationObserver = new MutationObserver(() => {
      checkSelect();
    });

    this.mutationObserver.observe(document.body, {
      childList: true,
      subtree: true
    });

    // İlk kontrol
    setTimeout(checkSelect, 200);
  }

  private removeScrollPrevention(): void {
    if (this.mutationObserver) {
      this.mutationObserver.disconnect();
      this.mutationObserver = undefined;
    }
    // CSS class'ı temizle
    const dialogMask = document.querySelector('.p-dialog-mask');
    if (dialogMask) {
      dialogMask.classList.remove('select-open');
    }
  }

  private initializeForm(): void {
    if (this.address) {
      // Edit mode
      const selectedCity = this.cities.find(c => c.id === this.address?.cityId);
      
      this.addressForm = this.fb.group({
        addressLine: [this.address.addressLine || this.address.fullAddress || '', [Validators.required, Validators.minLength(5)]],
        cityId: [selectedCity || null, [Validators.required]]
      });
    } else {
      // Create mode
      this.addressForm = this.fb.group({
        addressLine: ['', [Validators.required, Validators.minLength(5)]],
        cityId: [null, [Validators.required]]
      });
    }
  }

  onClose(): void {
    this.visible = false;
    this.visibleChange.emit(false);
    this.errorMessage = null;
    this.addressForm.reset();
  }

  onSave(): void {
    if (this.addressForm.invalid) {
      this.markFormGroupTouched(this.addressForm);
      return;
    }

    // Create modunda userProfileId kontrolü
    if (!this.isEditMode && !this.userProfileId) {
      this.errorMessage = 'User profile ID bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.addressForm.value;
    const addressLine = formValue.addressLine.trim();
    const cityId = formValue.cityId?.id || formValue.cityId;

    if (this.isEditMode) {
      // Update mode
      const updateRequest = {
        id: this.address!.id!,
        addressLine: addressLine,
        cityId: cityId
      };

      this.addressService.updateAddress(this.address!.id!, updateRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Adres güncellenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error updating address:', error);
          this.errorMessage = 'Adres güncellenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    } else {
      // Create mode
      const createRequest = {
        userProfileId: this.userProfileId!,
        addressLine: addressLine,
        cityId: cityId
      };

      this.addressService.createAddress(createRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Adres eklenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error creating address:', error);
          this.errorMessage = 'Adres eklenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    }
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }

  getErrorMessage(fieldName: string): string | null {
    const control = this.addressForm.get(fieldName);
    if (control && control.invalid && control.touched) {
      if (control.errors?.['required']) {
        return 'Bu alan zorunludur.';
      }
      if (control.errors?.['minlength']) {
        return `En az ${control.errors['minlength'].requiredLength} karakter olmalıdır.`;
      }
    }
    return null;
  }
}

