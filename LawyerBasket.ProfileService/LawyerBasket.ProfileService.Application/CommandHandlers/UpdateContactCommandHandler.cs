using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ApiResult<ContactDto>>
  {
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateContactCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContactRepository _contactRepository;
    public UpdateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, ILogger<UpdateContactCommandHandler> logger, IMapper mapper)
    {
      _contactRepository = contactRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
      _mapper = mapper;
    }
    public async Task<ApiResult<ContactDto>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling UpdateContactCommand for Contact with Email: {Email}", request.Email);
      try
      {
        var contact = await _contactRepository.GetByIdAsync(request.Id);
        if (contact == null)
        {
          _logger.LogWarning("Contact with Id: {Id} not found", request.Id);
          return ApiResult<ContactDto>.Fail($"Contact with Id: {request.Id} not found", System.Net.HttpStatusCode.NotFound);
        }
        contact.PhoneNumber = request.PhoneNumber;
        contact.AlternatePhoneNumber = request.AlternatePhoneNumber;
        contact.Email = request.Email;
        contact.AlternateEmail = request.AlternateEmail;
        contact.Website = request.Website;
        contact.UpdatedAt = request.UpdatedAt;

        _logger.LogInformation("Updating Contact with Id: {Id}", request.Id);
        _contactRepository.Update(contact);

        _logger.LogInformation("Saving changes for Contact with Id: {Id}", request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Contact with Id: {Id} updated successfully", request.Id);
        var contactDto = _mapper.Map<ContactDto>(contact);

        return ApiResult<ContactDto>.Success(contactDto);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error updating Contact with Id: {Id}", request.Id);
        return ApiResult<ContactDto>.Fail("An error occurred while updating the contact.");

      }
    }
  }
}
