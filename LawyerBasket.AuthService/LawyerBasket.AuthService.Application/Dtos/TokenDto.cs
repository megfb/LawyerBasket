namespace LawyerBasket.AuthService.Application.Dtos
{
  public class TokenDto
  {
    public string AccessToken { get; set; }

    public DateTime AccessTokenExpiration { get; set; }
  }
}
