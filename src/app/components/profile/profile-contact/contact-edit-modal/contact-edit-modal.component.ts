import { Component, EventEmitter, Input, OnInit, OnChanges, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { ContactInfo } from '../../../../models/profile.models';
import { ContactService } from '../../../../services/contact.service';

@Component({
  selector: 'app-contact-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    InputTextModule,
    ButtonModule,
    MessageModule
  ],
  templateUrl: './contact-edit-modal.component.html',
  styleUrl: './contact-edit-modal.component.css'
})
export class ContactEditModalComponent implements OnInit, OnChanges {
  private fb = inject(FormBuilder);
  private contactService = inject(ContactService);

  @Input() visible: boolean = false;
  @Input() contactInfo: ContactInfo | null = null;
  @Input() lawyerProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();

  contactForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;

  get isEditMode(): boolean {
    return !!this.contactInfo?.id;
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnChanges(): void {
    if (this.visible) {
      this.initializeForm();
    }
  }

  private initializeForm(): void {
    if (this.contactInfo && this.contactInfo.id) {
      // Edit mode
      this.contactForm = this.fb.group({
        phoneNumber: [this.contactInfo.phoneNumber || '', [Validators.required, Validators.pattern(/^[0-9+\-\s()]+$/)]],
        alternatePhoneNumber: [this.contactInfo.alternatePhoneNumber || ''],
        email: [this.contactInfo.email || '', [Validators.required, Validators.email]],
        alternateEmail: [this.contactInfo.alternateEmail || ''],
        website: [this.contactInfo.website || '']
      });
    } else {
      // Create mode
      this.contactForm = this.fb.group({
        phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9+\-\s()]+$/)]],
        alternatePhoneNumber: [''],
        email: ['', [Validators.required, Validators.email]],
        alternateEmail: [''],
        website: ['']
      });
    }
  }

  onClose(): void {
    this.visible = false;
    this.visibleChange.emit(false);
    this.errorMessage = null;
    this.contactForm.reset();
  }

  onSave(): void {
    if (this.contactForm.invalid) {
      this.markFormGroupTouched(this.contactForm);
      return;
    }

    // Create modunda lawyerProfileId kontrolü
    if (!this.isEditMode && !this.lawyerProfileId) {
      this.errorMessage = 'Lawyer profile ID bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.contactForm.value;
    const phoneNumber = formValue.phoneNumber.trim();
    const alternatePhoneNumber = formValue.alternatePhoneNumber?.trim() || null;
    const email = formValue.email.trim();
    const alternateEmail = formValue.alternateEmail?.trim() || null;
    const website = formValue.website?.trim() || null;

    if (this.isEditMode) {
      // Update mode
      const updateRequest = {
        id: this.contactInfo!.id!,
        phoneNumber: phoneNumber,
        alternatePhoneNumber: alternatePhoneNumber,
        email: email,
        alternateEmail: alternateEmail,
        website: website,
        updatedAt: new Date().toISOString()
      };

      this.contactService.updateContact(this.contactInfo!.id!, updateRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'İletişim bilgileri güncellenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error updating contact:', error);
          this.errorMessage = 'İletişim bilgileri güncellenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    } else {
      // Create mode
      const createRequest = {
        lawyerProfileId: this.lawyerProfileId!,
        phoneNumber: phoneNumber,
        alternatePhoneNumber: alternatePhoneNumber,
        email: email,
        alternateEmail: alternateEmail,
        website: website
      };

      this.contactService.createContact(createRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'İletişim bilgileri eklenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error creating contact:', error);
          this.errorMessage = 'İletişim bilgileri eklenirken bir hata oluştu.';
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
    const control = this.contactForm.get(fieldName);
    if (control && control.invalid && control.touched) {
      if (control.errors?.['required']) {
        return 'Bu alan zorunludur.';
      }
      if (control.errors?.['email']) {
        return 'Geçerli bir email adresi giriniz.';
      }
      if (control.errors?.['pattern']) {
        return 'Geçerli bir telefon numarası giriniz.';
      }
    }
    return null;
  }
}

