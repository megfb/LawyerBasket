using AutoMapper;
using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.PostService.Domain.Entities;

namespace LawyerBasket.PostService.Application.Mapping
{
  public class GeneralMapping : Profile
  {
    public GeneralMapping()
    {
      CreateMap<Post, PostDto>();
      CreateMap<Comment, CommentDto>();
    }
  }
}
