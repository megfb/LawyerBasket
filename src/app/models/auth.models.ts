export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
}

export interface TokenDto {
  accessToken: string;
  accessTokenExpiration: string;
}

export interface AppUserDto {
  id: string;
  email: string;
  passwordHash: string;
  lastLoginAt: string | null;
  createdAt: string;
  updatedAt: string | null;
  appUserRoleDto: any[] | null;
}

export interface ApiResult<T> {
  data?: T;
  errorMessage?: string[];
  isSuccess: boolean;
  isFail: boolean;
  status: number;
}

