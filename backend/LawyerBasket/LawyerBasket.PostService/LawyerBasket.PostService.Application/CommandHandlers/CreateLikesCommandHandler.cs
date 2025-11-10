using AutoMapper;
using LawyerBasket.PostService.Application.Commands;
using LawyerBasket.PostService.Application.Contracts.Data;
using LawyerBasket.PostService.Application.Dtos;
using LawyerBasket.PostService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace LawyerBasket.PostService.Application.CommandHandlers
{
    public class CreateLikesCommandHandler : IRequestHandler<CreateLikesCommand, ApiResult<LikesDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateLikesCommandHandler> _logger;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateLikesCommandHandler(IMapper mapper, ILogger<CreateLikesCommandHandler> logger, IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<LikesDto>> Handle(CreateLikesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Likes command is handling");
            try
            {
                var post = await _postRepository.GetByIdAsync(request.PostId);
                if (post == null)
                {
                    _logger.LogInformation("Post not found");
                    return ApiResult<LikesDto>.Fail("Post not found", HttpStatusCode.NotFound);
                }
                var likes = new Likes()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = request.UserId,
                    PostId = request.PostId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                post.Likes!.Add(likes);

                _postRepository.Update(post);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult<LikesDto>.Success(_mapper.Map<LikesDto>(likes));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error has occured");
                return ApiResult<LikesDto>.Fail(ex.Message);
            }
        }
    }
}
