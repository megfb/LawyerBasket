using LawyerBasket.ProfileService.Application.Commands;
using LawyerBasket.Shared.Common.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LawyerBasket.ProfileService.Application.CommandHandlers
{
  public class CreateUserProfileOrchestratorCommandHandler : IRequestHandler<CreateUserProfileOrchestratorCommand, ApiResult<string>>
  {
    private readonly IMediator _mediator;
    private readonly ILogger<CreateUserProfileOrchestratorCommandHandler> _logger;
    public CreateUserProfileOrchestratorCommandHandler(IMediator mediator, ILogger<CreateUserProfileOrchestratorCommandHandler> logger)
    {
      _mediator = mediator;
      _logger = logger;
    }

    public async Task<ApiResult<string>> Handle(CreateUserProfileOrchestratorCommand request, CancellationToken cancellationToken)
    {
      try
      {
        _logger.LogInformation("UserProfile orchestration started for {Email}", request.User.Email);

        // 1) Create user profile
        var userResult = await _mediator.Send(request.User, cancellationToken);
        if (userResult.IsFail) return ApiResult<string>.Fail(userResult.ErrorMessage);
        var userId = userResult.Data.Id;

        // 2) Address
        if (request.Address != null)
        {
          request.Address.UserProfileId = userId;
          var addr = await _mediator.Send(request.Address, cancellationToken);
          if (addr.IsFail) return ApiResult<string>.Fail(addr.ErrorMessage);
        }

        string? lawyerProfileId = null;
        if (request.LawyerProfile != null)
        {
          request.LawyerProfile.UserProfileId = userId;
          var lw = await _mediator.Send(request.LawyerProfile, cancellationToken);
          if (lw.IsFail) return ApiResult<string>.Fail(lw.ErrorMessage);
          lawyerProfileId = lw.Data.Id;
        }

        if (lawyerProfileId != null)
        {
          if (request.Contacts != null)
          {
            foreach (var c in request.Contacts)
            {
              c.LawyerProfileId = lawyerProfileId;
              var res = await _mediator.Send(c, cancellationToken);
              if (res.IsFail) return ApiResult<string>.Fail(res.ErrorMessage);
            }
          }

          if (request.Experiences != null)
          {
            foreach (var e in request.Experiences)
            {
              e.LawyerProfileId = lawyerProfileId;
              var res = await _mediator.Send(e, cancellationToken);
              if (res.IsFail) return ApiResult<string>.Fail(res.ErrorMessage);
            }
          }

          if (request.Academies != null)
          {
            foreach (var a in request.Academies)
            {
              a.LawyerProfileId = lawyerProfileId;
              var res = await _mediator.Send(a, cancellationToken);
              if (res.IsFail) return ApiResult<string>.Fail(res.ErrorMessage);
            }
          }

          if (request.Certificates != null)
          {
            foreach (var cert in request.Certificates)
            {
              cert.LawyerProfileId = lawyerProfileId;
              var res = await _mediator.Send(cert, cancellationToken);
              if (res.IsFail) return ApiResult<string>.Fail(res.ErrorMessage);
            }
          }

          if (request.Expertisements != null)
          {
            foreach (var ex in request.Expertisements)
            {
              ex.LawyerProfileId = lawyerProfileId;
              var res = await _mediator.Send(ex, cancellationToken);
              if (res.IsFail) return ApiResult<string>.Fail(res.ErrorMessage);
            }
          }
        }

        _logger.LogInformation("UserProfile orchestration completed. UserId: {UserId}", userId);
        return ApiResult<string>.Success(userId);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred during user profile orchestration: {Email}", request.User.Email);
        return ApiResult<string>.Fail("An unexpected error occurred");
      }
    }
  }
}


