using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class RemoveContactCommand : IRequest<ApiResult>
    {
        public string Id { get; set; } = default!;
    }
}
