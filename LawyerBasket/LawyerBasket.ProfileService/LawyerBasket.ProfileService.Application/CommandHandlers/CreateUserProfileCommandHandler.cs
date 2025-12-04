using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Api;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Common.Domain;
using LawyerBasket.Shared.Common.Response;
using LawyerBasket.Shared.Messaging.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, ApiResult<UserProfileDto>>
  {
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserProfileCommandHandler> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IOutboxMessageRepository _outboxMessageRepository;

    public CreateUserProfileCommandHandler(ICurrentUserService currentUserService, IUserProfileRepository userProfileRepository,
      IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateUserProfileCommandHandler> logger, IOutboxMessageRepository outboxMessageRepository)
    {
      _userProfileRepository = userProfileRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
      _logger = logger;
      _currentUserService = currentUserService;
      _outboxMessageRepository = outboxMessageRepository;
    }

    public async Task<ApiResult<UserProfileDto>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("CreateUserProfile started. Email: {Email}", request.Email);

        if (await _userProfileRepository.AnyByEmail(request.Email))
        {
          _logger.LogWarning("Attempt to create duplicate user profile: {Email}", request.Email);
          return ApiResult<UserProfileDto>.Fail("Email already exists");
        }

        var entity = new UserProfile
        {
          Id = _currentUserService.UserId!,
          FirstName = request.FirstName,
          LastName = request.LastName,
          Email = request.Email,
          PhoneNumber = request.PhoneNumber,
          GenderId = request.GenderId,
          BirthDate = request.BirthDate,
          NationalId = request.NationalId,
          UserType = Domain.Entities.UserType.Lawyer,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        };
        await _userProfileRepository.CreateAsync(entity);
        var outboxMessage = new OutboxMessage
        {
          Id = Guid.NewGuid().ToString(),
          Type = typeof(UserProfileCreatedEvent).AssemblyQualifiedName!,
          Payload = System.Text.Json.JsonSerializer.Serialize(new
          {
            Id = entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.UserType
          }),
          CreatedAt = DateTime.UtcNow,
          AggregateId = entity.Id,
          Error = null,
          ProcessedAt = null,
          Status = Status.Pending

        };
        await _outboxMessageRepository.CreateAsync(outboxMessage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("CreateUserProfile completed. Id: {Id}", entity.Id);
        return ApiResult<UserProfileDto>.Success(_mapper.Map<UserProfileDto>(entity));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while creating user profile: {Email}", request.Email);
        return ApiResult<UserProfileDto>.Fail("An unexpected error occurred");
      }
    }
  }
}


