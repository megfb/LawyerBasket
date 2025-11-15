import { Component, EventEmitter, Input, OnInit, OnChanges, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { CheckboxModule } from 'primeng/checkbox';
import { Education } from '../../../../models/profile.models';
import { AcademyService } from '../../../../services/academy.service';

@Component({
  selector: 'app-education-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    InputTextModule,
    DatePickerModule,
    ButtonModule,
    MessageModule,
    CheckboxModule
  ],
  templateUrl: './education-edit-modal.component.html',
  styleUrl: './education-edit-modal.component.css'
})
export class EducationEditModalComponent implements OnInit, OnChanges {
  private fb = inject(FormBuilder);
  private academyService = inject(AcademyService);

  @Input() visible: boolean = false;
  @Input() education: Education | null = null;
  @Input() lawyerProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();
  @Output() deleted = new EventEmitter<string>();

  educationForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  isCurrentlyStudying = false;
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
    if (this.education) {
      // Education modelinden AcademyDto formatına çevir
      // Education'da startDate ve endDate string formatında (YYYY-MM)
      const startDate = this.education.startDate 
        ? this.parseDate(this.education.startDate)
        : (this.education.graduationYear 
          ? new Date(this.education.graduationYear - 4, 0, 1) // Mezuniyet yılından 4 yıl önce (fallback)
          : new Date(new Date().getFullYear() - 4, 0, 1)); // Varsayılan: 4 yıl önce
      const endDate = this.education.endDate 
        ? this.parseDate(this.education.endDate)
        : (this.education.graduationYear 
          ? new Date(this.education.graduationYear, 11, 31) // Yıl sonu (fallback)
          : null);
      this.isCurrentlyStudying = !this.education.endDate;

      this.educationForm = this.fb.group({
        university: [this.education.schoolName || '', [Validators.required, Validators.minLength(2)]],
        degree: [this.education.degree || ''],
        department: [this.education.department || '', [Validators.maxLength(200)]],
        startDate: [startDate, [Validators.required]],
        endDate: [endDate],
        isCurrentlyStudying: [this.isCurrentlyStudying]
      });

      // İlk yüklemede endDate'in disabled state'ini ve validators'ını ayarla
      const endDateControl = this.educationForm.get('endDate');
      if (this.isCurrentlyStudying) {
        endDateControl?.disable({ emitEvent: false });
        endDateControl?.clearValidators();
      } else {
        endDateControl?.enable({ emitEvent: false });
        endDateControl?.setValidators([Validators.required]);
      }
      endDateControl?.updateValueAndValidity({ emitEvent: false });

    } else {
      this.isCurrentlyStudying = false;
      this.educationForm = this.fb.group({
        university: ['', [Validators.required, Validators.minLength(2)]],
        degree: [''],
        department: ['', [Validators.maxLength(200)]],
        startDate: [null, [Validators.required]],
        endDate: [null, [Validators.required]],
        isCurrentlyStudying: [false]
      });
    }

    // EndDate'i isCurrentlyStudying'a göre kontrol et
    this.educationForm.get('isCurrentlyStudying')?.valueChanges.subscribe(value => {
      this.isCurrentlyStudying = !!value;
      const endDateControl = this.educationForm.get('endDate');
      
      if (this.isCurrentlyStudying) {
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
    this.educationForm.get('startDate')?.valueChanges.subscribe(() => {
      this.validateDates();
    });

    this.educationForm.get('endDate')?.valueChanges.subscribe(() => {
      this.validateDates();
    });
  }

  private validateDates(): void {
    const startDate = this.educationForm.get('startDate')?.value;
    const endDate = this.educationForm.get('endDate')?.value;

    if (startDate && endDate && !this.isCurrentlyStudying) {
      const start = new Date(startDate);
      const end = new Date(endDate);

      if (end < start) {
        this.educationForm.get('endDate')?.setErrors({ dateRange: true });
      } else {
        this.educationForm.get('endDate')?.setErrors(null);
        this.educationForm.get('endDate')?.updateValueAndValidity();
      }
    }
  }


  private parseDate(dateString: string): Date {
    // "YYYY-MM" formatından Date'e çevir
    const [year, month] = dateString.split('-');
    return new Date(parseInt(year), parseInt(month) - 1, 1);
  }

  private formatDateForApi(date: Date): string {
    return date.toISOString();
  }

  get isEditMode(): boolean {
    return !!this.education;
  }

  getErrorMessage(fieldName: string): string | null {
    const control = this.educationForm.get(fieldName);
    if (control && control.invalid && control.touched) {
      if (control.hasError('required')) {
        return 'Bu alan zorunludur.';
      }
      if (control.hasError('minlength')) {
        return `En az ${control.errors?.['minlength'].requiredLength} karakter olmalıdır.`;
      }
      if (control.hasError('maxlength')) {
        return `Maksimum ${control.errors?.['maxlength'].requiredLength} karakter olabilir.`;
      }
      if (control.hasError('dateRange')) {
        return 'Bitiş tarihi başlangıç tarihinden önce olamaz.';
      }
    }
    return null;
  }

  onClose(): void {
    this.visible = false;
    this.visibleChange.emit(false);
    this.errorMessage = null;
    this.educationForm.reset();
  }

  onSave(): void {
    if (this.educationForm.invalid) {
      this.markFormGroupTouched(this.educationForm);
      return;
    }

    // Create modunda lawyerProfileId kontrolü
    if (!this.isEditMode && !this.lawyerProfileId) {
      this.errorMessage = 'Lawyer profile ID bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.educationForm.getRawValue();
    const startDate = this.formatDateForApi(formValue.startDate);
    const endDate = formValue.isCurrentlyStudying 
      ? null 
      : (formValue.endDate ? this.formatDateForApi(formValue.endDate) : null);

    if (this.isEditMode) {
      // Update mode
      const updateRequest = {
        id: this.education!.id,
        university: formValue.university.trim(),
        degree: formValue.degree?.trim() || undefined,
        department: formValue.department?.trim() || undefined,
        startDate: startDate,
        endDate: endDate
      };

      this.academyService.updateAcademy(this.education!.id, updateRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Eğitim bilgisi güncellenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error updating education:', error);
          this.errorMessage = 'Eğitim bilgisi güncellenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    } else {
      // Create mode
      const createRequest = {
        lawyerProfileId: this.lawyerProfileId!,
        university: formValue.university.trim(),
        degree: formValue.degree?.trim() || undefined,
        department: formValue.department?.trim() || undefined,
        startDate: startDate,
        endDate: endDate
      };

      this.academyService.createAcademy(createRequest).subscribe({
        next: (response) => {
          if (response.isSuccess) {
            this.saved.emit();
            this.onClose();
          } else {
            this.errorMessage = response.errorMessage?.join(', ') || 'Eğitim bilgisi eklenirken bir hata oluştu.';
          }
          this.isSubmitting = false;
        },
        error: (error) => {
          console.error('Error creating education:', error);
          this.errorMessage = 'Eğitim bilgisi eklenirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      });
    }
  }

  onDelete(): void {
    if (!this.education || !confirm('Bu eğitim bilgisini silmek istediğinizden emin misiniz?')) {
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const educationId = this.education.id;

    this.academyService.deleteAcademy(educationId).subscribe({
      next: (response) => {
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.isSubmitting = false;
          this.deleted.emit(educationId);
          this.visible = false;
          this.visibleChange.emit(false);
          this.errorMessage = null;
          this.educationForm.reset();
        } else {
          this.errorMessage = response?.errorMessage?.join(', ') || 'Eğitim bilgisi silinirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      },
      error: (error) => {
        console.error('Error deleting education:', error);
        if (error.status === 200 || error.status === 204) {
          this.isSubmitting = false;
          this.deleted.emit(educationId);
          this.visible = false;
          this.visibleChange.emit(false);
          this.errorMessage = null;
          this.educationForm.reset();
        } else {
          this.errorMessage = error.error?.errorMessage?.join(', ') || error.error?.message || 'Eğitim bilgisi silinirken bir hata oluştu.';
          this.isSubmitting = false;
        }
      }
    });
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }
}

