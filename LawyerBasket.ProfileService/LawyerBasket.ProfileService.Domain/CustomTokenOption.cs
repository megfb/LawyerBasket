namespace LawyerBasket.ProfileService.Domain
{
    public class CustomTokenOption
    {
        public string Issuer { get; set; } = default!;
        public List<string> Audience { get; set; } = default!;
        public int ExpiryMinutes { get; set; } = default!;
        public string SecurityKey { get; set; } = default!;
    }
}
