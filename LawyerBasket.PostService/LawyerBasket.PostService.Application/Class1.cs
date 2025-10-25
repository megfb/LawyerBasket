using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Domain.Entities;
using System.Net;

namespace LawyerBasket.PostService.Application
{
  public class Class1 : IClass1
  {
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;
    public Class1(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
      _postRepository = postRepository;
      _unitOfWork = unitOfWork;
    }


    public async Task<HttpStatusCode> AddToDb(Post post)
    {
      await _postRepository.CreateAsync(post);
      await _unitOfWork.SaveChangesAsync();
      return HttpStatusCode.OK;
    }
  }
}
