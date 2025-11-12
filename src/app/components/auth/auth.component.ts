import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { MessageModule } from 'primeng/message';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    PasswordModule,
    ButtonModule,
    CardModule,
    MessageModule
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  private authService = inject(AuthService);
  private profileService = inject(ProfileService);
  private router = inject(Router);

  email: string = '';
  password: string = '';
  isRegistering: boolean = false;
  errorMessage: string = '';
  isLoading: boolean = false;

  login() {
    if (!this.email || !this.password) {
      this.errorMessage = 'Lütfen email ve şifre alanlarını doldurun.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          // Token'ı localStorage'a kaydet
          localStorage.setItem('accessToken', response.data.accessToken);
          console.log('Giriş başarılı!', response.data);
          
          // Onboarding kontrolü yap
          this.checkOnboardingStatus();
        } else {
          this.isLoading = false;
          this.errorMessage = response.errorMessage?.join(', ') || 'Giriş başarısız.';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.errorMessage?.join(', ') || 'Bir hata oluştu. Lütfen tekrar deneyin.';
      }
    });
  }

  private checkOnboardingStatus(): void {
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.isSuccess && response.data) {
          const hasUserProfile = !!response.data.id;
          const hasLawyerProfile = !!response.data.lawyerProfile?.id;

          // Eğer UserProfile ve LawyerProfile varsa dashboard'a, yoksa onboarding'e yönlendir
          if (hasUserProfile && hasLawyerProfile) {
            this.router.navigate(['/dashboard']);
          } else {
            this.router.navigate(['/onboarding/welcome']);
          }
        } else {
          // Profil yoksa onboarding'e yönlendir
          this.router.navigate(['/onboarding/welcome']);
        }
      },
      error: () => {
        // Hata durumunda onboarding'e yönlendir (profil henüz oluşturulmamış olabilir)
        this.isLoading = false;
        this.router.navigate(['/onboarding/welcome']);
      }
    });
  }

  register() {
    if (!this.email || !this.password) {
      this.errorMessage = 'Lütfen email ve şifre alanlarını doldurun.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.register({ email: this.email, password: this.password }).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.isSuccess && response.data) {
          this.errorMessage = '';
          // Kayıt başarılı, login moduna geç
          this.isRegistering = false;
          alert('Kayıt başarılı! Lütfen giriş yapın.');
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Kayıt başarısız.';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.error?.errorMessage?.join(', ') || 'Bir hata oluştu. Lütfen tekrar deneyin.';
      }
    });
  }

  toggleMode() {
    this.isRegistering = !this.isRegistering;
    this.errorMessage = '';
    this.email = '';
    this.password = '';
  }
}

