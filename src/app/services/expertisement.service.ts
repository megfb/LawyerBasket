import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ApiResult, ExpertisementDto } from '../models/profile-api.models';

@Injectable({
  providedIn: 'root'
})
export class ExpertisementService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7141/api/Expertisement';

  // Şimdilik hardcoded liste - backend'de ExpertisementController yoksa bu kullanılacak
  private readonly hardcodedExpertisements: ExpertisementDto[] = [
    { id: 'e1a2b3c4-0001-4f5a-8c9d-1a2b3c4d5e6f', name: 'Ceza Hukuku', description: 'Criminal Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0002-4f5a-8c9d-1a2b3c4d5e6f', name: 'Ticaret / Şirketler Hukuku', description: 'Commercial / Corporate Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0003-4f5a-8c9d-1a2b3c4d5e6f', name: 'İş Hukuku', description: 'Labor / Employment Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0004-4f5a-8c9d-1a2b3c4d5e6f', name: 'Aile Hukuku', description: 'Family Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0005-4f5a-8c9d-1a2b3c4d5e6f', name: 'Gayrimenkul Hukuku', description: 'Real Estate Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0006-4f5a-8c9d-1a2b3c4d5e6f', name: 'Fikri Mülkiyet Hukuku', description: 'Intellectual Property Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0007-4f5a-8c9d-1a2b3c4d5e6f', name: 'Vergi Hukuku', description: 'Tax Law', createdAt: '2024-01-01' },
    { id: 'e1a2b3c4-0008-4f5a-8c9d-1a2b3c4d5e6f', name: 'İdare Hukuku', description: 'Administrative Law', createdAt: '2024-01-01' }
  ];

  getExpertisements(): Observable<ApiResult<ExpertisementDto[]>> {
    // Backend'de endpoint varsa kullan, yoksa hardcoded listeyi döndür
    // TODO: Backend'de ExpertisementController oluşturulduğunda bu kısmı güncelle
    return of({
      isSuccess: true,
      isFail: false,
      status: 200,
      data: this.hardcodedExpertisements
    } as ApiResult<ExpertisementDto[]>);
  }
}

