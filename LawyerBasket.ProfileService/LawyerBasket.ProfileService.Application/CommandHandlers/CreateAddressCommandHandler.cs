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
  public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResult<AddressDto>>
  {
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAddressCommandHandler> _logger;
    private readonly IMapper _mapper;
    public CreateAddressCommandHandler(IMapper mapper, IAddressRepository addressRepository, IUnitOfWork unitOfWork, ILogger<CreateAddressCommandHandler> logger)
    {
      _addressRepository = addressRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
      _mapper = mapper;
    }

    public async Task<ApiResult<AddressDto>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
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
        };

        _logger.LogInformation("Creating address entity for UserProfileId: {UserProfileId}", request.UserProfileId);
        await _addressRepository.CreateAsync(entity);

        _logger.LogInformation("Saving changes to the database for UserProfileId: {UserProfileId}", request.UserProfileId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("CreateAddress completed. AddressId: {AddressId}", entity.Id);
        return ApiResult<AddressDto>.Success(_mapper.Map<AddressDto>(entity));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating address for user {UserProfileId}", request.UserProfileId);
        return ApiResult<AddressDto>.Fail("An unexpected error occurred");
      }
    }
  }
}


