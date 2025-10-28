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
  public class UpdateLawyerProfileCommandHandler : IRequestHandler<UpdateLawyerProfileCommand, ApiResult<LawyerProfileDto>>
  {
    private readonly ILawyerProfileRepository _lawyerProfileRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateLawyerProfileCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLawyerProfileCommandHandler(ILogger<UpdateLawyerProfileCommandHandler> logger, ILawyerProfileRepository lawyerProfileRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
      _lawyerProfileRepository = lawyerProfileRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _logger = logger;
    }
    public async Task<ApiResult<LawyerProfileDto>> Handle(UpdateLawyerProfileCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling UpdateLawyerProfileCommand for LawyerProfileId: {Id}", request.Id);
      try
      {
        var lawyerProfile = await _lawyerProfileRepository.GetByIdAsync(request.Id);

        if (lawyerProfile is null)
        {
          _logger.LogError("LawyerProfile with Id: {Id} not found", request.Id);
          return ApiResult<LawyerProfileDto>.Fail("Lawyer profile not found.");
        }

        if (await _lawyerProfileRepository.LicenseNumberAny(request.LicenseNumber))
        {
          _logger.LogError("LicenseNumber: {LicenseNumber} already exists", request.LicenseNumber);
          return ApiResult<LawyerProfileDto>.Fail("License number already exists.");
        }

        if (await _lawyerProfileRepository.BarNumberAny(request.BarNumber))
        {
          _logger.LogError("BarNumber: {BarNumber} already exists", request.BarNumber);
          return ApiResult<LawyerProfileDto>.Fail("Bar number already exists.");
        }

        lawyerProfile.BarAssociation = request.BarAssociation;
        lawyerProfile.BarNumber = request.BarNumber;
        lawyerProfile.LicenseNumber = request.LicenseNumber;
        lawyerProfile.LicenseDate = request.LicenseDate;
        lawyerProfile.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Updating LawyerProfile with Id: {Id}", request.Id);
        _lawyerProfileRepository.Update(lawyerProfile);

        _logger.LogInformation("Saving changes for LawyerProfile with Id: {Id}", request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var lawyerProfileDto = _mapper.Map<LawyerProfileDto>(lawyerProfile);
        _logger.LogInformation("Successfully updated LawyerProfile with Id: {Id}", request.Id);

        return ApiResult<LawyerProfileDto>.Success(lawyerProfileDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while updating LawyerProfile with Id: {Id}", request.Id);
        return ApiResult<LawyerProfileDto>.Fail("An error occurred while updating the lawyer profile.");
      }
    }
  }
}
