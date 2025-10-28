using AutoMapper;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Application.Queries;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.QueryHandlers
{
  public class GetContactQueryHandler : IRequestHandler<GetContactQuery, ApiResult<ContactDto>>
  {
    private readonly IMapper _mapper;
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<GetContactQueryHandler> _logger;
    public GetContactQueryHandler(ILogger<GetContactQueryHandler> logger, IContactRepository contactRepository, IMapper mapper)
    {
      _logger = logger;
      _contactRepository = contactRepository;
      _mapper = mapper;
    }
    public async Task<ApiResult<ContactDto>> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling GetContactQuery for Id: {Id}", request.Id);
      try
      {
        var contact = await _contactRepository.GetByIdAsync(request.Id);
        if (contact == null)
        {
          _logger.LogWarning("Contact with Id: {Id} not found", request.Id);
          return ApiResult<ContactDto>.Fail($"Contact with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
        }
        var contactDto = _mapper.Map<ContactDto>(contact);
        _logger.LogInformation("Successfully retrieved Contact with Id: {Id}", request.Id);
        return ApiResult<ContactDto>.Success(contactDto, System.Net.HttpStatusCode.OK);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while retrieving Contact with Id: {Id}", request.Id);
        return ApiResult<ContactDto>.Fail("An error occurred while retrieving the contact.", System.Net.HttpStatusCode.InternalServerError);
      }
    }
  }
}
