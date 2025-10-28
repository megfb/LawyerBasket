using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetUserProfileQuery : IRequest<ApiResult<UserProfileDto>>
  {
    public string Id { get; set; }
  }
}
