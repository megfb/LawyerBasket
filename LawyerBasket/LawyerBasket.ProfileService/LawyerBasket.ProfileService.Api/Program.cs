using LawyerBasket.ProfileService.Api;
using LawyerBasket.ProfileService.Api.Extensions;
using LawyerBasket.ProfileService.Application.Extensions;
using LawyerBasket.ProfileService.Data;
using LawyerBasket.ProfileService.Data.Extensions;
using LawyerBasket.ProfileService.Infrastructure.Extensions;
using LawyerBasket.ProfileService.Worker.Extensions;
using LawyerBasket.Shared.Messaging.MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddRepositories(builder.Configuration).AddApplication(builder.Configuration).AddInfrastructure(builder.Configuration)
  .AddApiServices(builder.Configuration).AddWorkerServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRabbitMqConsumer<TestConsumer>(queueName: "queue-profile", routingKey: "route.profileservice", exchangeName: "AuthServiceExchange");
builder.Services.AddRabbitMqInfrastructure();
// Add CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngularApp", policy =>
  {
    if (builder.Environment.IsDevelopment())
    {
      // Development: Allow all origins for easier testing
      policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    }
    else
    {
      // Production: Specific origins only
      policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    }
  });
});

var app = builder.Build();

// Apply pending migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        var pendingMigrations = context.Database.GetPendingMigrations();
        
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Applying {Count} pending migration(s)...", pendingMigrations.Count());
            foreach (var migration in pendingMigrations)
            {
                logger.LogInformation("Pending migration: {MigrationName}", migration);
            }
            
            context.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
        }
        else
        {
            logger.LogInformation("No pending migrations. Database is up to date.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying migrations.");
        throw;
    }
}

// Configure the HTTP request pipeline.

// Use CORS first - Must be before any other middleware that might send responses
app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  // Only use HTTPS redirection in production
  app.UseHttpsRedirection();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
