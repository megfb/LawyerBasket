using LawyerBasket.ProfileService.Api.Extensions;
using LawyerBasket.ProfileService.Application.Extensions;
using LawyerBasket.ProfileService.Data;
using LawyerBasket.ProfileService.Data.Extensions;
using LawyerBasket.ProfileService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddRepositories(builder.Configuration).AddApplication(builder.Configuration).AddInfrastructure(builder.Configuration).AddApiServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
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
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate(); // Eksik tablolar varsa oluþturur
//}
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
