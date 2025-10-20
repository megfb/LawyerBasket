using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public UpdateUserProfileCommandHandler(ILogger<UpdateUserProfileCommandHandler> logger, IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
      _userProfileRepository = userProfileRepository;
    }
    public Task<ApiResult<UserProfileDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("UpdateUserProfileCommandHandler handle started.");
      //var userProfile = _userProfileRepository.GetByIdAsync();
      throw new NotImplementedException();
    }
  }
}
