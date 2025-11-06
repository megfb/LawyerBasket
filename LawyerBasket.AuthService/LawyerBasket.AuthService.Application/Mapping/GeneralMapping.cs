using AutoMapper;

namespace LawyerBasket.AuthService.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // CreateMap<Source, Destination>();
            // Example:
            CreateMap<Domain.Entities.AppUser, Dtos.AppUserDto>().ReverseMap();
            CreateMap<Domain.Entities.AppRole, Dtos.AppRoleDto>().ReverseMap();
        }
    }
}
