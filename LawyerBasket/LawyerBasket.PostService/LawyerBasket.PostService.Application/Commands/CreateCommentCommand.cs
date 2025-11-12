using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
    public class CreateCommentCommand : IRequest<ApiResult<CommentDto>>
    {
        //public string UserId { get; set; } = default!;
        public string PostId { get; set; } = default!;
        public string Text { get; set; } = default!;

    }
}
