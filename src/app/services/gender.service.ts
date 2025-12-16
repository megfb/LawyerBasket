import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult } from '../models/profile-api.models';

export interface GenderDto {
  id: string;
  name: string;
  description: string;
  createdAt: string;
  updatedAt?: string;
}

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GenderService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/gender`;

  getGenders(): Observable<ApiResult<GenderDto[]>> {
    return this.http.get<ApiResult<GenderDto[]>>(`${this.apiUrl}/GetGenders`);
  }
}

