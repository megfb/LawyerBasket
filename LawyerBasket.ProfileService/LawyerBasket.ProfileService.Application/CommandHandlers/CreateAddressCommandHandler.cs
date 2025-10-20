using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResult<string>>
  {
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAddressCommandHandler> _logger;
    public CreateAddressCommandHandler(IAddressRepository addressRepository, IUnitOfWork unitOfWork, ILogger<CreateAddressCommandHandler> logger)
    {
      _addressRepository = addressRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task<ApiResult<string>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateAddress started. UserProfileId: {UserProfileId}", request.UserProfileId);
        var entity = new Address
        {
          Id = Guid.NewGuid().ToString(),
          UserProfileId = request.UserProfileId,
          AddressLine = request.AddressLine,
          CityId = request.CityId,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _addressRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResult<string>.Success(entity.Id);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating address for user {UserProfileId}", request.UserProfileId);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


