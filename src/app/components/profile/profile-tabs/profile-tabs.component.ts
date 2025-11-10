import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-profile-tabs',
  standalone: true,
  imports: [CommonModule, CardModule],
  templateUrl: './profile-tabs.component.html',
  styleUrl: './profile-tabs.component.css'
})
export class ProfileTabsComponent {
  activeTab: string = 'posts';

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }
}

