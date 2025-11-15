import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { DatePickerModule } from 'primeng/datepicker';
import { LawyerProfileService } from '../../../services/lawyer-profile.service';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';

@Component({
  selector: 'app-lawyer-profile',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    TextareaModule,
    ButtonModule,
    MessageModule,
    DatePickerModule
  ],
  templateUrl: './lawyer-profile.component.html',
  styleUrl: './lawyer-profile.component.css'
})
export class LawyerProfileComponent implements OnInit {
  private fb = inject(FormBuilder);
  private lawyerProfileService = inject(LawyerProfileService);
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  lawyerProfileForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  maxDate = new Date();

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.LawyerProfile);
    this.initializeForm();
  }

  initializeForm(): void {
    this.lawyerProfileForm = this.fb.group({
      barAssociation: ['', [Validators.required, Validators.maxLength(200)]],
      barNumber: ['', [Validators.required, Validators.maxLength(50)]],
      licenseNumber: ['', [Validators.required, Validators.maxLength(50)]],
      licenseDate: [null, [Validators.required]],
      about: ['', [Validators.maxLength(2000)]]
    });
  }

  onSubmit(): void {
    if (this.lawyerProfileForm.invalid) {
      this.markFormGroupTouched(this.lawyerProfileForm);
      return;
    }

    const userProfileId = this.onboardingService.currentState.userProfileId;
    if (!userProfileId) {
      this.errorMessage = 'Önce kullanıcı profilini oluşturmalısınız.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.lawyerProfileForm.value;
    const request = {
      userProfileId: userProfileId,
      barAssociation: formValue.barAssociation,
      barNumber: formValue.barNumber,
      licenseNumber: formValue.licenseNumber,
      licenseDate: new Date(formValue.licenseDate).toISOString(),
      about: formValue.about || undefined
    };

    this.lawyerProfileService.createLawyerProfile(request).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.onboardingService.setLawyerProfileId(response.data.id);
          this.onboardingService.markStepCompleted(OnboardingStep.LawyerProfile);
          
          // Sonraki adıma geç (Address opsiyonel)
          const nextStep = this.onboardingService.nextStep();
          if (nextStep) {
            this.router.navigate([`/onboarding/${this.onboardingService.getStepRoute(nextStep)}`]);
          } else {
            // Tüm adımlar tamamlandı, dashboard'a yönlendir
            this.router.navigate(['/dashboard']);
          }
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Avukat profili oluşturulurken bir hata oluştu.';
        }
        this.isSubmitting = false;
      },
      error: (error) => {
        this.isSubmitting = false;
        this.errorMessage = error.error?.errorMessage?.join(', ') || 'Bir hata oluştu. Lütfen tekrar deneyin.';
      }
    });
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.lawyerProfileForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.lawyerProfileForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('maxlength')) {
      return `Maksimum ${field.errors?.['maxlength'].requiredLength} karakter olabilir.`;
    }
    return '';
  }
}

