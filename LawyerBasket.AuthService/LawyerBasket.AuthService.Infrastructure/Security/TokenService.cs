using LawyerBasket.AuthService.Application.Contracts.Infrastructure;
using LawyerBasket.AuthService.Application.Dtos;
using LawyerBasket.AuthService.Domain;
using LawyerBasket.AuthService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LawyerBasket.AuthService.Infrastructure.Security
{
  public class TokenService : ITokenService
  {
    private readonly CustomTokenOption _tokenOption;
    public TokenService(IOptions<CustomTokenOption> tokenOption)
    {
      _tokenOption = tokenOption.Value;
      Console.WriteLine("DEBUG TOKEN OPTION => " + System.Text.Json.JsonSerializer.Serialize(_tokenOption));
    }
    public TokenDto CreateToken(AppUser user)
    {
      var userRoles = user.AppUserRole!.Select(x => x.AppRole!.Name).ToList();

      var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };
      claims.AddRange(_tokenOption.Audience.Select(x => new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Aud, x)));
      claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

      var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.ExpiryMinutes);
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
      SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
          issuer: _tokenOption.Issuer,
          claims: claims,
          notBefore: DateTime.UtcNow,
          expires: accessTokenExpiration,
          signingCredentials: signingCredentials
      );

      var handler = new JwtSecurityTokenHandler();

      var token = handler.WriteToken(jwtSecurityToken);

      var tokenDto = new TokenDto
      {
        AccessToken = token,
        AccessTokenExpiration = accessTokenExpiration
      };

      return tokenDto;
    }
  }
}
