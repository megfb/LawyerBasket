using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Domain.Entities;

namespace LawyerBasket.AuthService.Application.Contracts.Infrastructure
{
    public interface ITokenService
    {
        public TokenDto CreateToken(AppUser user);

    }
}
