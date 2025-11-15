using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetGenderQueryHandler : IRequestHandler<GetGenderQuery, ApiResult<GenderDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetGenderQueryHandler> _logger;
        private readonly IGenderRepository _genderRepository;
        
        public GetGenderQueryHandler(
            IGenderRepository genderRepository, 
            IMapper mapper, 
            ILogger<GetGenderQueryHandler> logger)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<ApiResult<GenderDto>> Handle(GetGenderQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetGenderQuery for Id: {Id}", request.Id);
            try
            {
                var gender = await _genderRepository.GetByIdAsync(request.Id);
                if (gender == null)
                {
                    _logger.LogWarning("Gender with Id: {Id} not found", request.Id);
                    return ApiResult<GenderDto>.Fail($"Gender with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
                }
                
                var genderDto = _mapper.Map<GenderDto>(gender);
                _logger.LogInformation("Successfully retrieved Gender with Id: {Id}", request.Id);
                return ApiResult<GenderDto>.Success(genderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetGenderQuery for Id: {Id}", request.Id);
                return ApiResult<GenderDto>.Fail("An error occurred while processing your request.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

