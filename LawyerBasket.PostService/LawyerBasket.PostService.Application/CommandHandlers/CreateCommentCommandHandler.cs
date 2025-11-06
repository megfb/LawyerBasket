using AutoMapper;
using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.PostService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.PostService.Application.CommandHandlers
{
  public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ApiResult<CommentDto>>
  {

    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRepository _postRepository;
    private readonly ILogger<CreateCommentCommandHandler> _logger;
    private readonly IMapper _mapper;
    public CreateCommentCommandHandler(IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork, IPostRepository postRepository, ILogger<CreateCommentCommandHandler> logger)
    {
      _mediator = mediator;
      _unitOfWork = unitOfWork;
      _postRepository = postRepository;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<ApiResult<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Create comment command is handling");
      try
      {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post is null)
        {
          _logger.LogInformation("Post not found for id: {PostId}", request.PostId);
          return ApiResult<CommentDto>.Fail("Post not found");

        }
        _logger.LogInformation("Comment is creating");
        var comment = new Comment()
        {
          Id = Guid.NewGuid().ToString(),
          UserId = request.UserId,
          PostId = request.PostId,
          Text = request.Text,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        _logger.LogInformation("Comment is adding");
        post.Comments!.Add(comment);
        _logger.LogInformation("Comment is updating");
        _postRepository.Update(post);
        _logger.LogInformation("Saving changes to db");
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResult<CommentDto>.Success(_mapper.Map<CommentDto>(comment));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error has occured");
        return ApiResult<CommentDto>.Fail("An unexpected error occurred");
        throw;
      }
    }
  }
}
