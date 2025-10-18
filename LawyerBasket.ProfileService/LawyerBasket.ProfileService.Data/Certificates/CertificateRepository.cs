using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerBasket.ProfileService.Application.Contracts.Data;

namespace LawyerBasket.ProfileService.Data.Certificates
{
  public class CertificateRepository(AppDbContext context) : GenericRepository<Domain.Entities.Certificates>(context), ICertificateRepository
  {
  }
}
