using AutoMapper;
using LawyerBasket.SocialService.Api.Application.Dtos;
using LawyerBasket.SocialService.Api.Domain.Entities;

namespace LawyerBasket.SocialService.Api.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<FriendConnection, FriendConnectionDto>().ReverseMap();
            CreateMap<Friendship, FriendshipDto>().ReverseMap();
        }
    }
}
