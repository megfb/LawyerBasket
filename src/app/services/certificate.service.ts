import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, CertificatesDto } from '../models/profile-api.models';

export interface CreateCertificateRequest {
  lawyerProfileId: string;
  name: string;
  institution: string;
  dateReceived: string; // ISO string format
  description?: string;
}

export interface UpdateCertificateRequest {
  id: string;
  name: string;
  institution: string;
  dateReceived: string; // ISO string format
  description?: string;
}

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CertificateService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/certificate`;

  createCertificate(request: CreateCertificateRequest): Observable<ApiResult<CertificatesDto>> {
    return this.http.post<ApiResult<CertificatesDto>>(`${this.apiUrl}/CreateCertificate`, request);
  }

  updateCertificate(id: string, request: UpdateCertificateRequest): Observable<ApiResult<CertificatesDto>> {
    return this.http.put<ApiResult<CertificatesDto>>(`${this.apiUrl}/UpdateCertificate/${id}`, request);
  }

  deleteCertificate(id: string): Observable<ApiResult<any>> {
    return this.http.delete<ApiResult<any>>(`${this.apiUrl}/RemoveCertificate/${id}`);
  }

  getCertificate(id: string): Observable<ApiResult<CertificatesDto>> {
    return this.http.get<ApiResult<CertificatesDto>>(`${this.apiUrl}/GetCertificate/${id}`);
  }

  getCertificates(lawyerProfileId: string): Observable<ApiResult<CertificatesDto[]>> {
    return this.http.get<ApiResult<CertificatesDto[]>>(`${this.apiUrl}/GetCertificates/${lawyerProfileId}`);
  }
}

