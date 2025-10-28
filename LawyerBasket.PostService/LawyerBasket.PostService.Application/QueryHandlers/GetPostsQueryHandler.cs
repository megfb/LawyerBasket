using AutoMapper;
using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.PostService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.PostService.Application.QueryHandlers
{
  public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, ApiResult<IEnumerable<PostDto>>>
  {
    private readonly IMapper _mapper;
    private readonly ILogger<GetPostQueryHandler> _logger;
    private readonly IPostRepository _postRepository;
    public GetPostsQueryHandler(IMapper mapper, ILogger<GetPostQueryHandler> logger, IPostRepository postRepository)
    {
      _logger = logger;
      _mapper = mapper;
      _postRepository = postRepository;
    }
    public async Task<ApiResult<IEnumerable<PostDto>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Query is handling");
      try
      {
        var posts = await _postRepository.GetAllByUserIdAsync(request.Id);
        var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
        _logger.LogInformation("Successfully retrieved posts");
        return ApiResult<IEnumerable<PostDto>>.Success(postsDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving posts");
        return ApiResult<IEnumerable<PostDto>>.Fail("An error occurred while processing your request");
      }
    }
  }
}
