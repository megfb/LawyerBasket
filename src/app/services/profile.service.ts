import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiResult, ProfileDto, UserProfileWDetailsDto, UserProfileDto } from '../models/profile-api.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/Profile`;

  getUserProfileFull(): Observable<ApiResult<UserProfileWDetailsDto>> {
    // Use GetUserProfileFull endpoint through gateway
    // Gateway returns ProfileDto, extract userProfile from it
    return this.http.get<ApiResult<ProfileDto>>(`${this.apiUrl}/GetUserProfileFull`).pipe(
      map(response => {
        console.log('Gateway response:', response); // Debug log
        if (response && response.isSuccess && response.data?.userProfile) {
          return {
            ...response,
            data: response.data.userProfile
          } as ApiResult<UserProfileWDetailsDto>;
        }
        console.warn('Response mapping failed:', { response, hasData: !!response?.data, hasUserProfile: !!response?.data?.userProfile }); // Debug log
        return {
          ...response,
          data: undefined
        } as ApiResult<UserProfileWDetailsDto>;
      })
    );
  }

  getProfileFull(): Observable<ApiResult<ProfileDto>> {
    // Get full profile with posts and commented posts
    return this.http.get<ApiResult<ProfileDto>>(`${this.apiUrl}/GetUserProfileFull`);
  }

  getUserProfilesByIds(userIds: string[]): Observable<ApiResult<UserProfileDto[]>> {
    return this.http.post<ApiResult<UserProfileDto[]>>(`${this.apiUrl}/GetUserProfilesByIds`, userIds);
  }

  deleteFriendship(friendshipId: string): Observable<ApiResult<void>> {
    return this.http.delete<ApiResult<void>>(`${this.apiUrl}/DeleteFriendship/${friendshipId}`);
  }
}

