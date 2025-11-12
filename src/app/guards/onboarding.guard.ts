import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { ProfileService } from '../services/profile.service';

export const onboardingGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const profileService = inject(ProfileService);

  // Check if user has completed UserProfile and LawyerProfile
  return profileService.getUserProfileFull().pipe(
    map(response => {
      if (response.isSuccess && response.data) {
        const hasUserProfile = !!response.data.id;
        const hasLawyerProfile = !!response.data.lawyerProfile?.id;

        // If both profiles exist, redirect to dashboard (onboarding not needed)
        if (hasUserProfile && hasLawyerProfile) {
          router.navigate(['/dashboard']);
          return false;
        }

        // If onboarding is needed, allow access
        return true;
      }

      // If API call fails, allow onboarding (user might not have profile yet)
      return true;
    }),
    catchError(() => {
      // On error, allow onboarding (user might not have profile yet)
      return of(true);
    })
  );
};

