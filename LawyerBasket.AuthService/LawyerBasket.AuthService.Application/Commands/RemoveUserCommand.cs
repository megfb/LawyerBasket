using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LawyerBasket.AuthService.Application.Commands
{
    public class RemoveUserCommand:IRequest<ApiResult>
    {
        public string Id { get; set; }
    }
}
