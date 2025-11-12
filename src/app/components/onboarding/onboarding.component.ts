import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { ProgressBarModule } from 'primeng/progressbar';
import { OnboardingService, OnboardingStep } from '../../services/onboarding.service';

@Component({
  selector: 'app-onboarding',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    ProgressBarModule
  ],
  templateUrl: './onboarding.component.html',
  styleUrl: './onboarding.component.css'
})
export class OnboardingComponent implements OnInit {
  private onboardingService = inject(OnboardingService);
  private router = inject(Router);

  progress = 0;
  currentStep = OnboardingStep.Welcome;
  totalSteps = 0;

  ngOnInit(): void {
    this.onboardingService.initialize();
    this.onboardingService.state$.subscribe(state => {
      this.currentStep = state.currentStep;
      this.progress = this.onboardingService.progress;
      this.totalSteps = this.onboardingService.totalSteps;
    });
  }

  getCurrentStepIndex(): number {
    return this.onboardingService.currentStepIndex;
  }
}

