namespace LawyerBasket.SocialService.Api.Infrastructure
{
  public class CustomTokenOption
  {
    public string Issuer { get; set; }
    public List<string> Audience { get; set; }
    public int ExpiryMinutes { get; set; }
    public string SecurityKey { get; set; }
  }
}
