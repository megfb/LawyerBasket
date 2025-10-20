using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ApiResult<string>>
  {
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateContactCommandHandler> _logger;
    public CreateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, ILogger<CreateContactCommandHandler> logger)
    {
      _contactRepository = contactRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateContact started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        var entity = new Contact
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          PhoneNumber = request.PhoneNumber,
          AlternatePhoneNumber = request.AlternatePhoneNumber,
          Email = request.Email,
          AlternateEmail = request.AlternateEmail,
          Website = request.Website,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _contactRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating contact for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


