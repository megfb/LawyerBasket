import { Component, EventEmitter, Input, OnInit, OnChanges, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { LawyerInfo } from '../../../../models/profile.models';
import { LawyerProfileService } from '../../../../services/lawyer-profile.service';

@Component({
  selector: 'app-lawyer-info-edit-modal',
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
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './lawyer-info-edit-modal.component.html',
  styleUrl: './lawyer-info-edit-modal.component.css'
})
export class LawyerInfoEditModalComponent implements OnInit, OnChanges {
  private fb = inject(FormBuilder);
  private lawyerProfileService = inject(LawyerProfileService);
  private messageService = inject(MessageService);

  @Input() visible: boolean = false;
  @Input() lawyerInfo: LawyerInfo | null = null;
  @Input() lawyerProfileId: string | null = null;
  @Input() userProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();

  lawyerInfoForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
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
    const licenseDate = this.lawyerInfo?.licenseDate 
      ? new Date(this.lawyerInfo.licenseDate) 
      : null;

    this.lawyerInfoForm = this.fb.group({
      barAssociation: [this.lawyerInfo?.barAssociation || '', [Validators.required, Validators.maxLength(150)]],
      barNumber: [this.lawyerInfo?.barNumber || '', [Validators.required, Validators.maxLength(50)]],
      licenseNumber: [this.lawyerInfo?.licenseNumber || '', [Validators.required, Validators.maxLength(50)]],
      licenseDate: [licenseDate, [Validators.required]]
    });
    this.errorMessage = null;
  }

  onClose(): void {
    this.visibleChange.emit(false);
    this.lawyerInfoForm.reset();
    this.errorMessage = null;
  }

  onSave(): void {
    if (this.lawyerInfoForm.invalid) {
      this.lawyerInfoForm.markAllAsTouched();
      return;
    }

    if (!this.lawyerProfileId || !this.userProfileId) {
      this.errorMessage = 'Avukat profili bilgisi bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.lawyerInfoForm.value;

    // Önce mevcut lawyer profile'ı çek
    this.lawyerProfileService.getLawyerProfile().subscribe({
      next: (getResponse) => {
        if (getResponse.isSuccess && getResponse.data) {
          // Mevcut about'u koru, diğer alanları güncelle
          const request = {
            id: this.lawyerProfileId!,
            userProfileId: this.userProfileId!,
            barAssociation: formValue.barAssociation.trim(),
            barNumber: formValue.barNumber.trim(),
            licenseNumber: formValue.licenseNumber.trim(),
            licenseDate: new Date(formValue.licenseDate).toISOString(),
            about: getResponse.data.about // About'u koru
          };

          this.lawyerProfileService.updateLawyerProfile(this.lawyerProfileId!, request).subscribe({
            next: (response) => {
              this.isSubmitting = false;
              if (response.isSuccess) {
                this.messageService.add({
                  severity: 'success',
                  summary: 'Başarılı',
                  detail: 'Mesleki bilgiler başarıyla güncellendi.'
                });
                this.saved.emit();
                this.onClose();
              } else {
                this.errorMessage = response.errorMessage?.join(', ') || 'Mesleki bilgiler güncellenirken bir hata oluştu.';
              }
            },
            error: (error) => {
              this.isSubmitting = false;
              console.error('Mesleki bilgiler güncelleme hatası:', error);
              this.errorMessage = error.error?.errorMessage?.join(', ') || 'Mesleki bilgiler güncellenirken bir hata oluştu.';
            }
          });
        } else {
          this.isSubmitting = false;
          this.errorMessage = getResponse.errorMessage?.join(', ') || 'Avukat profili bilgisi alınamadı.';
        }
      },
      error: (error) => {
        this.isSubmitting = false;
        console.error('Avukat profili bilgisi alınırken hata oluştu:', error);
        this.errorMessage = error.error?.errorMessage?.join(', ') || 'Avukat profili bilgisi alınamadı.';
      }
    });
  }

  getErrorMessage(fieldName: string): string {
    const field = this.lawyerInfoForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('maxlength')) {
      return `Maksimum ${field.errors?.['maxlength'].requiredLength} karakter olabilir.`;
    }
    return '';
  }
}

