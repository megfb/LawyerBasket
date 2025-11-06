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
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ApiResult<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePostCommandHandler> _logger;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IMapper mapper, ILogger<CreatePostCommandHandler> logger, IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResult<PostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Command Handling is started");
            try
            {
                _logger.LogInformation("Post is creating");
                var post = new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = request.UserId,
                    Content = request.Content,
                    CreatedAt = DateTime.UtcNow,
                };
                _logger.LogInformation("Creating post entity");
                await _postRepository.CreateAsync(post);
                _logger.LogInformation("Saving to database");
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Command successfull");
                return ApiResult<PostDto>.Success(_mapper.Map<PostDto>(post));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating academy for post");
                return ApiResult<PostDto>.Fail("An unexpected error occurred");
            }
            throw new NotImplementedException();
        }
    }
}
