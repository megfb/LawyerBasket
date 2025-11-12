import { Component, Input, Output, EventEmitter, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Skill } from '../../../models/profile.models';
import { LawyerExpertisementService } from '../../../services/lawyer-expertisement.service';
import { ExpertisementService } from '../../../services/expertisement.service';

interface ExpertisementOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-profile-skills',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    CardModule,
    TagModule,
    ButtonModule,
    DialogModule,
    MultiSelectModule,
    ConfirmDialogModule,
    ToastModule
  ],
  providers: [ConfirmationService, MessageService],
  templateUrl: './profile-skills.component.html',
  styleUrl: './profile-skills.component.css'
})
export class ProfileSkillsComponent {
  @Input() skills: Skill[] = [];
  @Input() lawyerProfileId: string | null = null;
  @Output() skillDeleted = new EventEmitter<void>();

  private lawyerExpertisementService = inject(LawyerExpertisementService);
  private expertisementService = inject(ExpertisementService);
  private confirmationService = inject(ConfirmationService);
  private messageService = inject(MessageService);

  showAddModal = false;
  availableExpertisements: ExpertisementOption[] = [];
  selectedExpertisementIds: string[] = [];
  isSubmitting = false;

  getSeverity(level?: string): 'success' | 'info' | 'warn' | 'secondary' | 'contrast' | 'danger' | null {
    if (!level) return 'info';
    switch (level.toLowerCase()) {
      case 'expert':
        return 'success';
      case 'advanced':
        return 'info';
      case 'intermediate':
        return 'warn';
      case 'beginner':
        return 'secondary';
      default:
        return 'info';
    }
  }

  onDeleteSkill(event: Event, skill: Skill): void {
    event.stopPropagation();
    
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `"${skill.name}" uzmanlık alanını silmek istediğinizden emin misiniz?`,
      icon: 'pi pi-exclamation-triangle',
      acceptButtonStyleClass: 'p-button-danger',
      acceptLabel: 'Evet, Sil',
      rejectLabel: 'İptal',
      accept: () => {
        this.deleteSkill(skill.id);
      }
    });
  }

  onAddSkill(): void {
    if (!this.lawyerProfileId) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Uyarı',
        detail: 'Avukat profili bulunamadı.'
      });
      return;
    }

    this.showAddModal = true;
    this.loadAvailableExpertisements();
  }

  private loadAvailableExpertisements(): void {
    this.expertisementService.getExpertisements().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          // Mevcut uzmanlıkları filtrele
          const existingExpertisementIds = this.skills.map(skill => {
            // Skill'den expertisement ID'sini bulmak için backend'den gelen veriyi kullanmalıyız
            // Şimdilik tüm expertisement'leri göster, backend'de duplicate kontrolü yapılacak
            return null;
          });
          
          this.availableExpertisements = response.data.map(exp => ({
            id: exp.id,
            name: exp.name
          }));
        }
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: 'Uzmanlık alanları yüklenirken bir hata oluştu.'
        });
      }
    });
  }

  onSaveExpertisements(): void {
    if (!this.lawyerProfileId) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Uyarı',
        detail: 'Avukat profili bulunamadı.'
      });
      return;
    }

    if (!this.selectedExpertisementIds || this.selectedExpertisementIds.length === 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Uyarı',
        detail: 'Lütfen en az bir uzmanlık alanı seçiniz.'
      });
      return;
    }

    this.isSubmitting = true;
    const request = {
      lawyerProfileId: this.lawyerProfileId,
      expertisementIds: this.selectedExpertisementIds
    };

    this.lawyerExpertisementService.createLawyerExpertisement(request).subscribe({
      next: (response) => {
        this.isSubmitting = false;
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: `${this.selectedExpertisementIds.length} uzmanlık alanı başarıyla eklendi.`
          });
          this.showAddModal = false;
          this.selectedExpertisementIds = [];
          this.skillDeleted.emit();
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response?.errorMessage?.join(', ') || 'Uzmanlık alanları eklenirken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        this.isSubmitting = false;
        console.error('Uzmanlık alanları eklenirken hata oluştu:', error);
        this.messageService.add({
          severity: 'error',
          summary: 'Hata',
          detail: error.error?.errorMessage?.join(', ') || 'Uzmanlık alanları eklenirken bir hata oluştu.'
        });
      }
    });
  }

  onCancelAdd(): void {
    this.showAddModal = false;
    this.selectedExpertisementIds = [];
  }

  private deleteSkill(skillId: string): void {
    this.lawyerExpertisementService.removeLawyerExpertisement(skillId).subscribe({
      next: (response) => {
        // Backend ApiResult.Success döndürüyor, isSuccess: true olmalı
        if (response && (response.isSuccess === true || response.isSuccess === undefined)) {
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Uzmanlık alanı başarıyla silindi.'
          });
          this.skillDeleted.emit();
        } else {
          // Backend'den hata mesajı geldi
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: response?.errorMessage?.join(', ') || 'Uzmanlık alanı silinirken bir hata oluştu.'
          });
        }
      },
      error: (error) => {
        console.error('Uzmanlık alanı silinirken hata oluştu:', error);
        // HTTP 200 OK veya 204 No Content durumunda bile error callback'e düşebilir (Angular HttpClient davranışı)
        // Bu durumda response body'yi kontrol edelim
        if (error.status === 200 || error.status === 204) {
          // Başarılı silme (200 OK veya 204 No Content)
          this.messageService.add({
            severity: 'success',
            summary: 'Başarılı',
            detail: 'Uzmanlık alanı başarıyla silindi.'
          });
          this.skillDeleted.emit();
        } else {
          // Gerçek bir hata durumu
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: error.error?.errorMessage?.join(', ') || 'Uzmanlık alanı silinirken bir hata oluştu.'
          });
        }
      }
    });
  }
}

