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
    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, ApiResult<ExperienceDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateExperienceCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateExperienceCommandHandler(IMapper mapper, IExperienceRepository experienceRepository, IUnitOfWork unitOfWork, ILogger<CreateExperienceCommandHandler> logger)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResult<ExperienceDto>> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateExperience started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
            try
            {
                _logger.LogInformation("Creating experience entity for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                var entity = new Experience
                {
                    Id = Guid.NewGuid().ToString(),
                    LawyerProfileId = request.LawyerProfileId,
                    CompanyName = request.CompanyName,
                    Position = request.Position,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Description = request.Description,
                    CreatedAt = DateTime.UtcNow,
                };
                _logger.LogInformation("Persisting experience entity for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                await _experienceRepository.CreateAsync(entity);
                _logger.LogInformation("Saving changes to the database for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Experience created successfully with Id: {ExperienceId} for LawyerProfileId: {LawyerProfileId}", entity.Id, request.LawyerProfileId);
                return ApiResult<ExperienceDto>.Success(_mapper.Map<ExperienceDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating experience for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
                return ApiResult<ExperienceDto>.Fail("An unexpected error occurred");
            }
        }
    }
}


