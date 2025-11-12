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
import { CheckboxModule } from 'primeng/checkbox';
import { ExperienceService } from '../../../services/experience.service';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';

@Component({
  selector: 'app-experience',
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
    DatePickerModule,
    CheckboxModule
  ],
  templateUrl: './experience.component.html',
  styleUrl: './experience.component.css'
})
export class ExperienceComponent implements OnInit {
  private fb = inject(FormBuilder);
  private experienceService = inject(ExperienceService);
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  experienceForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  isCurrentJob = false;
  maxDate = new Date();

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.Experience);
    this.initializeForm();
  }

  initializeForm(): void {
    this.isCurrentJob = false;
    this.experienceForm = this.fb.group({
      companyName: ['', [Validators.required, Validators.minLength(2)]],
      position: ['', [Validators.required, Validators.minLength(2)]],
      startDate: [null, [Validators.required]],
      endDate: [null, [Validators.required]],
      isCurrentJob: [false],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });

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

    // İlk yüklemede isCurrentJob değerini senkronize et
    this.isCurrentJob = this.experienceForm.get('isCurrentJob')?.value || false;
  }

  get isEndDateDisabled(): boolean {
    return this.experienceForm.get('endDate')?.disabled || false;
  }

  onSubmit(): void {
    if (this.experienceForm.invalid) {
      this.markFormGroupTouched(this.experienceForm);
      return;
    }

    const lawyerProfileId = this.onboardingService.currentState.lawyerProfileId;
    if (!lawyerProfileId) {
      this.errorMessage = 'Önce avukat profilini oluşturmalısınız.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.experienceForm.value;
    const request = {
      lawyerProfileId: lawyerProfileId,
      companyName: formValue.companyName,
      position: formValue.position,
      startDate: new Date(formValue.startDate).toISOString(),
      endDate: this.isCurrentJob ? null : new Date(formValue.endDate).toISOString(),
      description: formValue.description
    };

    this.experienceService.createExperience(request).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.onboardingService.markStepCompleted(OnboardingStep.Experience);
          this.navigateToNext();
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Deneyim bilgisi oluşturulurken bir hata oluştu.';
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
    this.onboardingService.markStepCompleted(OnboardingStep.Experience);
    this.navigateToNext();
  }

  private navigateToNext(): void {
    const nextStep = this.onboardingService.nextStep();
    if (nextStep) {
      this.router.navigate([`/onboarding/${this.onboardingService.getStepRoute(nextStep)}`]);
    } else {
      this.router.navigate(['/dashboard']);
    }
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.experienceForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.experienceForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('minlength')) {
      return `Minimum ${field.errors?.['minlength'].requiredLength} karakter olmalıdır.`;
    }
    return '';
  }
}
