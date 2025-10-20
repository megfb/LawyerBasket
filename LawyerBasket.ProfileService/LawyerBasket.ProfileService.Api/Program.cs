using LawyerBasket.ProfileService.Api.Extensions;
using LawyerBasket.ProfileService.Application.Extensions;
using LawyerBasket.ProfileService.Data.Extensions;
using LawyerBasket.ProfileService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddRepositories(builder.Configuration).AddApplication(builder.Configuration).AddInfrastructure(builder.Configuration).AddApiServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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
