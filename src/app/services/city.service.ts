import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, CityDto } from '../models/profile-api.models';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7001/api/city'; // Gateway API URL

  getCities(): Observable<ApiResult<CityDto[]>> {
    return this.http.get<ApiResult<CityDto[]>>(`${this.apiUrl}/GetCities`);
  }
}

