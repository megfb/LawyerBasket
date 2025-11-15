import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { SelectModule } from 'primeng/select';
import { AddressService } from '../../../services/address.service';
import { CityService } from '../../../services/city.service';
import { CityDto } from '../../../models/profile-api.models';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';

@Component({
  selector: 'app-address',
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
    SelectModule
  ],
  templateUrl: './address.component.html',
  styleUrl: './address.component.css'
})
export class AddressComponent implements OnInit {
  private fb = inject(FormBuilder);
  private addressService = inject(AddressService);
  private cityService = inject(CityService);
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  addressForm!: FormGroup;
  isSubmitting = false;
  errorMessage: string | null = null;
  isLoadingCities = false;
  cities: CityDto[] = [];

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.Address);
    this.initializeForm();
    this.loadCities();
  }

  loadCities(): void {
    this.isLoadingCities = true;
    this.cityService.getCities().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.cities = response.data;
        } else {
          this.errorMessage = 'Şehirler yüklenirken bir hata oluştu.';
        }
        this.isLoadingCities = false;
      },
      error: (error) => {
        this.isLoadingCities = false;
        this.errorMessage = 'Şehirler yüklenirken bir hata oluştu. Lütfen tekrar deneyin.';
        console.error('Error loading cities:', error);
      }
    });
  }

  initializeForm(): void {
    this.addressForm = this.fb.group({
      addressLine: ['', [Validators.required, Validators.maxLength(500)]],
      cityId: ['', [Validators.required]]
    });
  }

  onSubmit(): void {
    if (this.addressForm.invalid) {
      this.markFormGroupTouched(this.addressForm);
      return;
    }

    const userProfileId = this.onboardingService.currentState.userProfileId;
    if (!userProfileId) {
      this.errorMessage = 'Önce kullanıcı profilini oluşturmalısınız.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const formValue = this.addressForm.value;
    const request = {
      userProfileId: userProfileId,
      addressLine: formValue.addressLine,
      cityId: formValue.cityId
    };

    this.addressService.createAddress(request).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.onboardingService.markStepCompleted(OnboardingStep.Address);
          this.navigateToNext();
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Adres oluşturulurken bir hata oluştu.';
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
    this.onboardingService.markStepCompleted(OnboardingStep.Address);
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
    const field = this.addressForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.addressForm.get(fieldName);
    if (field?.hasError('required')) {
      return 'Bu alan zorunludur.';
    }
    if (field?.hasError('maxlength')) {
      return `Maksimum ${field.errors?.['maxlength'].requiredLength} karakter olabilir.`;
    }
    return '';
  }
}

