using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.PostService.Application.CommandHandlers
{
  public class RemovePostCommandHandler : IRequestHandler<RemovePostCommand, ApiResult>
  {
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemovePostCommandHandler> _logger;
    public RemovePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork, ILogger<RemovePostCommandHandler> logger)
    {
      _postRepository = postRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult> Handle(RemovePostCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Remove post command is handling");
      try
      {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post is null)
        {
          _logger.LogError("Post not found");
          return ApiResult.Fail("Post not found", System.Net.HttpStatusCode.NotFound);
        }
        _logger.LogInformation("Post is removing");
        _postRepository.Delete(post);

        _logger.LogInformation("Saving changes to database");
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Handling is successfull");
        return ApiResult.Success();

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error has occured");
        return ApiResult.Fail("An error has occured");
      }

    }
  }
}
