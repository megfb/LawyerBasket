using AutoMapper;
using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.PostService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.PostService.Application.QueryHandlers
{
  public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ApiResult<PostDto>>
  {
    private readonly IMapper _mapper;
    private readonly ILogger<GetPostQueryHandler> _logger;
    private readonly IPostRepository _postRepository;
    public GetPostQueryHandler(IMapper mapper, ILogger<GetPostQueryHandler> logger, IPostRepository postRepository)
    {
      _mapper = mapper;
      _logger = logger;
      _postRepository = postRepository;
    }
    public async Task<ApiResult<PostDto>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Command started");
      try
      {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null)
        {
          _logger.LogError("Post not found");
          return ApiResult<PostDto>.Fail("Post not found", System.Net.HttpStatusCode.NotFound);
        }
        var postDto = _mapper.Map<PostDto>(post);
        _logger.LogInformation("Command is succesfull");
        return ApiResult<PostDto>.Success(postDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving academies");
        return ApiResult<PostDto>.Fail("An error occurred while processing your request");
        throw;
      }
    }
  }
}
