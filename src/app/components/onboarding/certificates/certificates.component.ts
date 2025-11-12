import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { DatePickerModule } from 'primeng/datepicker';
import { CertificateService } from '../../../services/certificate.service';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';

@Component({
  selector: 'app-certificates',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    MessageModule,
    DatePickerModule
  ],
  templateUrl: './certificates.component.html',
  styleUrl: './certificates.component.css'
})
export class CertificatesComponent implements OnInit {
  private fb = inject(FormBuilder);
  private certificateService = inject(CertificateService);
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  certificateForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  maxDate = new Date();

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.Certificates);
    this.initializeForm();
  }

  initializeForm(): void {
    this.certificateForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      institution: ['', [Validators.required, Validators.minLength(2)]],
      dateReceived: [null, [Validators.required]]
    });
  }

  onSubmit(): void {
    if (this.certificateForm.invalid) {
      this.markFormGroupTouched(this.certificateForm);
      return;
    }

    const lawyerProfileId = this.onboardingService.currentState.lawyerProfileId;
    if (!lawyerProfileId) {
      this.errorMessage = 'Önce avukat profilini oluşturmalısınız.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.certificateForm.value;
    const request = {
      lawyerProfileId: lawyerProfileId,
      name: formValue.name,
      institution: formValue.institution,
      dateReceived: new Date(formValue.dateReceived).toISOString()
    };

    this.certificateService.createCertificate(request).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.onboardingService.markStepCompleted(OnboardingStep.Certificates);
          this.router.navigate(['/dashboard']);
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Sertifika oluşturulurken bir hata oluştu.';
        }
        this.isSubmitting = false;
      },
      error: (error) => {
        this.isSubmitting = false;
        this.errorMessage = error.error?.errorMessage?.join(', ') || 'Bir hata oluştu. Lütfen tekrar deneyin.';
      }
    });
  }

  onSkip(): void {
    this.onboardingService.markStepCompleted(OnboardingStep.Certificates);
    this.router.navigate(['/dashboard']);
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.certificateForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.certificateForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('minlength')) {
      return `Minimum ${field.errors?.['minlength'].requiredLength} karakter olmalıdır.`;
    }
    return '';
  }
}
