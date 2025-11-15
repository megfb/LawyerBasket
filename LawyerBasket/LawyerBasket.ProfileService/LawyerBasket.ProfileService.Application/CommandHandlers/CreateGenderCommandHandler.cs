using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, ApiResult<GenderDto>>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateGenderCommandHandler> _logger;
        private readonly IMapper _mapper;
        
        public CreateGenderCommandHandler(
            IGenderRepository genderRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<CreateGenderCommandHandler> logger, 
            IMapper mapper)
        {
            _genderRepository = genderRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<GenderDto>> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateGender started. Name: {Name}", request.Name);
            try
            {
                var entity = new Gender
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                };
                
                _logger.LogInformation("Creating gender entity with Name: {Name}", request.Name);
                await _genderRepository.CreateAsync(entity);

                _logger.LogInformation("Saving changes to the database for gender with Name: {Name}", request.Name);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Gender created successfully with Id: {GenderId}", entity.Id);
                return ApiResult<GenderDto>.Success(_mapper.Map<GenderDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating gender with Name: {Name}", request.Name);
                return ApiResult<GenderDto>.Fail("An unexpected error occurred");
            }
        }
    }
}

