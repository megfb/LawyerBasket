import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginRequest, RegisterRequest, ApiResult, TokenDto, AppUserDto } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private apiUrl = 'https://localhost:7299/api/Auth'; // AuthService API URL

  login(loginRequest: LoginRequest): Observable<ApiResult<TokenDto>> {
    return this.http.post<ApiResult<TokenDto>>(`${this.apiUrl}/Login`, loginRequest);
  }

  register(registerRequest: RegisterRequest): Observable<ApiResult<AppUserDto>> {
    return this.http.post<ApiResult<AppUserDto>>(`${this.apiUrl}/Register`, registerRequest);
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    this.router.navigate(['/auth']);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('accessToken');
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }
}

