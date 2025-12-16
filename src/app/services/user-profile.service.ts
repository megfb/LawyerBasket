import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, UserProfileDto, UserType } from '../models/profile-api.models';

export interface CreateUserProfileRequest {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  genderId: string;
  birthDate?: string | null;
  nationalId?: string;
  userType: UserType;
}

export interface UpdateUserProfileRequest {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  genderId: string;
  birthDate?: string | null;
  nationalId?: string;
}

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/userprofile`;

  createUserProfile(request: CreateUserProfileRequest): Observable<ApiResult<UserProfileDto>> {
    return this.http.post<ApiResult<UserProfileDto>>(`${this.apiUrl}/CreateUserProfile`, request);
  }

  updateUserProfile(request: UpdateUserProfileRequest): Observable<ApiResult<UserProfileDto>> {
    return this.http.put<ApiResult<UserProfileDto>>(`${this.apiUrl}/UpdateUserProfile`, request);
  }

  getUserProfile(id: string): Observable<ApiResult<UserProfileDto>> {
    return this.http.get<ApiResult<UserProfileDto>>(`${this.apiUrl}/GetUserProfile/${id}`);
  }
}

