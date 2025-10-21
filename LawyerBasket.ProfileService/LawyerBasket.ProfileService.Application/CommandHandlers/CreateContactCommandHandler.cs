using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ApiResult<ContactDto>>
  {
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateContactCommandHandler> _logger;
    private readonly IMapper _mapper;
    public CreateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, ILogger<CreateContactCommandHandler> logger, IMapper mapper)
    {
      _contactRepository = contactRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _logger = logger;
    }
    public async Task<ApiResult<ContactDto>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("CreateContact started. LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
      try
      {
        var entity = new Contact
        {
          Id = Guid.NewGuid().ToString(),
          LawyerProfileId = request.LawyerProfileId,
          PhoneNumber = request.PhoneNumber,
          AlternatePhoneNumber = request.AlternatePhoneNumber,
          Email = request.Email,
          AlternateEmail = request.AlternateEmail,
          Website = request.Website,
          CreatedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Creating contact entity for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);
        await _contactRepository.CreateAsync(entity);
        _logger.LogInformation("Saving changes to the database for LawyerProfileId: {LawyerProfileId}", request.LawyerProfileId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("CreateContact completed. ContactId: {ContactId}", entity.Id);
        return ApiResult<ContactDto>.Success(_mapper.Map<ContactDto>(entity));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating contact for lawyer profile {LawyerProfileId}", request.LawyerProfileId);
        return ApiResult<ContactDto>.Fail("An unexpected error occurred");
      }
    }
  }
}


