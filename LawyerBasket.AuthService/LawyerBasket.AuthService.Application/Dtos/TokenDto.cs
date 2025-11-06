namespace LawyerBasket.AuthService.Application.Dtos
{
  public class TokenDto
  {
    public string AccessToken { get; set; } = default!;

    public DateTime AccessTokenExpiration { get; set; }
  }
}
