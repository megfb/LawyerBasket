import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, ExpertisementDto } from '../models/profile-api.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpertisementService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/expertisement`;

  getExpertisements(): Observable<ApiResult<ExpertisementDto[]>> {
    return this.http.get<ApiResult<ExpertisementDto[]>>(`${this.apiUrl}/GetExpertisements`);
  }
}

