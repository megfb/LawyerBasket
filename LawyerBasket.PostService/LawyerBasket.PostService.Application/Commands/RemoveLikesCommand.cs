using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Application.Commands
{
  public class RemoveLikesCommand : IRequest<ApiResult>
  {
    public string PostId { get; set; }
    public string LikeId { get; set; }
  }
}
