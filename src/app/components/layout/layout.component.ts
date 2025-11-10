import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    InputTextModule,
    ButtonModule,
    MenuModule
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  searchQuery: string = '';
  userEmail: string = 'user@example.com'; // TODO: Get from token or user service

  menuItems = [
    { label: 'Anasayfa', icon: 'pi pi-home', route: '/dashboard' },
    { label: 'İş İlanları', icon: 'pi pi-briefcase', route: '/jobs' },
    { label: 'Mesajlaşma', icon: 'pi pi-comments', route: '/messages' },
    { label: 'Bildirimler', icon: 'pi pi-bell', route: '/notifications' }
  ];

  get profileMenuItems() {
    return [
      {
        label: 'Profili Görüntüle',
        icon: 'pi pi-user',
        command: () => {
          this.viewProfile();
        }
      },
      {
        label: 'Ayarlar',
        icon: 'pi pi-cog',
        command: () => {
          this.openSettings();
        }
      },
      {
        label: 'Oturumu Kapat',
        icon: 'pi pi-sign-out',
        command: () => {
          this.logout();
        }
      }
    ];
  }

  viewProfile(): void {
    this.router.navigate(['/profile']);
  }

  openSettings(): void {
    this.router.navigate(['/settings']);
  }

  logout(): void {
    this.authService.logout();
  }

  onSearch(): void {
    if (this.searchQuery.trim()) {
      // TODO: Implement search functionality
      console.log('Searching for:', this.searchQuery);
    }
  }

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
}

