using LawyerBasket.PostService.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Data.Post
{
  public class PostRepository(AppDbContext appDbContext) : GenericRepository<Domain.Entities.Post>(appDbContext), IPostRepository
  {
  }
}
