export interface ProfileSummary {
  firstName: string;
  lastName: string;
  title: string;
  education: string;
  lastWorkPlace: string;
  city: string;
  profileImage?: string;
  coverImage?: string;
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
}

export interface Certificate {
  id: string;
  name: string;
  issuingOrganization: string;
  issueDate: string;
  expirationDate?: string;
  credentialId?: string;
  credentialUrl?: string;
}

export interface Address {
  city: string;
  district?: string;
  fullAddress?: string;
  postalCode?: string;
}

export interface ContactInfo {
  phoneNumber?: string;
  email: string;
  website?: string;
  linkedin?: string;
  github?: string;
}

export interface ProfileData {
  summary: ProfileSummary;
  experiences: Experience[];
  skills: Skill[];
  education: Education[];
  certificates: Certificate[];
  address: Address;
  contactInfo: ContactInfo;
}

