using LawyerBasket.PostService.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Application.Commands
{
  public class CreateCommentCommand : IRequest<ApiResult<CommentDto>>
  {
    public string UserId { get; set; }
    public string PostId { get; set; }
    public string Text { get; set; }

  }
}
