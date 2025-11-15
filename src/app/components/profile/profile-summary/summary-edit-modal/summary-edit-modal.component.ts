import { Component, EventEmitter, Input, OnInit, OnChanges, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { ProfileSummary } from '../../../../models/profile.models';
import { LawyerProfileService } from '../../../../services/lawyer-profile.service';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-summary-edit-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    InputTextModule,
    TextareaModule,
    ButtonModule,
    MessageModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './summary-edit-modal.component.html',
  styleUrl: './summary-edit-modal.component.css'
})
export class SummaryEditModalComponent implements OnInit, OnChanges {
  private fb = inject(FormBuilder);
  private lawyerProfileService = inject(LawyerProfileService);
  private messageService = inject(MessageService);

  @Input() visible: boolean = false;
  @Input() summary: ProfileSummary | null = null;
  @Input() lawyerProfileId: string | null = null;
  @Input() userProfileId: string | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() saved = new EventEmitter<void>();

  summaryForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnChanges(): void {
    if (this.visible) {
      this.initializeForm();
    }
  }

  private initializeForm(): void {
    this.summaryForm = this.fb.group({
      about: [this.summary?.about || '', [Validators.maxLength(2000)]]
    });
    this.errorMessage = null;
  }

  onClose(): void {
    this.visibleChange.emit(false);
    this.summaryForm.reset();
    this.errorMessage = null;
  }

  onSave(): void {
    if (this.summaryForm.invalid) {
      this.summaryForm.markAllAsTouched();
      return;
    }

    if (!this.lawyerProfileId || !this.userProfileId) {
      this.errorMessage = 'Avukat profili bilgisi bulunamadı.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.summaryForm.value;
    
    // Önce mevcut lawyer profile'ı çek
    this.lawyerProfileService.getLawyerProfile().subscribe({
      next: (getResponse) => {
        if (getResponse.isSuccess && getResponse.data) {
          // Mevcut değerleri kullan, sadece about'u güncelle
          const request = {
            id: this.lawyerProfileId!,
            userProfileId: this.userProfileId!,
            barAssociation: getResponse.data.barAssociation,
            barNumber: getResponse.data.barNumber,
            licenseNumber: getResponse.data.licenseNumber,
            licenseDate: getResponse.data.licenseDate,
            about: formValue.about?.trim() || undefined
          };

          this.lawyerProfileService.updateLawyerProfile(this.lawyerProfileId!, request).subscribe({
            next: (response) => {
              this.isSubmitting = false;
              if (response.isSuccess) {
                this.messageService.add({
                  severity: 'success',
                  summary: 'Başarılı',
                  detail: 'Profil bilgileri başarıyla güncellendi.'
                });
                this.saved.emit();
                this.onClose();
              } else {
                this.errorMessage = response.errorMessage?.join(', ') || 'Profil bilgileri güncellenirken bir hata oluştu.';
              }
            },
            error: (error) => {
              this.isSubmitting = false;
              console.error('Profil güncelleme hatası:', error);
              this.errorMessage = error.error?.errorMessage?.join(', ') || 'Profil bilgileri güncellenirken bir hata oluştu.';
            }
          });
        } else {
          this.isSubmitting = false;
          this.errorMessage = 'Avukat profili bilgisi alınamadı.';
        }
      },
      error: (error) => {
        this.isSubmitting = false;
        console.error('Profil bilgisi alma hatası:', error);
        this.errorMessage = 'Avukat profili bilgisi alınamadı.';
      }
    });
  }

  getErrorMessage(fieldName: string): string {
    const field = this.summaryForm.get(fieldName);
    if (field?.hasError('maxlength')) {
      return `Maksimum ${field.errors?.['maxlength'].requiredLength} karakter olabilir.`;
    }
    return '';
  }
}

