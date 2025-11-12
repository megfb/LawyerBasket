import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { OnboardingService, OnboardingStep } from '../../../services/onboarding.service';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    CardModule
  ],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css'
})
export class WelcomeComponent {
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  onContinue(): void {
    this.onboardingService.setCurrentStep(OnboardingStep.UserProfile);
    this.router.navigate(['/onboarding/user-profile']);
  }
}

