import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, ContactDto } from '../models/profile-api.models';

export interface CreateContactRequest {
  lawyerProfileId: string;
  phoneNumber: string;
  alternatePhoneNumber?: string | null;
  email: string;
  alternateEmail?: string | null;
  website?: string | null;
}

export interface UpdateContactRequest {
  id: string;
  phoneNumber: string;
  alternatePhoneNumber?: string | null;
  email: string;
  alternateEmail?: string | null;
  website?: string | null;
  updatedAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7001/api/contact'; // Gateway API URL

  createContact(request: CreateContactRequest): Observable<ApiResult<ContactDto>> {
    return this.http.post<ApiResult<ContactDto>>(`${this.apiUrl}/CreateContact`, request);
  }

  updateContact(id: string, request: UpdateContactRequest): Observable<ApiResult<ContactDto>> {
    return this.http.put<ApiResult<ContactDto>>(`${this.apiUrl}/UpdateContact/${id}`, request);
  }

  getContact(id: string): Observable<ApiResult<ContactDto>> {
    return this.http.get<ApiResult<ContactDto>>(`${this.apiUrl}/GetContact/${id}`);
  }
}

