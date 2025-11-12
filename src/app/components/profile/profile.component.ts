import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MessageModule } from 'primeng/message';
import { ButtonModule } from 'primeng/button';
import { ProfileSummaryComponent } from './profile-summary/profile-summary.component';
import { ProfileTabsComponent } from './profile-tabs/profile-tabs.component';
import { ProfileExperiencesComponent } from './profile-experiences/profile-experiences.component';
import { ProfileSkillsComponent } from './profile-skills/profile-skills.component';
import { ProfileEducationComponent } from './profile-education/profile-education.component';
import { ProfileCertificatesComponent } from './profile-certificates/profile-certificates.component';
import { ProfileAddressComponent } from './profile-address/profile-address.component';
import { ProfileContactComponent } from './profile-contact/profile-contact.component';
import { ProfileData, ProfileSummary, Experience, Education, Certificate, Skill, Address, ContactInfo } from '../../models/profile.models';
import { ProfileService } from '../../services/profile.service';
import { UserProfileWDetailsDto } from '../../models/profile-api.models';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    CardModule,
    ProgressSpinnerModule,
    MessageModule,
    ButtonModule,
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
      firstName: '',
      lastName: '',
      title: '',
      education: '',
      lastWorkPlace: '',
      city: ''
    },
    experiences: [],
    skills: [],
    education: [],
    certificates: [],
    address: {
      city: ''
    },
    contactInfo: {
      email: ''
    }
  };

  lawyerProfileId: string | null = null;
  isLoading = true;
  errorMessage: string | null = null;

  ngOnInit(): void {
    this.loadProfileData();
  }

  loadProfileData(): void {
    this.isLoading = true;
    this.errorMessage = null;

    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          this.profileData = this.mapToProfileData(response.data);
          this.lawyerProfileId = response.data.lawyerProfile?.id || null;
        } else {
          this.errorMessage = response.errorMessage?.join(', ') || 'Profil verileri yüklenemedi.';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Profil verileri yüklenirken hata oluştu:', error);
        this.errorMessage = 'Profil verileri yüklenirken bir hata oluştu.';
        this.isLoading = false;
      }
    });
  }

  refreshExperiences(): void {
    // Scroll pozisyonunu koru
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    // Sadece deneyimleri yeniden yükle (loading state olmadan)
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const newProfileData = this.mapToProfileData(response.data);
          // Sadece experiences'i güncelle
          this.profileData.experiences = newProfileData.experiences;
          // Scroll pozisyonunu geri yükle
          requestAnimationFrame(() => {
            window.scrollTo(0, scrollPosition);
          });
        }
      },
      error: (error) => {
        console.error('Deneyimler yenilenirken hata oluştu:', error);
        // Hata durumunda da scroll pozisyonunu koru
        requestAnimationFrame(() => {
          window.scrollTo(0, scrollPosition);
        });
      }
    });
  }

  refreshEducation(): void {
    // Scroll pozisyonunu koru
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    // Sadece eğitim bilgilerini yeniden yükle (loading state olmadan)
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const newProfileData = this.mapToProfileData(response.data);
          // Sadece education'ı güncelle
          this.profileData.education = newProfileData.education;
          // Scroll pozisyonunu geri yükle
          requestAnimationFrame(() => {
            window.scrollTo(0, scrollPosition);
          });
        }
      },
      error: (error) => {
        console.error('Eğitim bilgileri yenilenirken hata oluştu:', error);
        // Hata durumunda da scroll pozisyonunu koru
        requestAnimationFrame(() => {
          window.scrollTo(0, scrollPosition);
        });
      }
    });
  }

  refreshAddress(): void {
    // Scroll pozisyonunu koru
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    // Sadece adres bilgilerini yeniden yükle (loading state olmadan)
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const newProfileData = this.mapToProfileData(response.data);
          // Sadece address'i güncelle
          this.profileData.address = newProfileData.address;
          // Scroll pozisyonunu geri yükle
          requestAnimationFrame(() => {
            window.scrollTo(0, scrollPosition);
          });
        }
      },
      error: (error) => {
        console.error('Adres bilgileri yenilenirken hata oluştu:', error);
        // Hata durumunda da scroll pozisyonunu koru
        requestAnimationFrame(() => {
          window.scrollTo(0, scrollPosition);
        });
      }
    });
  }

  refreshContact(): void {
    // Scroll pozisyonunu koru
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    // Sadece iletişim bilgilerini yeniden yükle (loading state olmadan)
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const newProfileData = this.mapToProfileData(response.data);
          // Sadece contactInfo'yu güncelle
          this.profileData.contactInfo = newProfileData.contactInfo;
          // Scroll pozisyonunu geri yükle
          requestAnimationFrame(() => {
            window.scrollTo(0, scrollPosition);
          });
        }
      },
      error: (error) => {
        console.error('İletişim bilgileri yenilenirken hata oluştu:', error);
        // Hata durumunda da scroll pozisyonunu koru
        requestAnimationFrame(() => {
          window.scrollTo(0, scrollPosition);
        });
      }
    });
  }

  refreshCertificates(): void {
    // Scroll pozisyonunu koru
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    // Sadece sertifikaları yeniden yükle (loading state olmadan)
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const newProfileData = this.mapToProfileData(response.data);
          // Sadece certificates'i güncelle
          this.profileData.certificates = newProfileData.certificates;
          // Scroll pozisyonunu geri yükle
          requestAnimationFrame(() => {
            window.scrollTo(0, scrollPosition);
          });
        }
      },
      error: (error) => {
        console.error('Sertifikalar yenilenirken hata oluştu:', error);
        // Hata durumunda da scroll pozisyonunu koru
        requestAnimationFrame(() => {
          window.scrollTo(0, scrollPosition);
        });
      }
    });
  }

  refreshSkills(): void {
    // Scroll pozisyonunu koru
    const scrollPosition = window.scrollY || document.documentElement.scrollTop;
    
    // Sadece uzmanlıkları yeniden yükle (loading state olmadan)
    this.profileService.getUserProfileFull().subscribe({
      next: (response) => {
        if (response.isSuccess && response.data) {
          const newProfileData = this.mapToProfileData(response.data);
          // Sadece skills'i güncelle
          this.profileData.skills = newProfileData.skills;
          // Scroll pozisyonunu geri yükle
          requestAnimationFrame(() => {
            window.scrollTo(0, scrollPosition);
          });
        }
      },
      error: (error) => {
        console.error('Uzmanlıklar yenilenirken hata oluştu:', error);
        // Hata durumunda da scroll pozisyonunu koru
        requestAnimationFrame(() => {
          window.scrollTo(0, scrollPosition);
        });
      }
    });
  }

  private mapToProfileData(apiData: UserProfileWDetailsDto): ProfileData {
    const lawyerProfile = apiData.lawyerProfile;

    // Summary mapping
    const summary: ProfileSummary = {
      firstName: this.capitalizeWords(apiData.firstName),
      lastName: this.capitalizeWords(apiData.lastName),
      title: this.getLatestPosition(lawyerProfile?.experience) ? this.capitalizeWords(this.getLatestPosition(lawyerProfile?.experience)) : 'Kullanıcı',
      education: this.getLatestEducation(lawyerProfile?.academy),
      lastWorkPlace: this.getLatestWorkPlace(lawyerProfile?.experience) ? this.capitalizeWords(this.getLatestWorkPlace(lawyerProfile?.experience)) : '',
      city: apiData.address?.city?.name ? this.capitalizeWords(apiData.address.city.name) : '',
      barAssociation: lawyerProfile?.barAssociation ? this.capitalizeWords(lawyerProfile.barAssociation) : undefined,
      barNumber: lawyerProfile?.barNumber
    };

    // Experiences mapping and sorting by start date (newest first)
    const experiences: Experience[] = (lawyerProfile?.experience || [])
      .slice() // Create a copy to avoid mutating the original array
      .sort((a, b) => {
        // Sort by start date (newest first)
        const dateA = new Date(a.startDate);
        const dateB = new Date(b.startDate);
        return dateB.getTime() - dateA.getTime();
      })
      .map(exp => ({
        id: exp.id,
        companyName: this.capitalizeWords(exp.companyName),
        position: this.capitalizeWords(exp.position),
        startDate: this.formatDate(exp.startDate),
        endDate: exp.endDate ? this.formatDate(exp.endDate) : undefined,
        description: exp.description ? this.capitalizeFirst(exp.description) : '',
        isCurrent: !exp.endDate
      }));

    // Education mapping and sorting by start date (newest first)
    const education: Education[] = (lawyerProfile?.academy || [])
      .slice() // Create a copy to avoid mutating the original array
      .sort((a, b) => {
        // Sort by start date (newest first)
        const dateA = new Date(a.startDate);
        const dateB = new Date(b.startDate);
        return dateB.getTime() - dateA.getTime();
      })
      .map(acad => ({
        id: acad.id,
        schoolName: this.capitalizeWords(acad.university),
        department: '',
        graduationYear: acad.endDate ? new Date(acad.endDate).getFullYear() : undefined,
        degree: acad.degree ? this.capitalizeWords(acad.degree) : undefined,
        startDate: this.formatDate(acad.startDate),
        endDate: acad.endDate ? this.formatDate(acad.endDate) : undefined
      }));

    // Certificates mapping
    const certificates: Certificate[] = (lawyerProfile?.certificates || []).map(cert => ({
      id: cert.id,
      name: this.capitalizeWords(cert.name),
      issuingOrganization: this.capitalizeWords(cert.institution),
      issueDate: this.formatDate(cert.dateReceived)
    }));

    // Skills mapping (from LawyerExpertisements)
    const skills: Skill[] = (lawyerProfile?.lawyerExpertisements || []).map(exp => ({
      id: exp.id,
      name: this.capitalizeWords(exp.expertisement.name)
    }));

    // Address mapping
    const address: Address = {
      id: apiData.address?.id,
      userProfileId: apiData.address?.userProfileId,
      city: apiData.address?.city?.name ? this.capitalizeWords(apiData.address.city.name) : '',
      cityId: apiData.address?.cityId,
      addressLine: apiData.address?.addressLine ? this.capitalizeFirst(apiData.address.addressLine) : undefined,
      fullAddress: apiData.address?.addressLine ? this.capitalizeFirst(apiData.address.addressLine) : undefined
    };

    // Contact mapping
    const contactInfo: ContactInfo = {
      id: lawyerProfile?.contact?.id,
      lawyerProfileId: lawyerProfile?.contact?.lawyerProfileId,
      phoneNumber: lawyerProfile?.contact?.phoneNumber || apiData.phoneNumber || '',
      alternatePhoneNumber: lawyerProfile?.contact?.alternatePhoneNumber,
      email: lawyerProfile?.contact?.email || apiData.email,
      alternateEmail: lawyerProfile?.contact?.alternateEmail,
      website: lawyerProfile?.contact?.website,
      linkedin: undefined, // Frontend only
      github: undefined // Frontend only
    };

    return {
      summary,
      experiences,
      skills,
      education,
      certificates,
      address,
      contactInfo
    };
  }

  private getLatestEducation(academyList?: any[]): string {
    if (!academyList || academyList.length === 0) return '';
    
    // En son biten eğitimi bul (EndDate'e göre)
    const sorted = [...academyList]
      .filter(a => a.endDate)
      .sort((a, b) => new Date(b.endDate).getTime() - new Date(a.endDate).getTime());
    
    if (sorted.length > 0) {
      const latest = sorted[0];
      const degree = latest.degree ? this.capitalizeWords(latest.degree) : '';
      const university = this.capitalizeWords(latest.university);
      return degree ? `${degree} - ${university}` : university;
    }
    
    // Eğer biten eğitim yoksa, en son başlayanı al
    const sortedByStart = [...academyList]
      .sort((a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime());
    
    if (sortedByStart.length > 0) {
      const latest = sortedByStart[0];
      const degree = latest.degree ? this.capitalizeWords(latest.degree) : '';
      const university = this.capitalizeWords(latest.university);
      return degree ? `${degree} - ${university}` : university;
    }
    
    return '';
  }

  private getLatestWorkPlace(experienceList?: any[]): string {
    if (!experienceList || experienceList.length === 0) return '';
    
    // Önce devam eden işleri kontrol et
    const currentJobs = experienceList.filter(exp => !exp.endDate);
    if (currentJobs.length > 0) {
      return currentJobs[0].companyName;
    }
    
    // Devam eden iş yoksa, en son biten işi al
    const sorted = [...experienceList]
      .filter(exp => exp.endDate)
      .sort((a, b) => new Date(b.endDate).getTime() - new Date(a.endDate).getTime());
    
    return sorted.length > 0 ? sorted[0].companyName : '';
  }

  private getLatestPosition(experienceList?: any[]): string {
    if (!experienceList || experienceList.length === 0) return '';
    
    // Önce devam eden işleri kontrol et
    const currentJobs = experienceList.filter(exp => !exp.endDate);
    if (currentJobs.length > 0) {
      return currentJobs[0].position;
    }
    
    // Devam eden iş yoksa, en son biten işin pozisyonunu al
    const sorted = [...experienceList]
      .filter(exp => exp.endDate)
      .sort((a, b) => new Date(b.endDate).getTime() - new Date(a.endDate).getTime());
    
    return sorted.length > 0 ? sorted[0].position : '';
  }

  private formatDate(dateString: string): string {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    return `${year}-${month}`;
  }

  private capitalizeWords(str: string): string {
    if (!str) return '';
    return str
      .toLowerCase()
      .split(' ')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');
  }

  private capitalizeFirst(str: string): string {
    if (!str) return '';
    return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
  }
}

