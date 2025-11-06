using LawyerBasket.ProfileService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;

namespace LawyerBasket.ProfileService.Data.Certificates
{
    public class CertificateRepository(AppDbContext context) : GenericRepository<Domain.Entities.Certificates>(context), ICertificateRepository
    {
        public async Task<IEnumerable<Domain.Entities.Certificates>> GetAllByLawyerIdAsync(string id)
        {
            return await context.Certificates.Where(c => c.LawyerProfileId == id).ToListAsync();
        }
    }
}
