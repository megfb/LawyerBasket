using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, ApiResult<List<CityDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCitiesQueryHandler> _logger;
        private readonly ICityRepository _cityRepository;
        
        public GetCitiesQueryHandler(
            ICityRepository cityRepository, 
            ILogger<GetCitiesQueryHandler> logger, 
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<List<CityDto>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetCitiesQuery");
            try
            {
                var cities = await _cityRepository.GetAllAsync();
                var cityDtos = _mapper.Map<List<CityDto>>(cities);
                _logger.LogInformation("Successfully retrieved {Count} cities", cityDtos.Count);
                return ApiResult<List<CityDto>>.Success(cityDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cities");
                return ApiResult<List<CityDto>>.Fail("An error occurred while processing your request");
            }
        }
    }
}

