using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.UserProfile
{
    public class UserProfileRepository(AppDbContext dbContext) : GenericRepository<Domain.Entities.UserProfile>(dbContext), IUserProfileRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> AnyByEmail(string email)
        {
            return await _dbContext.UserProfile.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
