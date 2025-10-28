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
  public class UpdateAcademyCommandHandler : IRequestHandler<UpdateAcademyCommand, ApiResult<AcademyDto>>
  {
    private readonly IAcademyRepository _academyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAcademyCommandHandler> _logger;
    private readonly IMapper _mapper;
    public UpdateAcademyCommandHandler(IAcademyRepository academyRepository, IUnitOfWork unitOfWork, ILogger<UpdateAcademyCommandHandler> logger, IMapper mapper)
    {
      _academyRepository = academyRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<AcademyDto>> Handle(UpdateAcademyCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("UpdateAcademyCommandHandler Handle method invoked.");
      try
      {
        var academy = await _academyRepository.GetByIdAsync(request.Id);
        if (academy == null)
        {
          _logger.LogWarning("Academy with Id: {AcademyId} not found.", request.Id);
          return ApiResult<AcademyDto>.Fail("Academy not found", System.Net.HttpStatusCode.NotFound);
        }
        academy.University = request.University;
        academy.Degree = request.Degree;
        academy.StartDate = request.StartDate;
        academy.EndDate = request.EndDate;
        academy.UpdatedAt = request.UpdatedAt;
        _logger.LogInformation("Updating academy with Id: {AcademyId}.", request);
        _academyRepository.Update(academy);
        _logger.LogInformation("Saving changes to the database for AcademyId: {AcademyId}.", request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Academy with Id: {AcademyId} updated successfully.", request.Id);
        return ApiResult<AcademyDto>.Success(_mapper.Map<AcademyDto>(academy));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error updating academy with Id: {AcademyId}.", request.Id);
        return ApiResult<AcademyDto>.Fail("An unexpected error occurred");
      }
    }
  }
}
