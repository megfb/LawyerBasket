using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, ApiResult<AddressDto>>
  {
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAddressQueryHandler> _logger;
    public GetAddressQueryHandler(IAddressRepository addressRepository, IMapper mapper, ILogger<GetAddressQueryHandler> logger)
    {
      _mapper = mapper;
      _logger = logger;
      _addressRepository = addressRepository;
    }
    async Task<ApiResult<AddressDto>> IRequestHandler<GetAddressQuery, ApiResult<AddressDto>>.Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("Handler is started");
        var address = await _addressRepository.GetByIdAsync(request.Id);

        if (address is null)
        {
          _logger.LogError("Address not found");
          return ApiResult<AddressDto>.Fail("Address not found");
        }

        var addressDto = _mapper.Map<AddressDto>(address);

        _logger.LogError("Handler is successful");
        return ApiResult<AddressDto>.Success(addressDto);

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while getting address");
        return ApiResult<AddressDto>.Fail("Error occurred while getting address");
      }

    }
  }
}
