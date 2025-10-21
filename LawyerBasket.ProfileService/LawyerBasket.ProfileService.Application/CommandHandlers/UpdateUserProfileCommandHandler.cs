using AutoMapper;
using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.ProfileService.Application.Contracts.Data;
using LawyerBasket.ProfileService.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, ApiResult<UserProfileDto>>
  {
    private readonly ILogger<UpdateUserProfileCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IMapper _mapper;
    public UpdateUserProfileCommandHandler(IMapper mapper, ILogger<UpdateUserProfileCommandHandler> logger, IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
      _userProfileRepository = userProfileRepository;
      _mapper = mapper;

    }
    public async Task<ApiResult<UserProfileDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {

      try
      {

        _logger.LogInformation("UpdateUserProfileCommandHandler handle started.");

        var user = await _userProfileRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
          _logger.LogWarning("User not found. UserId: {UserId}", request.Id);
          return ApiResult<UserProfileDto>.Fail("User not found");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.GenderId = request.GenderId;
        user.BirthDate = request.BirthDate;
        user.NationalId = request.NationalId;
        user.UpdatedAt = DateTime.UtcNow;

        _logger.LogInformation("Updating user profile in repository. UserId: {UserId}", request.Id);
        _userProfileRepository.Update(user);
        _logger.LogInformation("Saving changes to the database.");
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return ApiResult<UserProfileDto>.Success(_mapper.Map<UserProfileDto>(user));
      }
      catch (Exception ex)
      {
        _logger.LogWarning(ex, "An error occurred while updating user profile. UserId: {UserId}", request.Id);
        return ApiResult<UserProfileDto>.Fail("An unexpected error occurred");
      }

    }
  }
}
