using AutoMapper;

namespace LawyerBasket.ProfileService.Application.Mapping
{
  public class GeneralMapping : Profile
  {
    public GeneralMapping()
    {
      CreateMap<Domain.Entities.UserProfile, Dtos.UserProfileDto>().ReverseMap();
      CreateMap<Domain.Entities.Address, Dtos.AddressDto>().ReverseMap();
      CreateMap<Domain.Entities.LawyerProfile, Dtos.LawyerProfileDto>().ReverseMap();
      CreateMap<Domain.Entities.Contact, Dtos.ContactDto>().ReverseMap();
      CreateMap<Domain.Entities.Experience, Dtos.ExperienceDto>().ReverseMap();
      CreateMap<Domain.Entities.Academy, Dtos.AcademyDto>().ReverseMap();
      CreateMap<Domain.Entities.Certificates, Dtos.CertificatesDto>().ReverseMap();
      CreateMap<Domain.Entities.LawyerExpertisement, Dtos.LawyerExpertisementDto>().ReverseMap();
    }
  }
}


