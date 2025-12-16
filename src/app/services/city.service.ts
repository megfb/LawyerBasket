import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, CityDto } from '../models/profile-api.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/city`;

  getCities(): Observable<ApiResult<CityDto[]>> {
    return this.http.get<ApiResult<CityDto[]>>(`${this.apiUrl}/GetCities`);
  }
}

