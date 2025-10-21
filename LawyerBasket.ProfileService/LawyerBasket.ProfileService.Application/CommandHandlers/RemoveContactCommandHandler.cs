using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand, ApiResult>
  {
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<RemoveContactCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveContactCommandHandler(IContactRepository contactRepository, ILogger<RemoveContactCommandHandler> logger, IUnitOfWork unitOfWork)
    {
      _contactRepository = contactRepository;
      _logger = logger;
      _unitOfWork = unitOfWork;
    }
    public async Task<ApiResult> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling RemoveContactCommand for Id: {Id}", request.Id);
      try
      {
        var contact = await _contactRepository.GetByIdAsync(request.Id);
        if (contact == null)
        {
          _logger.LogWarning("Contact with Id: {Id} not found", request.Id);
          return ApiResult.Fail($"Contact with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
        }
        _logger.LogInformation("Removing Contact with Id: {Id}", request.Id);
        _contactRepository.Delete(contact);
        _logger.LogInformation("Saving changes to the database for Contact Id: {Id}", request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Successfully removed Contact with Id: {Id}", request.Id);
        return ApiResult.Success(System.Net.HttpStatusCode.OK);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while removing Contact with Id: {Id}", request.Id);
        return ApiResult.Fail("An error occurred while removing the contact.", System.Net.HttpStatusCode.InternalServerError);
      }
    }
  }
}
