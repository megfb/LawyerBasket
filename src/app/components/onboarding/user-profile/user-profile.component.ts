import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';
import { UserProfileService } from '../../../services/user-profile.service';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';
import { AuthService } from '../../../services/auth.service';
import { UserType } from '../../../models/profile-api.models';

interface GenderOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    MessageModule,
    SelectModule,
    DatePickerModule
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit {
  private fb = inject(FormBuilder);
  private userProfileService = inject(UserProfileService);
  private onboardingService = inject(OnboardingService);
  private authService = inject(AuthService);
  private router = inject(Router);

  userProfileForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  maxDate = new Date();

  // Gender listesi - backend'den gelecek veya hardcode edilebilir
  genders: GenderOption[] = [
    { id: 'c1d2e3f4-0001-4a5b-8c9d-1a2b3c4d5e6f', name: 'Erkek' },
    { id: 'c1d2e3f4-0002-4a5b-8c9d-1a2b3c4d5e6f', name: 'Kadın' }
  ];

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.UserProfile);
    this.initializeForm();
  }

  initializeForm(): void {
    this.userProfileForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(200)]],
      phoneNumber: ['', [Validators.maxLength(20)]],
      genderId: ['', [Validators.required]],
      birthDate: [null],
      nationalId: ['', [Validators.maxLength(50)]]
    });
  }

  onSubmit(): void {
    if (this.userProfileForm.invalid) {
      this.markFormGroupTouched(this.userProfileForm);
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.userProfileForm.value;
    const request = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phoneNumber: formValue.phoneNumber || undefined,
      genderId: formValue.genderId,
      birthDate: formValue.birthDate ? new Date(formValue.birthDate).toISOString() : null,
      nationalId: formValue.nationalId || undefined,
      userType: UserType.Lawyer
    };

    this.userProfileService.createUserProfile(request).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.onboardingService.setUserProfileId(response.data.id);
          this.onboardingService.markStepCompleted(OnboardingStep.UserProfile);
          const nextStep = this.onboardingService.nextStep();
          if (nextStep) {
            this.router.navigate([`/onboarding/${this.onboardingService.getStepRoute(nextStep)}`]);
          }
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Profil oluşturulurken bir hata oluştu.';
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
    const field = this.userProfileForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.userProfileForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('email')) {
      return 'Geçerli bir e-posta adresi giriniz.';
    }
    if (field?.hasError('maxlength')) {
      return `Maksimum ${field.errors?.['maxlength'].requiredLength} karakter olabilir.`;
    }
    return '';
  }
}

