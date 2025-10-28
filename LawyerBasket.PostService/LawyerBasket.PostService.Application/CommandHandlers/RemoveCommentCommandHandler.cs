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
  public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand, ApiResult>
  {
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveCommentCommandHandler> _logger;

    public RemoveCommentCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork, ILogger<RemoveCommentCommandHandler> logger)
    {
      _postRepository = postRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Remove comment command is handling");
      try
      {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post is null)
        {
          _logger.LogError("Post not found");
          return ApiResult.Fail("Post not found", System.Net.HttpStatusCode.NotFound);
        }

        var comment = post.Comments.Where(x => x.Id == request.CommentId).FirstOrDefault();
        if (comment is null)
        {
          _logger.LogError("Comment not found");
          return ApiResult.Fail("Comment not found", System.Net.HttpStatusCode.NotFound);
        }
        _logger.LogInformation("Comment is removing");
        post.Comments.Remove(comment);
        _logger.LogInformation("Database is updating");
        _postRepository.Update(post);
        _logger.LogInformation("Saving changes to database");
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult.Success();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error has occured for remove comment command");
        return ApiResult.Fail("An error has occured for comment");
      }

    }
  }
}
