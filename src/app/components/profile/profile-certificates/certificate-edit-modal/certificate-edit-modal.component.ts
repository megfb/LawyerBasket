import { Component, EventEmitter, Input, OnInit, OnChanges, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { Certificate } from '../../../../models/profile.models';
import { CertificateService } from '../../../../services/certificate.service';

@Component({
  selector: 'app-certificate-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    InputTextModule,
    TextareaModule,
    DatePickerModule,
    ButtonModule,
    MessageModule
  ],
  templateUrl: './certificate-edit-modal.component.html',
  styleUrl: './certificate-edit-modal.component.css'
})
export class CertificateEditModalComponent implements OnInit, OnChanges {
  private fb = inject(FormBuilder);
  private certificateService = inject(CertificateService);

  @Input() visible: boolean = false;
  @Input() certificate: Certificate | null = null;
  @Input() lawyerProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();
  @Output() deleted = new EventEmitter<string>();

  certificateForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  today = new Date();

  get isEditMode(): boolean {
    return !!this.certificate?.id;
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
    if (this.certificate) {
      // Edit mode
      const dateReceived = this.certificate.issueDate 
        ? this.parseDate(this.certificate.issueDate)
        : new Date();

      this.certificateForm = this.fb.group({
        name: [this.certificate.name || '', [Validators.required, Validators.minLength(2)]],
        institution: [this.certificate.issuingOrganization || '', [Validators.required, Validators.minLength(2)]],
        dateReceived: [dateReceived, [Validators.required]],
        description: [this.certificate.description || '', [Validators.maxLength(2000)]]
      });
    } else {
      // Create mode
      this.certificateForm = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(2)]],
        institution: ['', [Validators.required, Validators.minLength(2)]],
        dateReceived: [null, [Validators.required]],
        description: ['', [Validators.maxLength(2000)]]
      });
    }
  }

  private parseDate(dateString: string): Date {
    // Date string formatı: "YYYY-MM" veya "YYYY-MM-DD" veya tam tarih
    if (!dateString) return new Date();
    
    // "YYYY-MM" formatı
    if (dateString.match(/^\d{4}-\d{2}$/)) {
      const [year, month] = dateString.split('-').map(Number);
      return new Date(year, month - 1, 1);
    }
    
    // Diğer formatlar için Date parse
    const parsed = new Date(dateString);
    return isNaN(parsed.getTime()) ? new Date() : parsed;
  }

  private formatDateForApi(date: Date | null): string {
    if (!date) return '';
    // Backend DateTime bekliyor, ISO string formatında gönder
    return date.toISOString();
  }

  onClose(): void {
    this.visible = false;
    this.visibleChange.emit(false);
    this.errorMessage = null;
    this.certificateForm.reset();
  }

  onSave(): void {
    if (this.certificateForm.invalid) {
      this.markFormGroupTouched(this.certificateForm);
      return;
    }

    // Create modunda lawyerProfileId kontrolü
    if (!this.isEditMode && !this.lawyerProfileId) {
      this.errorMessage = 'Lawyer profile ID bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.certificateForm.value;
    const name = formValue.name.trim();
    const institution = formValue.institution.trim();
    const dateReceived = this.formatDateForApi(formValue.dateReceived);
    const description = formValue.description?.trim() || undefined;

    if (this.isEditMode) {
      // Update mode
      const updateRequest = {
        id: this.certificate!.id,
        name: name,
        institution: institution,
        dateReceived: dateReceived,
        description: description
      };

      this.certificateService.updateCertificate(this.certificate!.id, updateRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Sertifika güncellenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error updating certificate:', error);
          this.errorMessage = 'Sertifika güncellenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    } else {
      // Create mode
      const createRequest = {
        lawyerProfileId: this.lawyerProfileId!,
        name: name,
        institution: institution,
        dateReceived: dateReceived,
        description: description
      };

      this.certificateService.createCertificate(createRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Sertifika eklenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error creating certificate:', error);
          this.errorMessage = 'Sertifika eklenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    }
  }

  onDelete(): void {
    if (!this.certificate || !confirm('Bu sertifikayı silmek istediğinizden emin misiniz?')) {
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const certificateId = this.certificate.id;

    this.certificateService.deleteCertificate(certificateId).subscribe({
      next: (response) => {
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.isSubmitting = false;
          this.deleted.emit(certificateId);
          this.visible = false;
          this.visibleChange.emit(false);
          this.errorMessage = null;
          this.certificateForm.reset();
        } else {
          this.errorMessage = response?.errorMessage?.join(', ') || 'Sertifika silinirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      },
      error: (error) => {
        console.error('Error deleting certificate:', error);
        if (error.status === 200 || error.status === 204) {
          this.isSubmitting = false;
          this.deleted.emit(certificateId);
          this.visible = false;
          this.visibleChange.emit(false);
          this.errorMessage = null;
          this.certificateForm.reset();
        } else {
          this.errorMessage = error.error?.errorMessage?.join(', ') || error.error?.message || 'Sertifika silinirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      }
    });
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
    const control = this.certificateForm.get(fieldName);
    if (control && control.invalid && control.touched) {
      if (control.errors?.['required']) {
        return 'Bu alan zorunludur.';
      }
      if (control.errors?.['minlength']) {
        return `En az ${control.errors['minlength'].requiredLength} karakter olmalıdır.`;
      }
      if (control.errors?.['maxlength']) {
        return `Maksimum ${control.errors['maxlength'].requiredLength} karakter olabilir.`;
      }
    }
    return null;
  }
}

