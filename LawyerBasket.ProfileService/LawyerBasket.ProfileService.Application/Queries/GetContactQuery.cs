using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Queries
{
  public class GetContactQuery : IRequest<ApiResult<ContactDto>>
  {
    public string Id { get; set; } = default!;
  }
}
