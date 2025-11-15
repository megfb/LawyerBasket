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
    public class GetPostsLikedByUserQueryHandler : IRequestHandler<GetPostsLikedByUserQuery, ApiResult<IEnumerable<PostDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPostsLikedByUserQueryHandler> _logger;
        private readonly IPostRepository _postRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetPostsLikedByUserQueryHandler(
            IMapper mapper,
            ILogger<GetPostsLikedByUserQueryHandler> logger,
            IPostRepository postRepository,
            ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _logger = logger;
            _postRepository = postRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResult<IEnumerable<PostDto>>> Handle(GetPostsLikedByUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPostsLikedByUserQuery is handling for UserId: {UserId}", _currentUserService.UserId);
            try
            {
                var posts = await _postRepository.GetPostsLikedByUserIdAsync(_currentUserService.UserId);
                var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
                _logger.LogInformation("Successfully retrieved {Count} posts liked by user {UserId}", postsDto.Count(), _currentUserService.UserId);
                return ApiResult<IEnumerable<PostDto>>.Success(postsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving posts liked by user {UserId}", _currentUserService.UserId);
                return ApiResult<IEnumerable<PostDto>>.Fail("An error occurred while processing your request");
            }
        }
    }
}

