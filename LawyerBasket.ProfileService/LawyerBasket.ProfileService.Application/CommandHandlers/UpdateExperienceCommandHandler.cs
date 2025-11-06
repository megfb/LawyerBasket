using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
    public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, ApiResult<ExperienceDto>>
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateExperienceCommandHandler> _logger;
        public UpdateExperienceCommandHandler(IExperienceRepository experienceRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateExperienceCommandHandler> logger)
        {
            _experienceRepository = experienceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ApiResult<ExperienceDto>> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("UpdateExperienceCommandHandler called with Id: {Id}", request.Id);

            try
            {
                var experience = await _experienceRepository.GetByIdAsync(request.Id);
                if (experience == null)
                {
                    _logger.LogWarning("Experience with Id: {Id} not found", request.Id);
                    return ApiResult<ExperienceDto>.Fail($"Experience with Id {request.Id} not found", System.Net.HttpStatusCode.NotFound);
                }
                experience.CompanyName = request.CompanyName;
                experience.Position = request.Position;
                experience.StartDate = request.StartDate;
                experience.EndDate = request.EndDate;
                experience.Description = request.Description;
                experience.UpdatedAt = DateTime.UtcNow;

                _logger.LogInformation("Updating experience with Id: {Id}", request.Id);
                _experienceRepository.Update(experience);

                _logger.LogInformation("Saving changes to the database for experience with Id: {Id}", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var experienceDto = _mapper.Map<ExperienceDto>(experience);

                _logger.LogInformation("Experience with Id: {Id} updated successfully", request.Id);
                return ApiResult<ExperienceDto>.Success(experienceDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating experience with Id: {Id}", request.Id);
                return ApiResult<ExperienceDto>.Fail("An error occurred while updating the experience.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
