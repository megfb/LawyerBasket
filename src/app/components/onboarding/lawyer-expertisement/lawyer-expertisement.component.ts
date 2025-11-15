import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { MultiSelectModule } from 'primeng/multiselect';
import { MessageModule } from 'primeng/message';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';
import { LawyerExpertisementService } from '../../../services/lawyer-expertisement.service';
import { ExpertisementService } from '../../../services/expertisement.service';

interface ExpertisementOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-lawyer-expertisement',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    CardModule,
    ButtonModule,
    MultiSelectModule,
    MessageModule
  ],
  templateUrl: './lawyer-expertisement.component.html',
  styleUrl: './lawyer-expertisement.component.css'
})
export class LawyerExpertisementComponent implements OnInit {
  private onboardingService = inject(OnboardingService);
  private lawyerExpertisementService = inject(LawyerExpertisementService);
  private expertisementService = inject(ExpertisementService);
  private router = inject(Router);

  availableExpertisements: ExpertisementOption[] = [];
  selectedExpertisementIds: string[] = [];
  isSubmitting = false;
  errorMessage: string | null = null;
  isLoadingExpertisements = false;

  ngOnInit(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.LawyerExpertisement);
    this.loadExpertisements();
  }

  private loadExpertisements(): void {
    this.isLoadingExpertisements = true;
    this.expertisementService.getExpertisements().subscribe({
      next: (response) => {
        this.isLoadingExpertisements = false;
        if (response.isSuccess && response.data) {
          this.availableExpertisements = response.data.map(exp => ({
            id: exp.id,
            name: exp.name
          }));
        } else {
          this.errorMessage = 'Uzmanlık alanları yüklenirken bir hata oluştu.';
        }
      },
      error: (error) => {
        this.isLoadingExpertisements = false;
        this.errorMessage = 'Uzmanlık alanları yüklenirken bir hata oluştu. Lütfen tekrar deneyin.';
        console.error('Error loading expertisements:', error);
      }
    });
  }

  onSubmit(): void {
    const lawyerProfileId = this.onboardingService.currentState.lawyerProfileId;
    if (!lawyerProfileId) {
      this.errorMessage = 'Önce avukat profilini oluşturmalısınız.';
      return;
    }

    if (!this.selectedExpertisementIds || this.selectedExpertisementIds.length === 0) {
      this.errorMessage = 'Lütfen en az bir uzmanlık alanı seçiniz.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = null;

    const request = {
      lawyerProfileId: lawyerProfileId,
      expertisementIds: this.selectedExpertisementIds
    };

    this.lawyerExpertisementService.createLawyerExpertisement(request).subscribe({
      next: (response) => {
        this.isSubmitting = false;
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.onboardingService.markStepCompleted(OnboardingStep.LawyerExpertisement);
          this.navigateToNext();
        } else {
          this.errorMessage = response?.errorMessage?.join(', ') || 'Uzmanlık alanları eklenirken bir hata oluştu.';
        }
      },
      error: (error) => {
        this.isSubmitting = false;
        this.errorMessage = error.error?.errorMessage?.join(', ') || 'Bir hata oluştu. Lütfen tekrar deneyin.';
      }
    });
  }

  onSkip(): void {
    this.onboardingService.markStepCompleted(OnboardingStep.LawyerExpertisement);
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
}
