using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ApiResult<AddressDto>>
  {
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAddressCommandHandler> _logger;
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateAddressCommandHandler(IMapper mapper, ILogger<UpdateAddressCommandHandler> logger, IAddressRepository addressRepository, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _logger = logger;
      _addressRepository = addressRepository;
      _unitOfWork = unitOfWork;
    }
    public async Task<ApiResult<AddressDto>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {

      _logger.LogInformation("Handling UpdateAddressCommand for AddressId: {AddressId}", request.Id);
      try
      {
        var address = await _addressRepository.GetByIdAsync(request.Id);
        if (address is null)
        {
          _logger.LogWarning("Address with Id: {AddressId} not found", request.Id);
          return ApiResult<AddressDto>.Fail("Address not found");
        }

        address.AddressLine = request.AddressLine;
        address.CityId = request.CityId;
        address.UpdatedAt = DateTime.UtcNow;


        _logger.LogInformation("Address updated");
        _addressRepository.Update(address);

        _logger.LogInformation("Saving changes to database");
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Mapping Address entity to AddressDto");
        var addressDto = _mapper.Map<AddressDto>(address);

        _logger.LogInformation("Successfully updated Address with Id: {AddressId}", request.Id);
        return ApiResult<AddressDto>.Success(addressDto);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while updating Address with Id: {AddressId}", request.Id);
        return ApiResult<AddressDto>.Fail("An error occurred while updating the address.");
      }
    }
  }
}
