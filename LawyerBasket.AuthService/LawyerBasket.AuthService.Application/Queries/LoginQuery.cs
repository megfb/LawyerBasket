using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.AuthService.Application.Dtos;
using MediatR;

namespace LawyerBasket.AuthService.Application.Queries
{
    public class LoginQuery : IRequest<ApiResult<TokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
