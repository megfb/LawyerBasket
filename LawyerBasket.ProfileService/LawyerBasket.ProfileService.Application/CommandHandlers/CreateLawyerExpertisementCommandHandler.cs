using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateLawyerExpertisementCommandHandler : IRequestHandler<CreateLawyerExpertisementCommand, ApiResult<LawyerExpertisementDto>>
  {
    private readonly ILawyerExpertisementRepository _lawyerExpertisementRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfwork;
    private readonly ILogger<CreateLawyerExpertisementCommandHandler> _logger;
    public CreateLawyerExpertisementCommandHandler(ILogger<CreateLawyerExpertisementCommandHandler> logger, IUnitOfWork unitOfwork, IMapper mapper, ILawyerExpertisementRepository lawyerExpertisementRepository)
    {
      _logger = logger;
      _unitOfwork = unitOfwork;
      _mapper = mapper;
      _lawyerExpertisementRepository = lawyerExpertisementRepository;
    }
    public async Task<ApiResult<LawyerExpertisementDto>> Handle(CreateLawyerExpertisementCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling CreateLawyerExpertisementCommand");
      try
      {
        var entity = new Domain.Entities.LawyerExpertisement
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          ExpertisementId = request.ExpertisementId,
          CreatedAt = DateTime.UtcNow,
        };
        _logger.LogInformation("Creating LawyerExpertisement entity for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        await _lawyerExpertisementRepository.CreateAsync(entity);
        _logger.LogInformation("Saving changes to the database for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        await _unitOfwork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("LawyerExpertisement created successfully with Id: {LawyerExpertisementId}", entity.Id);
        return ApiResult<LawyerExpertisementDto>.Success(_mapper.Map<LawyerExpertisementDto>(entity));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating LawyerExpertisement for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<LawyerExpertisementDto>.Fail("An unexpected error occurred");
      }
    }
  }
}
