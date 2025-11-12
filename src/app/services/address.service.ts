import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResult, AddressDto } from '../models/profile-api.models';

export interface CreateAddressRequest {
  userProfileId: string;
  addressLine: string;
  cityId: string;
}

export interface UpdateAddressRequest {
  id: string;
  addressLine: string;
  cityId: string;
}

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7141/api/Address'; // Address API URL

  createAddress(request: CreateAddressRequest): Observable<ApiResult<AddressDto>> {
    return this.http.post<ApiResult<AddressDto>>(`${this.apiUrl}/CreateAddress`, request);
  }

  updateAddress(id: string, request: UpdateAddressRequest): Observable<ApiResult<AddressDto>> {
    return this.http.put<ApiResult<AddressDto>>(`${this.apiUrl}/UpdateAddress/${id}`, request);
  }

  getAddress(): Observable<ApiResult<AddressDto>> {
    return this.http.get<ApiResult<AddressDto>>(`${this.apiUrl}/GetAddress`);
  }
}

