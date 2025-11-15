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
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, ApiResult<CityDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCityCommandHandler> _logger;
        private readonly IMapper _mapper;
        
        public CreateCityCommandHandler(
            ICityRepository cityRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<CreateCityCommandHandler> logger, 
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<CityDto>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateCity started. Name: {Name}", request.Name);
            try
            {
                var entity = new City
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    CreatedAt = DateTime.UtcNow,
                };
                
                _logger.LogInformation("Creating city entity with Name: {Name}", request.Name);
                await _cityRepository.CreateAsync(entity);

                _logger.LogInformation("Saving changes to the database for city with Name: {Name}", request.Name);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("City created successfully with Id: {CityId}", entity.Id);
                return ApiResult<CityDto>.Success(_mapper.Map<CityDto>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating city with Name: {Name}", request.Name);
                return ApiResult<CityDto>.Fail("An unexpected error occurred");
            }
        }
    }
}

