import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { LayoutComponent } from './components/layout/layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { OnboardingComponent } from './components/onboarding/onboarding.component';
import { WelcomeComponent } from './components/onboarding/welcome/welcome.component';
import { authGuard } from './guards/auth.guard';
import { authRedirectGuard } from './guards/auth-redirect.guard';
import { onboardingGuard } from './guards/onboarding.guard';

export const routes: Routes = [
  { path: 'auth', component: AuthComponent, canActivate: [authRedirectGuard] },
  {
    path: 'onboarding',
    component: OnboardingComponent,
    canActivate: [authGuard, onboardingGuard],
    children: [
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: 'welcome', component: WelcomeComponent },
      // Placeholder routes - will be created
      { path: 'user-profile', loadComponent: () => import('./components/onboarding/user-profile/user-profile.component').then(m => m.UserProfileComponent) },
      { path: 'lawyer-profile', loadComponent: () => import('./components/onboarding/lawyer-profile/lawyer-profile.component').then(m => m.LawyerProfileComponent) },
      { path: 'address', loadComponent: () => import('./components/onboarding/address/address.component').then(m => m.AddressComponent) },
      { path: 'academy', loadComponent: () => import('./components/onboarding/academy/academy.component').then(m => m.AcademyComponent) },
      { path: 'experience', loadComponent: () => import('./components/onboarding/experience/experience.component').then(m => m.ExperienceComponent) },
      { path: 'lawyer-expertisement', loadComponent: () => import('./components/onboarding/lawyer-expertisement/lawyer-expertisement.component').then(m => m.LawyerExpertisementComponent) },
      { path: 'certificates', loadComponent: () => import('./components/onboarding/certificates/certificates.component').then(m => m.CertificatesComponent) }
    ]
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'jobs', component: DashboardComponent }, // Placeholder
      { path: 'messages', component: DashboardComponent }, // Placeholder
      { path: 'notifications', component: DashboardComponent }, // Placeholder
      { path: 'profile', component: ProfileComponent },
      { path: 'posts', loadComponent: () => import('./components/posts-list/posts-list.component').then(m => m.PostsListComponent) },
      { path: 'settings', component: DashboardComponent } // Placeholder
    ]
  },
  { path: '**', redirectTo: '/dashboard' }
];
