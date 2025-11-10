import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';

export const authRedirectGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('accessToken');
  
  // If user is already logged in, redirect to dashboard
  if (token) {
    router.navigate(['/dashboard']);
    return false;
  }
  
  return true;
};

