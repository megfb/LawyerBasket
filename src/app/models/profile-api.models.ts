// Backend API Response Models
import { PostDto } from './post.models';

export interface ApiResult<T> {
  data?: T;
  errorMessage?: string[];
  isSuccess: boolean;
  isFail: boolean;
  status: number;
}

export enum UserType {
  Undefined = 0,
  Lawyer = 1,
  Client = 2
}

export interface GenderDto {
  id: string;
  name: string;
  description: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CityDto {
  id: string;
  name: string;
  createdAt: string;
  updatedAt?: string;
}

export interface AddressDto {
  id: string;
  userProfileId: string;
  addressLine: string;
  city: CityDto;
  cityId: string;
  createdAt: string;
  updatedAt?: string;
}

export interface ExpertisementDto {
  id: string;
  name: string;
  description: string;
  createdAt: string;
  updatedAt?: string;
}

export interface LawyerExpertisementDto {
  id: string;
  lawyerProfileId: string;
  expertisementId: string;
  expertisement: ExpertisementDto;
  createdAt: string;
  updatedAt?: string;
}

export interface ExperienceDto {
  id: string;
  lawyerProfileId: string;
  companyName: string;
  position: string;
  startDate: string;
  endDate?: string;
  description: string;
  createdAt: string;
  updatedAt?: string;
}

export interface AcademyDto {
  id: string;
  lawyerProfileId: string;
  university: string;
  degree?: string;
  department?: string;
  startDate: string;
  endDate?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CertificatesDto {
  id: string;
  lawyerProfileId: string;
  name: string;
  institution: string;
  dateReceived: string;
  description?: string;
  createdAt: string;
  updatedAt: string;
}

export interface ContactDto {
  id: string;
  lawyerProfileId: string;
  phoneNumber: string;
  alternatePhoneNumber?: string;
  email: string;
  alternateEmail?: string;
  website?: string;
  createdAt: string;
  updatedAt: string;
}

export interface UserProfileDto {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  genderId: string;
  birthDate?: string;
  nationalId?: string;
  userType: UserType;
  createdAt: string;
  updatedAt?: string;
}

export interface LawyerProfileDto {
  id: string;
  userProfileId: string;
  barAssociation: string;
  barNumber: string;
  licenseNumber: string;
  licenseDate: string;
  about?: string;
  lawyerExpertisements?: LawyerExpertisementDto[];
  experience?: ExperienceDto[];
  academy?: AcademyDto[];
  certificates?: CertificatesDto[];
  contact?: ContactDto;
  createdAt: string;
  updatedAt: string;
}

export interface UserProfileWDetailsDto {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  gender: GenderDto;
  genderId: string;
  birthDate?: string;
  nationalId?: string;
  address?: AddressDto;
  userType: UserType;
  lawyerProfile?: LawyerProfileDto;
  createdAt: string;
  updatedAt?: string;
}

export interface FriendWithProfileDto {
  friendshipId: string;
  friendUserId: string;
  friendshipCreatedAt: string;
  profile?: UserProfileDto;
}

// Gateway ProfileDto (aggregation)
export interface ProfileDto {
  userProfile?: UserProfileWDetailsDto;
  posts?: PostDto[];
  commentedPosts?: PostDto[];
  likedPosts?: PostDto[];
  friends?: FriendWithProfileDto[];
}

