import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, LawyerProfileDto } from '../models/profile-api.models';

export interface CreateLawyerProfileRequest {
  userProfileId: string;
  barAssociation: string;
  barNumber: string;
  licenseNumber: string;
  licenseDate: string; // ISO string format
}

export interface UpdateLawyerProfileRequest {
  id: string;
  barAssociation: string;
  barNumber: string;
  licenseNumber: string;
  licenseDate: string; // ISO string format
}

@Injectable({
  providedIn: 'root'
})
export class LawyerProfileService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7141/api/LawyerProfile';

  createLawyerProfile(request: CreateLawyerProfileRequest): Observable<ApiResult<LawyerProfileDto>> {
    return this.http.post<ApiResult<LawyerProfileDto>>(`${this.apiUrl}/CreateLawyerProfile`, request);
  }

  updateLawyerProfile(id: string, request: UpdateLawyerProfileRequest): Observable<ApiResult<LawyerProfileDto>> {
    return this.http.put<ApiResult<LawyerProfileDto>>(`${this.apiUrl}/UpdateLawyerProfile/${id}`, request);
  }

  getLawyerProfile(): Observable<ApiResult<LawyerProfileDto>> {
    return this.http.get<ApiResult<LawyerProfileDto>>(`${this.apiUrl}/GetLawyerProfile`);
  }
}

