using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Queries
{
    public class GetPostLikesQuery : IRequest<ApiResult<IEnumerable<LikesDto>>>
    {
        public string PostId { get; set; } = default!;
    }
}

