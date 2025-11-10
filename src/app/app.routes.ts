import { Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { LayoutComponent } from './components/layout/layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ProfileComponent } from './components/profile/profile.component';
import { authGuard } from './guards/auth.guard';
import { authRedirectGuard } from './guards/auth-redirect.guard';

export const routes: Routes = [
  { path: 'auth', component: AuthComponent, canActivate: [authRedirectGuard] },
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
      { path: 'settings', component: DashboardComponent } // Placeholder
    ]
  },
  { path: '**', redirectTo: '/dashboard' }
];
