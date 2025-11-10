import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ProfileSummaryComponent } from './profile-summary/profile-summary.component';
import { ProfileTabsComponent } from './profile-tabs/profile-tabs.component';
import { ProfileExperiencesComponent } from './profile-experiences/profile-experiences.component';
import { ProfileSkillsComponent } from './profile-skills/profile-skills.component';
import { ProfileEducationComponent } from './profile-education/profile-education.component';
import { ProfileCertificatesComponent } from './profile-certificates/profile-certificates.component';
import { ProfileAddressComponent } from './profile-address/profile-address.component';
import { ProfileContactComponent } from './profile-contact/profile-contact.component';
import { ProfileData, Experience, Education, Certificate, Address, ContactInfo } from '../../models/profile.models';
import { ProfileService } from '../../services/profile.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ProfileSummaryComponent,
    ProfileTabsComponent,
    ProfileExperiencesComponent,
    ProfileSkillsComponent,
    ProfileEducationComponent,
    ProfileCertificatesComponent,
    ProfileAddressComponent,
    ProfileContactComponent
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  private profileService = inject(ProfileService);
  
  profileData: ProfileData = {
    summary: {
      firstName: 'Ahmet',
      lastName: 'Yılmaz',
      title: 'Yazılım Geliştirici',
      education: 'Bilgisayar Mühendisliği - İstanbul Üniversitesi',
      lastWorkPlace: 'ABC Teknoloji A.Ş.',
      city: 'İstanbul',
      profileImage: '/img/profilephoto.jpg',
      coverImage: '/img/cover.jpg'
    },
    experiences: [],
    skills: [
      { id: '1', name: '.NET', level: 'Expert' },
      { id: '2', name: 'Angular', level: 'Advanced' },
      { id: '3', name: 'SQL', level: 'Advanced' },
      { id: '4', name: 'TypeScript', level: 'Advanced' },
      { id: '5', name: 'C#', level: 'Expert' },
      { id: '6', name: 'JavaScript', level: 'Advanced' }
    ],
    education: [],
    certificates: [],
    address: {
      city: '',
      district: undefined,
      fullAddress: undefined,
      postalCode: undefined
    },
    contactInfo: {
      phoneNumber: undefined,
      email: '',
      website: undefined,
      linkedin: undefined,
      github: undefined
    }
  };

  ngOnInit(): void {
    this.loadProfileData();
  }

  loadProfileData(): void {
    this.profileService.getLawyerProfile().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const lawyerProfileId = response.data.id;
          this.loadExperiences(lawyerProfileId);
          this.loadEducation(lawyerProfileId);
          this.loadCertificates(lawyerProfileId);
          
          // Contact bilgisi LawyerProfile içinde geliyorsa onu kullan
          if (response.data.contact?.id) {
            this.loadContact(response.data.contact.id);
          }
        }
      },
      error: (error) => {
        console.error('Lawyer profile yüklenirken hata oluştu:', error);
      }
    });
    this.loadAddress();
  }

  loadExperiences(lawyerProfileId: string): void {
    this.profileService.getExperiencesMapped(lawyerProfileId).subscribe({
      next: (experiences: Experience[]) => {
        this.profileData.experiences = experiences;
      },
      error: (error) => {
        console.error('Deneyimler yüklenirken hata oluştu:', error);
      }
    });
  }

  loadEducation(lawyerProfileId: string): void {
    this.profileService.getAcademiesMapped(lawyerProfileId).subscribe({
      next: (education: Education[]) => {
        this.profileData.education = education;
      },
      error: (error) => {
        console.error('Eğitim bilgileri yüklenirken hata oluştu:', error);
      }
    });
  }

  loadCertificates(lawyerProfileId: string): void {
    this.profileService.getCertificatesMapped(lawyerProfileId).subscribe({
      next: (certificates: Certificate[]) => {
        this.profileData.certificates = certificates;
      },
      error: (error) => {
        console.error('Sertifikalar yüklenirken hata oluştu:', error);
      }
    });
  }

  loadAddress(): void {
    this.profileService.getAddressMapped().subscribe({
      next: (address: Address | null) => {
        if (address) {
          this.profileData.address = address;
        }
      },
      error: (error) => {
        console.error('Adres bilgileri yüklenirken hata oluştu:', error);
      }
    });
  }

  loadContact(contactId: string): void {
    this.profileService.getContactMapped(contactId).subscribe({
      next: (contactInfo: ContactInfo | null) => {
        if (contactInfo) {
          this.profileData.contactInfo = contactInfo;
        }
      },
      error: (error) => {
        console.error('İletişim bilgileri yüklenirken hata oluştu:', error);
      }
    });
  }
}

