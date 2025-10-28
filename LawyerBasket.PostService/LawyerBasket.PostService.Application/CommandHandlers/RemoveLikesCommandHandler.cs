using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Contracts.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerBasket.PostService.Application.CommandHandlers
{
  public class RemoveLikesCommandHandler : IRequestHandler<RemoveLikesCommand, ApiResult>
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRepository _postRepository;
    private readonly ILogger<RemoveLikesCommandHandler> _logger;

    public RemoveLikesCommandHandler(IUnitOfWork unitOfWork, IPostRepository postRepository, ILogger<RemoveLikesCommandHandler> logger)
    {
      _unitOfWork = unitOfWork;
      _postRepository = postRepository;
      _logger = logger;
    }
    public async Task<ApiResult> Handle(RemoveLikesCommand request, CancellationToken cancellationToken)
    {

      _logger.LogInformation("Remove likes command is handling");
      try
      {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post is null)
        {
          _logger.LogError("Post not found");
          return ApiResult.Fail("Post not found", System.Net.HttpStatusCode.NotFound);
        }

        var like = post.Likes.Where(x => x.Id == request.LikeId).FirstOrDefault();

        _logger.LogInformation("Like is removing");
        post.Likes.Remove(like);

        _logger.LogInformation("Post is updating");
        _postRepository.Update(post);

        _logger.LogInformation("Saving changes to db");
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Remove like command is handling");
        return ApiResult.Success();

      }
      catch (Exception ex)
      {
        _logger.LogInformation(ex, "An error has occured");
        return ApiResult.Fail("An error has occured");
      }
    }
  }
}
