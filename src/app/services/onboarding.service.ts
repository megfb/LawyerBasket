import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export enum OnboardingStep {
  Welcome = 'welcome',
  UserProfile = 'user-profile',
  LawyerProfile = 'lawyer-profile',
  Address = 'address',
  Academy = 'academy',
  Experience = 'experience',
  LawyerExpertisement = 'lawyer-expertisement',
  Certificates = 'certificates'
}

export interface OnboardingState {
  currentStep: OnboardingStep;
  completedSteps: OnboardingStep[];
  userProfileId?: string;
  lawyerProfileId?: string;
}

@Injectable({
  providedIn: 'root'
})
export class OnboardingService {
  private readonly STEPS: OnboardingStep[] = [
    OnboardingStep.Welcome,
    OnboardingStep.UserProfile,
    OnboardingStep.LawyerProfile,
    OnboardingStep.Address,
    OnboardingStep.Academy,
    OnboardingStep.Experience,
    OnboardingStep.LawyerExpertisement,
    OnboardingStep.Certificates
  ];

  private stateSubject = new BehaviorSubject<OnboardingState>({
    currentStep: OnboardingStep.Welcome,
    completedSteps: []
  });

  public state$: Observable<OnboardingState> = this.stateSubject.asObservable();

  get currentState(): OnboardingState {
    return this.stateSubject.value;
  }

  get totalSteps(): number {
    return this.STEPS.length;
  }

  get currentStepIndex(): number {
    return this.STEPS.indexOf(this.currentState.currentStep);
  }

  get progress(): number {
    return ((this.currentStepIndex + 1) / this.totalSteps) * 100;
  }

  isOptionalStep(step: OnboardingStep): boolean {
    return [
      OnboardingStep.Address,
      OnboardingStep.Academy,
      OnboardingStep.Experience,
      OnboardingStep.LawyerExpertisement,
      OnboardingStep.Certificates
    ].includes(step);
  }

  initialize(currentStep: OnboardingStep = OnboardingStep.Welcome): void {
    this.stateSubject.next({
      currentStep,
      completedSteps: [],
      userProfileId: undefined,
      lawyerProfileId: undefined
    });
  }

  setCurrentStep(step: OnboardingStep): void {
    const currentState = this.currentState;
    this.stateSubject.next({
      ...currentState,
      currentStep: step
    });
  }

  markStepCompleted(step: OnboardingStep): void {
    const currentState = this.currentState;
    if (!currentState.completedSteps.includes(step)) {
      this.stateSubject.next({
        ...currentState,
        completedSteps: [...currentState.completedSteps, step]
      });
    }
  }

  nextStep(): OnboardingStep | null {
    const currentIndex = this.currentStepIndex;
    if (currentIndex < this.STEPS.length - 1) {
      const nextStep = this.STEPS[currentIndex + 1];
      this.setCurrentStep(nextStep);
      return nextStep;
    }
    return null;
  }

  getStepRoute(step: OnboardingStep): string {
    const routeMap: Record<OnboardingStep, string> = {
      [OnboardingStep.Welcome]: 'welcome',
      [OnboardingStep.UserProfile]: 'user-profile',
      [OnboardingStep.LawyerProfile]: 'lawyer-profile',
      [OnboardingStep.Address]: 'address',
      [OnboardingStep.Academy]: 'academy',
      [OnboardingStep.Experience]: 'experience',
      [OnboardingStep.LawyerExpertisement]: 'lawyer-expertisement',
      [OnboardingStep.Certificates]: 'certificates'
    };
    return routeMap[step] || 'welcome';
  }

  previousStep(): OnboardingStep | null {
    const currentIndex = this.currentStepIndex;
    if (currentIndex > 0) {
      const previousStep = this.STEPS[currentIndex - 1];
      this.setCurrentStep(previousStep);
      return previousStep;
    }
    return null;
  }

  skipStep(): void {
    const step = this.nextStep();
    if (step) {
      this.markStepCompleted(step);
    }
  }

  setUserProfileId(id: string): void {
    const currentState = this.currentState;
    this.stateSubject.next({
      ...currentState,
      userProfileId: id
    });
  }

  setLawyerProfileId(id: string): void {
    const currentState = this.currentState;
    this.stateSubject.next({
      ...currentState,
      lawyerProfileId: id
    });
  }

  reset(): void {
    this.initialize();
  }
}

