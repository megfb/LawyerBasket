using LawyerBasket.Shared.Common.Response;
using MediatR;

namespace LawyerBasket.PostService.Application.Commands
{
  public class RemoveCommentCommand : IRequest<ApiResult>
  {
    public string PostId { get; set; } = default!;
    public string CommentId { get; set; } = default!;
  }
}
