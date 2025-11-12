import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, UserProfileWDetailsDto } from '../models/profile-api.models';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7141/api/UserProfile'; // ProfileService API URL

  getUserProfileFull(): Observable<ApiResult<UserProfileWDetailsDto>> {
    return this.http.get<ApiResult<UserProfileWDetailsDto>>(`${this.apiUrl}/GetUserProfileFull`);
  }
}

