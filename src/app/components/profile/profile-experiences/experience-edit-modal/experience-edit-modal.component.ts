import { Component, EventEmitter, Input, OnInit, OnChanges, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { CheckboxModule } from 'primeng/checkbox';
import { Experience } from '../../../../models/profile.models';
import { ExperienceService } from '../../../../services/experience.service';

@Component({
  selector: 'app-experience-edit-modal',
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
    MessageModule,
    CheckboxModule
  ],
  templateUrl: './experience-edit-modal.component.html',
  styleUrl: './experience-edit-modal.component.css'
})
export class ExperienceEditModalComponent implements OnInit, OnChanges {
  private fb = inject(FormBuilder);
  private experienceService = inject(ExperienceService);

  @Input() visible: boolean = false;
  @Input() experience: Experience | null = null;
  @Input() lawyerProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();
  @Output() deleted = new EventEmitter<string>();

  experienceForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  isCurrentJob = false;
  today = new Date();

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnChanges(): void {
    if (this.visible) {
      this.initializeForm();
    }
  }

  private initializeForm(): void {
    if (this.experience) {
      const startDate = this.parseDate(this.experience.startDate);
      const endDate = this.experience.endDate ? this.parseDate(this.experience.endDate) : null;
      this.isCurrentJob = !this.experience.endDate;

      this.experienceForm = this.fb.group({
        companyName: [this.experience.companyName || '', [Validators.required, Validators.minLength(2)]],
        position: [this.experience.position || '', [Validators.required, Validators.minLength(2)]],
        startDate: [startDate, [Validators.required]],
        endDate: [endDate], // Disabled state'i form control metodlarıyla yöneteceğiz
        isCurrentJob: [this.isCurrentJob],
        description: [this.experience.description || '', [Validators.required, Validators.minLength(10)]]
      });

      // İlk yüklemede endDate'in disabled state'ini ve validators'ını ayarla
      const endDateControl = this.experienceForm.get('endDate');
      if (this.isCurrentJob) {
        endDateControl?.disable({ emitEvent: false });
        endDateControl?.clearValidators();
      } else {
        endDateControl?.enable({ emitEvent: false });
        endDateControl?.setValidators([Validators.required]);
      }
      endDateControl?.updateValueAndValidity({ emitEvent: false });

    } else {
      this.isCurrentJob = false;
      this.experienceForm = this.fb.group({
        companyName: ['', [Validators.required, Validators.minLength(2)]],
        position: ['', [Validators.required, Validators.minLength(2)]],
        startDate: [null, [Validators.required]],
        endDate: [null, [Validators.required]], // Yeni deneyim için başlangıçta enabled
        isCurrentJob: [false],
        description: ['', [Validators.required, Validators.minLength(10)]]
      });
    }

    // EndDate'i isCurrentJob'a göre kontrol et
    this.experienceForm.get('isCurrentJob')?.valueChanges.subscribe(value => {
      this.isCurrentJob = !!value;
      const endDateControl = this.experienceForm.get('endDate');
      
      if (this.isCurrentJob) {
        endDateControl?.clearValidators();
        endDateControl?.setValue(null, { emitEvent: false });
        endDateControl?.disable({ emitEvent: false });
      } else {
        endDateControl?.enable({ emitEvent: false });
        endDateControl?.setValidators([Validators.required]);
      }
      endDateControl?.updateValueAndValidity({ emitEvent: false });
    });

    // Tarih validasyonu
    this.experienceForm.get('startDate')?.valueChanges.subscribe(() => {
      this.validateDates();
    });
    this.experienceForm.get('endDate')?.valueChanges.subscribe(() => {
      this.validateDates();
    });
  }

  private validateDates(): void {
    const startDate = this.experienceForm.get('startDate')?.value;
    const endDate = this.experienceForm.get('endDate')?.value;
    const isCurrent = this.experienceForm.get('isCurrentJob')?.value;

    if (startDate && endDate && !isCurrent) {
      if (new Date(endDate) < new Date(startDate)) {
        this.experienceForm.get('endDate')?.setErrors({ dateRange: true });
      } else {
        const errors = this.experienceForm.get('endDate')?.errors;
        if (errors) {
          delete errors['dateRange'];
          if (Object.keys(errors).length === 0) {
            this.experienceForm.get('endDate')?.setErrors(null);
          } else {
            this.experienceForm.get('endDate')?.setErrors(errors);
          }
        }
      }
    }
  }

  private parseDate(dateString: string): Date {
    // Format: YYYY-MM
    const [year, month] = dateString.split('-');
    return new Date(parseInt(year), parseInt(month) - 1, 1);
  }

  private formatDateForApi(date: Date): string {
    return date.toISOString();
  }


  onClose(): void {
    this.visible = false;
    this.visibleChange.emit(false);
    this.errorMessage = null;
    this.experienceForm.reset();
  }

  get isEditMode(): boolean {
    return !!this.experience;
  }

  onSave(): void {
    if (this.experienceForm.invalid) {
      this.markFormGroupTouched(this.experienceForm);
      return;
    }

    // Create modunda lawyerProfileId kontrolü
    if (!this.isEditMode && !this.lawyerProfileId) {
      this.errorMessage = 'Lawyer profile ID bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    // Disabled olan endDate'i de almak için getRawValue kullan
    const formValue = this.experienceForm.getRawValue();
    const startDate = this.formatDateForApi(formValue.startDate);
    const endDate = formValue.isCurrentJob ? null : (formValue.endDate ? this.formatDateForApi(formValue.endDate) : null);

    if (this.isEditMode) {
      // Update mode
      const updateRequest = {
        id: this.experience!.id,
        companyName: formValue.companyName.trim(),
        position: formValue.position.trim(),
        startDate: startDate,
        endDate: endDate,
        description: formValue.description.trim()
      };

      this.experienceService.updateExperience(this.experience!.id, updateRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Deneyim güncellenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error updating experience:', error);
          this.errorMessage = 'Deneyim güncellenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    } else {
      // Create mode
      const createRequest = {
        lawyerProfileId: this.lawyerProfileId!,
        companyName: formValue.companyName.trim(),
        position: formValue.position.trim(),
        startDate: startDate,
        endDate: endDate,
        description: formValue.description.trim()
      };

      this.experienceService.createExperience(createRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Deneyim eklenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error creating experience:', error);
          this.errorMessage = 'Deneyim eklenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    }
  }

  onDelete(): void {
    if (!this.experience || !confirm('Bu deneyimi silmek istediğinizden emin misiniz?')) {
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const experienceId = this.experience.id;

    this.experienceService.deleteExperience(experienceId).subscribe({
      next: (response) => {
        // Backend ApiResult.Success döndürüyor, isSuccess: true olmalı
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          // Başarılı silme - önce event emit edelim, sonra modal'ı kapatalım
          this.isSubmitting = false;
          this.deleted.emit(experienceId);
          // Modal'ı kapat
          this.visible = false;
          this.visibleChange.emit(false);
          this.errorMessage = null;
          this.experienceForm.reset();
        } else {
          // Backend'den hata mesajı geldi
          this.errorMessage = response?.errorMessage?.join(', ') || 'Deneyim silinirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      },
      error: (error) => {
        console.error('Error deleting experience:', error);
        // HTTP 200 OK durumunda bile error callback'e düşebilir (Angular HttpClient davranışı)
        // Bu durumda response body'yi kontrol edelim
        if (error.status === 200 || error.status === 204) {
          // Başarılı silme (200 OK veya 204 No Content)
          this.isSubmitting = false;
          this.deleted.emit(experienceId);
          // Modal'ı kapat
          this.visible = false;
          this.visibleChange.emit(false);
          this.errorMessage = null;
          this.experienceForm.reset();
        } else {
          // Gerçek bir hata var
          this.errorMessage = error.error?.errorMessage?.join(', ') || error.error?.message || 'Deneyim silinirken bir hata oluştu.';
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

  getErrorMessage(fieldName: string): string {
    const control = this.experienceForm.get(fieldName);
    if (!control || !control.errors || !control.touched) {
      return '';
    }

    if (control.errors['required']) {
      return `${this.getFieldLabel(fieldName)} zorunludur.`;
    }
    if (control.errors['minlength']) {
      return `${this.getFieldLabel(fieldName)} en az ${control.errors['minlength'].requiredLength} karakter olmalıdır.`;
    }
    if (control.errors['dateRange']) {
      return 'Bitiş tarihi başlangıç tarihinden önce olamaz.';
    }

    return '';
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      companyName: 'Şirket adı',
      position: 'Pozisyon',
      startDate: 'Başlangıç tarihi',
      endDate: 'Bitiş tarihi',
      description: 'Açıklama'
    };
    return labels[fieldName] || fieldName;
  }
}

