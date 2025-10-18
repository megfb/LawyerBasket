using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Certificates
{
  public class CertificateRepository(AppDbContext context) : GenericRepository<Domain.Entities.Certificates>(context), ICertificateRepository
  {
  }
}
