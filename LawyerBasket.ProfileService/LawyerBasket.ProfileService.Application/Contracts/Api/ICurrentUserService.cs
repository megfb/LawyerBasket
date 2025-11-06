namespace LawyerBasket.ProfileService.Application.Contracts.Api
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}
