import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResult } from '../models/auth.models';
import { Experience, Education, Certificate, Address, ContactInfo } from '../models/profile.models';
import { AuthService } from './auth.service';

export interface ExperienceDto {
  id: string;
  lawyerProfileId: string;
  companyName: string;
  position: string;
  startDate: string;
  endDate?: string | null;
  description: string;
  createdAt: string;
  updatedAt?: string | null;
}

export interface LawyerProfileDto {
  id: string;
  userProfileId: string;
  barAssociation: string;
  barNumber: string;
  licenseNumber: string;
  licenseDate: string;
  contact?: ContactDto | null;
  createdAt: string;
  updatedAt: string;
}

export interface AcademyDto {
  id: string;
  lawyerProfileId: string;
  university: string;
  degree?: string | null;
  startDate: string;
  endDate?: string | null;
  createdAt: string;
  updatedAt?: string | null;
}

export interface CertificatesDto {
  id: string;
  lawyerProfileId: string;
  name: string;
  institution: string;
  dateReceived: string;
  createdAt: string;
  updatedAt: string;
}

export interface CityDto {
  name: string;
  addressDto?: AddressDto[];
}

export interface AddressDto {
  id: string;
  userProfileId: string;
  addressLine: string;
  cityDto: CityDto;
  cityId: string;
}

export interface ContactDto {
  id: string;
  lawyerProfileId: string;
  phoneNumber: string;
  alternatePhoneNumber?: string | null;
  email: string;
  alternateEmail?: string | null;
  website?: string | null;
  createdAt: string;
  updatedAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private http = inject(HttpClient);
  private authService = inject(AuthService);
  private apiUrl = 'https://localhost:7141/api'; // ProfileService API URL

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    let headers = new HttpHeaders();
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
  }

  getLawyerProfile(): Observable<ApiResult<LawyerProfileDto>> {
    return this.http.get<ApiResult<LawyerProfileDto>>(
      `${this.apiUrl}/LawyerProfile/GetLawyerProfile`,
      { headers: this.getHeaders() }
    );
  }

  getExperiences(lawyerProfileId: string): Observable<ApiResult<ExperienceDto[]>> {
    return this.http.get<ApiResult<ExperienceDto[]>>(
      `${this.apiUrl}/Experience/GetExperiences/${lawyerProfileId}`,
      { headers: this.getHeaders() }
    );
  }

  getExperiencesMapped(lawyerProfileId: string): Observable<Experience[]> {
    return this.getExperiences(lawyerProfileId).pipe(
      map(response => {
        if (response.isSuccess && response.data) {
          return response.data.map(exp => this.mapExperienceDtoToExperience(exp));
        }
        return [];
      })
    );
  }

  getAcademies(lawyerProfileId: string): Observable<ApiResult<AcademyDto[]>> {
    return this.http.get<ApiResult<AcademyDto[]>>(
      `${this.apiUrl}/Academy/GetAcademies/${lawyerProfileId}`,
      { headers: this.getHeaders() }
    );
  }

  getAcademiesMapped(lawyerProfileId: string): Observable<Education[]> {
    return this.getAcademies(lawyerProfileId).pipe(
      map(response => {
        if (response.isSuccess && response.data) {
          return response.data.map(academy => this.mapAcademyDtoToEducation(academy));
        }
        return [];
      })
    );
  }

  getCertificates(lawyerProfileId: string): Observable<ApiResult<CertificatesDto[]>> {
    return this.http.get<ApiResult<CertificatesDto[]>>(
      `${this.apiUrl}/Certificate/GetCertificates/${lawyerProfileId}`,
      { headers: this.getHeaders() }
    );
  }

  getCertificatesMapped(lawyerProfileId: string): Observable<Certificate[]> {
    return this.getCertificates(lawyerProfileId).pipe(
      map(response => {
        if (response.isSuccess && response.data) {
          return response.data.map(cert => this.mapCertificatesDtoToCertificate(cert));
        }
        return [];
      })
    );
  }

  getAddress(): Observable<ApiResult<AddressDto>> {
    return this.http.get<ApiResult<AddressDto>>(
      `${this.apiUrl}/Address/GetAddress`,
      { headers: this.getHeaders() }
    );
  }

  getAddressMapped(): Observable<Address | null> {
    return this.getAddress().pipe(
      map(response => {
        if (response.isSuccess && response.data) {
          return this.mapAddressDtoToAddress(response.data);
        }
        return null;
      })
    );
  }

  getContact(contactId: string): Observable<ApiResult<ContactDto>> {
    return this.http.get<ApiResult<ContactDto>>(
      `${this.apiUrl}/Contact/GetContact/${contactId}`,
      { headers: this.getHeaders() }
    );
  }

  getContactMapped(contactId: string): Observable<ContactInfo | null> {
    return this.getContact(contactId).pipe(
      map(response => {
        if (response.isSuccess && response.data) {
          return this.mapContactDtoToContactInfo(response.data);
        }
        return null;
      })
    );
  }

  private mapExperienceDtoToExperience(dto: ExperienceDto): Experience {
    const startDate = new Date(dto.startDate);
    const endDate = dto.endDate ? new Date(dto.endDate) : null;
    
    return {
      id: dto.id,
      companyName: dto.companyName,
      position: dto.position,
      startDate: `${startDate.getFullYear()}-${String(startDate.getMonth() + 1).padStart(2, '0')}`,
      endDate: endDate ? `${endDate.getFullYear()}-${String(endDate.getMonth() + 1).padStart(2, '0')}` : undefined,
      description: dto.description,
      isCurrent: !dto.endDate
    };
  }

  private mapAcademyDtoToEducation(dto: AcademyDto): Education {
    const endDate = dto.endDate ? new Date(dto.endDate) : null;
    
    return {
      id: dto.id,
      schoolName: dto.university,
      department: '', // Backend'de department alanı yok, boş bırakıyoruz
      graduationYear: endDate ? endDate.getFullYear() : undefined,
      degree: dto.degree || undefined
    };
  }

  private mapCertificatesDtoToCertificate(dto: CertificatesDto): Certificate {
    const dateReceived = new Date(dto.dateReceived);
    
    return {
      id: dto.id,
      name: dto.name,
      issuingOrganization: dto.institution,
      issueDate: `${dateReceived.getFullYear()}-${String(dateReceived.getMonth() + 1).padStart(2, '0')}`,
      expirationDate: undefined, // Backend'de expirationDate alanı yok
      credentialId: undefined, // Backend'de credentialId alanı yok
      credentialUrl: undefined // Backend'de credentialUrl alanı yok
    };
  }

  private mapAddressDtoToAddress(dto: AddressDto): Address {
    return {
      city: dto.cityDto?.name || '',
      district: undefined, // Backend'de district alanı yok
      fullAddress: dto.addressLine || undefined,
      postalCode: undefined // Backend'de postalCode alanı yok
    };
  }

  private mapContactDtoToContactInfo(dto: ContactDto): ContactInfo {
    return {
      phoneNumber: dto.phoneNumber || undefined,
      email: dto.email,
      website: dto.website || undefined,
      linkedin: undefined, // Backend'de linkedin alanı yok
      github: undefined // Backend'de github alanı yok
    };
  }
}

