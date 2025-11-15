import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { DatePickerModule } from 'primeng/datepicker';
import { CheckboxModule } from 'primeng/checkbox';
import { AcademyService } from '../../../services/academy.service';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';

@Component({
  selector: 'app-academy',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    MessageModule,
    DatePickerModule,
    CheckboxModule
  ],
  templateUrl: './academy.component.html',
  styleUrl: './academy.component.css'
})
export class AcademyComponent implements OnInit {
  private fb = inject(FormBuilder);
  private academyService = inject(AcademyService);
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  academyForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  isCurrentlyStudying = false;
  maxDate = new Date();

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.Academy);
    this.initializeForm();
  }

  initializeForm(): void {
    this.isCurrentlyStudying = false;
    this.academyForm = this.fb.group({
      university: ['', [Validators.required, Validators.minLength(2)]],
      degree: [''],
      department: ['', [Validators.maxLength(200)]],
      startDate: [null, [Validators.required]],
      endDate: [null, [Validators.required]],
      isCurrentlyStudying: [false]
    });

    // EndDate'i isCurrentlyStudying'a göre kontrol et
    this.academyForm.get('isCurrentlyStudying')?.valueChanges.subscribe(value => {
      this.isCurrentlyStudying = !!value;
      const endDateControl = this.academyForm.get('endDate');
      
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

    // İlk yüklemede isCurrentlyStudying değerini senkronize et
    this.isCurrentlyStudying = this.academyForm.get('isCurrentlyStudying')?.value || false;
  }

  get isEndDateDisabled(): boolean {
    return this.academyForm.get('endDate')?.disabled || false;
  }

  onSubmit(): void {
    if (this.academyForm.invalid) {
      this.markFormGroupTouched(this.academyForm);
      return;
    }

    const lawyerProfileId = this.onboardingService.currentState.lawyerProfileId;
    if (!lawyerProfileId) {
      this.errorMessage = 'Önce avukat profilini oluşturmalısınız.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.academyForm.value;
    const request = {
      lawyerProfileId: lawyerProfileId,
      university: formValue.university,
      degree: formValue.degree || undefined,
      department: formValue.department || undefined,
      startDate: new Date(formValue.startDate).toISOString(),
      endDate: this.isCurrentlyStudying ? null : new Date(formValue.endDate).toISOString()
    };

    this.academyService.createAcademy(request).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.onboardingService.markStepCompleted(OnboardingStep.Academy);
          this.navigateToNext();
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Eğitim bilgisi oluşturulurken bir hata oluştu.';
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
    this.onboardingService.markStepCompleted(OnboardingStep.Academy);
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
    const field = this.academyForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.academyForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('minlength')) {
      return `Minimum ${field.errors?.['minlength'].requiredLength} karakter olmalıdır.`;
    }
    if (field?.hasError('maxlength')) {
      return `Maksimum ${field.errors?.['maxlength'].requiredLength} karakter olabilir.`;
    }
    return '';
  }
}
