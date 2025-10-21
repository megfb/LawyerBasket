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
    }
  }
}


