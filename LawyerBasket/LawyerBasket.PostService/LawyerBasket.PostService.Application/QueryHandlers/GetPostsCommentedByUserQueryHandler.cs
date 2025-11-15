using AutoMapper;
using LawyerBasket.PostService.Application.Contracts.Api;
using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.PostService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.PostService.Application.QueryHandlers
{
    public class GetPostsCommentedByUserQueryHandler : IRequestHandler<GetPostsCommentedByUserQuery, ApiResult<IEnumerable<PostDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPostsCommentedByUserQueryHandler> _logger;
        private readonly IPostRepository _postRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetPostsCommentedByUserQueryHandler(
            IMapper mapper,
            ILogger<GetPostsCommentedByUserQueryHandler> logger,
            IPostRepository postRepository,
            ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _logger = logger;
            _postRepository = postRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResult<IEnumerable<PostDto>>> Handle(GetPostsCommentedByUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPostsCommentedByUserQuery is handling for UserId: {UserId}", _currentUserService.UserId);
            try
            {
                if (string.IsNullOrEmpty(_currentUserService.UserId))
                {
                    _logger.LogWarning("UserId is null or empty");
                    return ApiResult<IEnumerable<PostDto>>.Fail("User ID is required");
                }

                var posts = await _postRepository.GetPostsCommentedByUserIdAsync(_currentUserService.UserId);
                var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
                
                _logger.LogInformation("Successfully retrieved {Count} posts commented by user", postsDto.Count());
                return ApiResult<IEnumerable<PostDto>>.Success(postsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving posts commented by user");
                return ApiResult<IEnumerable<PostDto>>.Fail("An error occurred while processing your request");
            }
        }
    }
}

