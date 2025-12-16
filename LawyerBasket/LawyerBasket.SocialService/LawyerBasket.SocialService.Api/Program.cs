using LawyerBasket.SocialService.Api.Domain.Repositories.EntityFramework.DbContexts;
using LawyerBasket.SocialService.Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices(builder.Configuration).AddJwtSwagger(builder.Configuration);
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
