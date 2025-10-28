using LawyerBasket.PostService.Application.Dtos;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class CreateLikesCommand:IRequest<ApiResult<LikesDto>>
  {
    public string UserId { get; set; }
    public string PostId { get; set; }
  }
}
