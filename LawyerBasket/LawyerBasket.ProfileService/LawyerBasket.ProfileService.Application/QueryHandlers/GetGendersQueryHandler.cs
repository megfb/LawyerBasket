using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, ApiResult<List<GenderDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetGendersQueryHandler> _logger;
        private readonly IGenderRepository _genderRepository;
        
        public GetGendersQueryHandler(
            IGenderRepository genderRepository, 
            ILogger<GetGendersQueryHandler> logger, 
            IMapper mapper)
        {
            _genderRepository = genderRepository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<ApiResult<List<GenderDto>>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetGendersQuery");
            try
            {
                var genders = await _genderRepository.GetAllAsync();
                var genderDtos = _mapper.Map<List<GenderDto>>(genders);
                _logger.LogInformation("Successfully retrieved {Count} genders", genderDtos.Count);
                return ApiResult<List<GenderDto>>.Success(genderDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving genders");
                return ApiResult<List<GenderDto>>.Fail("An error occurred while processing your request");
            }
        }
    }
}

