export interface ProfileSummary {
  firstName: string;
  lastName: string;
  title: string;
  education: string;
  lastWorkPlace: string;
  city: string;
  profileImage?: string;
  coverImage?: string;
  barAssociation?: string;
  barNumber?: string;
  about?: string;
}

export interface Experience {
  id: string;
  companyName: string;
  position: string;
  startDate: string;
  endDate?: string;
  description?: string;
  isCurrent: boolean;
}

export interface Skill {
  id: string;
  name: string;
  level?: string; // Beginner, Intermediate, Advanced, Expert
}

export interface Education {
  id: string;
  schoolName: string;
  department: string;
  graduationYear?: number;
  degree?: string; // Bachelor, Master, PhD
  startDate?: string; // Format: "YYYY-MM"
  endDate?: string; // Format: "YYYY-MM" or null if still studying
}

export interface Certificate {
  id: string;
  name: string;
  issuingOrganization: string;
  issueDate: string;
  description?: string;
  expirationDate?: string;
  credentialId?: string;
  credentialUrl?: string;
}

export interface Address {
  id?: string;
  userProfileId?: string;
  city: string;
  cityId?: string;
  district?: string;
  fullAddress?: string;
  addressLine?: string;
  postalCode?: string;
}

export interface ContactInfo {
  id?: string;
  lawyerProfileId?: string;
  phoneNumber?: string;
  alternatePhoneNumber?: string;
  email: string;
  alternateEmail?: string;
  website?: string;
  linkedin?: string; // Frontend only
  github?: string; // Frontend only
}

export interface LawyerInfo {
  barAssociation?: string;
  barNumber?: string;
  licenseNumber?: string;
  licenseDate?: string;
}

export interface ProfileData {
  summary: ProfileSummary;
  experiences: Experience[];
  skills: Skill[];
  education: Education[];
  certificates: Certificate[];
  address: Address;
  contactInfo: ContactInfo;
  lawyerInfo?: LawyerInfo;
}

