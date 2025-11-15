using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetCityQueryHandler : IRequestHandler<GetCityQuery, ApiResult<CityDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCityQueryHandler> _logger;
        private readonly ICityRepository _cityRepository;
        
        public GetCityQueryHandler(
            ICityRepository cityRepository, 
            IMapper mapper, 
            ILogger<GetCityQueryHandler> logger)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<ApiResult<CityDto>> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetCityQuery for Id: {Id}", request.Id);
            try
            {
                var city = await _cityRepository.GetByIdAsync(request.Id);
                if (city == null)
                {
                    _logger.LogWarning("City with Id: {Id} not found", request.Id);
                    return ApiResult<CityDto>.Fail($"City with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
                }
                
                var cityDto = _mapper.Map<CityDto>(city);
                _logger.LogInformation("Successfully retrieved City with Id: {Id}", request.Id);
                return ApiResult<CityDto>.Success(cityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetCityQuery for Id: {Id}", request.Id);
                return ApiResult<CityDto>.Fail("An error occurred while processing your request.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

