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
    public class CreateAcademyCommandHandler : IRequestHandler<CreateAcademyCommand, ApiResult<AcademyDto>>
    {
        private readonly IAcademyRepository _academyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAcademyCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateAcademyCommandHandler(IAcademyRepository academyRepository, IUnitOfWork unitOfWork, ILogger<CreateAcademyCommandHandler> logger, IMapper mapper)
        {
            _academyRepository = academyRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResult<AcademyDto>> Handle(CreateAcademyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateAcademy started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
            try
            {
                var entity = new Academy
                {
                    Id = Guid.NewGuid().ToString(),
                    LawyerProfileId = request.LawyerProfileId,
                    University = request.University,
                    Degree = request.Degree,
                    Department = request.Department,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    CreatedAt = DateTime.UtcNow,
                };
                _logger.LogInformation("Creating academy entity for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                await _academyRepository.CreateAsync(entity);

                _logger.LogInformation("Saving changes to the database for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Academy created successfully with Id: {AcademyId}", entity.Id);
                return ApiResult<AcademyDto>.Success(_mapper.Map<AcademyDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating academy for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
                return ApiResult<AcademyDto>.Fail("An unexpected error occurred");
            }
        }
    }
}


