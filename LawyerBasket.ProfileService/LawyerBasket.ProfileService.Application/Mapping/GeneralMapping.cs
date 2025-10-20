using AutoMapper;

namespace LawyerBasket.ProfileService.Application.Mapping
{
  public class GeneralMapping : Profile
  {
    public GeneralMapping()
    {
      CreateMap<Domain.Entities.UserProfile, Dtos.UserProfileDto>().ReverseMap();
    }
  }
}


