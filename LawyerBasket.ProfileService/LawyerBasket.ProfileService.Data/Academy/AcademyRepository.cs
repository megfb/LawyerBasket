using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Academy
{
  public class AcademyRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Academy>(appDbContext), IAcademyRepository
  {
  }

}
