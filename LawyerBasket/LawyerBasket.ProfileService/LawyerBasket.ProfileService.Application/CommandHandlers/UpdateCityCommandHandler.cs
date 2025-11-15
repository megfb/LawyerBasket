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
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, ApiResult<CityDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCityCommandHandler> _logger;
        private readonly IMapper _mapper;
        
        public UpdateCityCommandHandler(
            ICityRepository cityRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<UpdateCityCommandHandler> logger, 
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<CityDto>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateCityCommandHandler Handle method invoked for Id: {Id}", request.Id);
            try
            {
                var city = await _cityRepository.GetByIdAsync(request.Id);
                if (city == null)
                {
                    _logger.LogWarning("City with Id: {CityId} not found.", request.Id);
                    return ApiResult<CityDto>.Fail("City not found", System.Net.HttpStatusCode.NotFound);
                }
                
                city.Name = request.Name;
                city.UpdatedAt = request.UpdatedAt;
                
                _logger.LogInformation("Updating city with Id: {CityId}.", request.Id);
                _cityRepository.Update(city);
                
                _logger.LogInformation("Saving changes to the database for CityId: {CityId}.", request.Id);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation("City with Id: {CityId} updated successfully.", request.Id);
                return ApiResult<CityDto>.Success(_mapper.Map<CityDto>(city));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating city with Id: {CityId}.", request.Id);
                return ApiResult<CityDto>.Fail("An unexpected error occurred");
            }
        }
    }
}

