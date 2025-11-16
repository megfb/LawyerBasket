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
    public class GetPostLikesQueryHandler : IRequestHandler<GetPostLikesQuery, ApiResult<IEnumerable<LikesDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetPostLikesQueryHandler> _logger;
        private readonly IPostRepository _postRepository;

        public GetPostLikesQueryHandler(
            IMapper mapper,
            ILogger<GetPostLikesQueryHandler> logger,
            IPostRepository postRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _postRepository = postRepository;
        }

        public async Task<ApiResult<IEnumerable<LikesDto>>> Handle(GetPostLikesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetPostLikesQuery is handling for PostId: {PostId}", request.PostId);
            try
            {
                if (string.IsNullOrEmpty(request.PostId))
                {
                    _logger.LogWarning("PostId is null or empty");
                    return ApiResult<IEnumerable<LikesDto>>.Fail("Post ID is required");
                }

                var post = await _postRepository.GetByIdAsync(request.PostId);
                if (post == null)
                {
                    _logger.LogWarning("Post not found for PostId: {PostId}", request.PostId);
                    return ApiResult<IEnumerable<LikesDto>>.Fail("Post not found");
                }

                var likes = post.Likes ?? new List<Domain.Entities.Likes>();
                var likesDto = _mapper.Map<IEnumerable<LikesDto>>(likes);

                _logger.LogInformation("Successfully retrieved {Count} likes for post", likesDto.Count());
                return ApiResult<IEnumerable<LikesDto>>.Success(likesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving likes for post");
                return ApiResult<IEnumerable<LikesDto>>.Fail("An error occurred while processing your request");
            }
        }
    }
}

