import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, ExperienceDto } from '../models/profile-api.models';

export interface CreateExperienceRequest {
  lawyerProfileId: string;
  companyName: string;
  position: string;
  startDate: string; // ISO string format
  endDate?: string | null; // ISO string format
  description: string;
}

export interface UpdateExperienceRequest {
  id: string;
  companyName: string;
  position: string;
  startDate: string; // ISO string format
  endDate?: string | null; // ISO string format
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class ExperienceService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7001/api/experience'; // Gateway API URL

  updateExperience(id: string, request: UpdateExperienceRequest): Observable<ApiResult<ExperienceDto>> {
    return this.http.put<ApiResult<ExperienceDto>>(`${this.apiUrl}/UpdateExperience/${id}`, request);
  }

  deleteExperience(id: string): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/RemoveExperience/${id}`);
  }

  createExperience(request: CreateExperienceRequest): Observable<ApiResult<ExperienceDto>> {
    return this.http.post<ApiResult<ExperienceDto>>(`${this.apiUrl}/CreateExperience`, request);
  }
}

