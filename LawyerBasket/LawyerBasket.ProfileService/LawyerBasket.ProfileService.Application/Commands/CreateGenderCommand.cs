using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.ProfileService.Application.Commands
{
    public class CreateGenderCommand : IRequest<ApiResult<GenderDto>>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}

