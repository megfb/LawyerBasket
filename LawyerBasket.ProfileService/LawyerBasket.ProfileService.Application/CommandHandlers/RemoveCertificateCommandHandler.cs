using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class RemoveCertificateCommandHandler : IRequestHandler<RemoveCertificateCommand, ApiResult>
  {
    private readonly ILogger<RemoveCertificateCommandHandler> _logger;
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveCertificateCommandHandler(ILogger<RemoveCertificateCommandHandler> logger, ICertificateRepository certificateRepository, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _certificateRepository = certificateRepository;
      _unitOfWork = unitOfWork;
    }
    public async Task<ApiResult> Handle(RemoveCertificateCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling RemoveCertificateCommand");
      try
      {
        var certificate = await _certificateRepository.GetByIdAsync(request.Id);
        if (certificate == null)
        {
          _logger.LogWarning("Certificate with Id {Id} not found", request.Id);
          return ApiResult.Fail("Certificate not found");
        }
        _logger.LogInformation("Removing Certificate with Id {Id}", request.Id);
        _certificateRepository.Delete(certificate);
        _logger.LogInformation("Saving changes to the database for Certificate Id {Id}", request.Id);
        await _unitOfWork.SaveChangesAsync();
        return ApiResult.Success(System.Net.HttpStatusCode.NoContent);
      }
      catch (Exception)
      {
        _logger.LogError("Error occurred while handling RemoveCertificateCommand for Id {Id}", request.Id);
        return ApiResult.Fail("An error occurred while processing your request");
      }
    }
  }
}
