using LawyerBasket.ProfileService.Application.Contracts.Infrastructure;
using LawyerBasket.ProfileService.Domain.Entities;
using LawyerBasket.Shared.Messaging.Events;
using MassTransit;

namespace LawyerBasket.ProfileService.Api
{
  public class UserProfileCreatedConsumer : IConsumer<UserProfileCreatedEvent>
  {
    private readonly ILogger<UserProfileCreatedConsumer> _logger;
    private readonly IElasticSearchService<UserProfileES> _elasticSearchService;
    public UserProfileCreatedConsumer(ILogger<UserProfileCreatedConsumer> logger, IElasticSearchService<UserProfileES> elasticSearchService)
    {
      _logger = logger;
      _elasticSearchService = elasticSearchService;
    }
    public async Task Consume(ConsumeContext<UserProfileCreatedEvent> context)
    {

      var userProfileES = new UserProfileES
      {
        Id = context.Message.Id,
        FirstName = context.Message.FirstName,
        LastName = context.Message.LastName,
        Email = context.Message.Email,
        UserType = context.Message.UserType.ToString(),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = null
      };
      await _elasticSearchService.CreateOrUpdateAsync(userProfileES, "profiles");

      //_logger.LogInformation("UserProfileCreatedEvent consumed: {FirstName} {LastName} {Email} {UserType}",
      //  context.Message.FirstName,
      //  context.Message.LastName,
      //  context.Message.Email,
      //  context.Message.UserType);
      //return Task.CompletedTask;
    }
  }
}
