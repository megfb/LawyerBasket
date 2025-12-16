import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, AcademyDto } from '../models/profile-api.models';

export interface CreateAcademyRequest {
  lawyerProfileId: string;
  university: string;
  degree?: string;
  department?: string;
  startDate: string; // ISO string format
  endDate?: string | null; // ISO string format
}

export interface UpdateAcademyRequest {
  id: string;
  university: string;
  degree?: string;
  department?: string;
  startDate: string; // ISO string format
  endDate?: string | null; // ISO string format
}

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AcademyService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/academy`;

  createAcademy(request: CreateAcademyRequest): Observable<ApiResult<AcademyDto>> {
    return this.http.post<ApiResult<AcademyDto>>(`${this.apiUrl}/CreateAcademy`, request);
  }

  updateAcademy(id: string, request: UpdateAcademyRequest): Observable<ApiResult<AcademyDto>> {
    return this.http.put<ApiResult<AcademyDto>>(`${this.apiUrl}/UpdateAcademy/${id}`, request);
  }

  deleteAcademy(id: string): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/RemoveAcademy/${id}`);
  }

  getAcademy(id: string): Observable<ApiResult<AcademyDto>> {
    return this.http.get<ApiResult<AcademyDto>>(`${this.apiUrl}/GetAcademy/${id}`);
  }

  getAcademies(lawyerProfileId: string): Observable<ApiResult<AcademyDto[]>> {
    return this.http.get<ApiResult<AcademyDto[]>>(`${this.apiUrl}/GetAcademies/${lawyerProfileId}`);
  }
}

