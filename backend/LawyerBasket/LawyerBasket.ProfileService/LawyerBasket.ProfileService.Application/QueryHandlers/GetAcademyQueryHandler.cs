using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
    public class GetAcademyQueryHandler : IRequestHandler<GetAcademyQuery, ApiResult<AcademyDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAcademyQueryHandler> _logger;
        private readonly IAcademyRepository _academyRepository;
        public GetAcademyQueryHandler(IAcademyRepository academyRepository, IMapper mapper, ILogger<GetAcademyQueryHandler> logger)
        {
            _academyRepository = academyRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<ApiResult<AcademyDto>> Handle(GetAcademyQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAcademyQuery for Id: {Id}", request.Id);
            try
            {
                var academy = await _academyRepository.GetByIdAsync(request.Id);
                if (academy == null)
                {
                    _logger.LogWarning("Academy with Id: {Id} not found", request.Id);
                    return ApiResult<AcademyDto>.Fail($"Academy with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
                }
                var academyDto = _mapper.Map<AcademyDto>(academy);
                _logger.LogInformation("Successfully retrieved Academy with Id: {Id}", request.Id);
                return ApiResult<AcademyDto>.Success(academyDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GetAcademyQuery for Id: {Id}", request.Id);
                return ApiResult<AcademyDto>.Fail("An error occurred while processing your request.", System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
