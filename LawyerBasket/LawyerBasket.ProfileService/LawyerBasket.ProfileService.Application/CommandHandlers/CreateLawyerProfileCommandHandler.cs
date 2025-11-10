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
    public class CreateLawyerProfileCommandHandler : IRequestHandler<CreateLawyerProfileCommand, ApiResult<LawyerProfileDto>>
    {
        private readonly ILawyerProfileRepository _lawyerProfileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateLawyerProfileCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateLawyerProfileCommandHandler(ILawyerProfileRepository lawyerProfileRepository, IUnitOfWork unitOfWork, ILogger<CreateLawyerProfileCommandHandler> logger, IMapper mapper)
        {
            _lawyerProfileRepository = lawyerProfileRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResult<LawyerProfileDto>> Handle(CreateLawyerProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("CreateLawyerProfile started. UserProfileId: {UserProfileId}", request.UserProfileId);
                if (await _lawyerProfileRepository.BarNumberAny(request.BarNumber))
                {
                    _logger.LogError("Bar number is exist");
                    return ApiResult<LawyerProfileDto>.Fail("Bar number is exist");
                }

                if (await _lawyerProfileRepository.LicenseNumberAny(request.LicenseNumber))
                {
                    _logger.LogError("License number is exist");
                    return ApiResult<LawyerProfileDto>.Fail("License number is exist");
                }

                var entity = new LawyerProfile
                {
                    Id = Guid.NewGuid().ToString(),
                    UserProfileId = request.UserProfileId,
                    BarAssociation = request.BarAssociation,
                    BarNumber = request.BarNumber,
                    LicenseNumber = request.LicenseNumber,
                    LicenseDate = request.LicenseDate,
                    CreatedAt = DateTime.UtcNow,
                };

                _logger.LogInformation("Creating lawyer profile entity for user {UserProfileId}", request.UserProfileId);
                await _lawyerProfileRepository.CreateAsync(entity);

                _logger.LogInformation("Saving changes to the database for user {UserProfileId}", request.UserProfileId);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Lawyer profile created successfully for user {UserProfileId}", request.UserProfileId);
                return ApiResult<LawyerProfileDto>.Success(_mapper.Map<LawyerProfileDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating lawyer profile for user {UserProfileId}", request.UserProfileId);
                return ApiResult<LawyerProfileDto>.Fail("An unexpected error occurred");
            }
        }
    }
}


