import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, LawyerExpertisementDto } from '../models/profile-api.models';

export interface CreateLawyerExpertisementRequest {
  lawyerProfileId: string;
  expertisementIds: string[];
}

@Injectable({
  providedIn: 'root'
})
export class LawyerExpertisementService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7001/api/lawyerexpertisement'; // Gateway API URL

  createLawyerExpertisement(request: CreateLawyerExpertisementRequest): Observable<ApiResult<LawyerExpertisementDto[]>> {
    return this.http.post<ApiResult<LawyerExpertisementDto[]>>(`${this.apiUrl}/CreateLawyerExpertisement`, request);
  }

  getLawyerExpertisements(lawyerProfileId: string): Observable<ApiResult<LawyerExpertisementDto[]>> {
    return this.http.get<ApiResult<LawyerExpertisementDto[]>>(`${this.apiUrl}/GetLawyerExpertisements/${lawyerProfileId}`);
  }

  removeLawyerExpertisement(id: string): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/RemoveLawyerExpertisement/${id}`);
  }
}

