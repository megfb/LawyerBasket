using LawyerBasket.GatewayTest.Aggregators;
using LawyerBasket.GatewayTest.Composes;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureAppConfiguration((ctx, config) =>
{
  config.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
});
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<UserProfileAggregator>();
builder.Services.AddHttpClient<PostAggregator>();
builder.Services.AddScoped<PostAggregator>();
builder.Services.AddScoped<UserProfileAggregator>();
builder.Services.AddScoped<UserProfilePostCompose>();
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapWhen(context =>
    context.Request.Path.StartsWithSegments("/api/UserPost"),
    appBuilder =>
    {
      appBuilder.UseRouting();
      appBuilder.UseAuthorization();
      appBuilder.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers(); // Sadece UserPostController!
      });
    });
await app.UseOcelot();



app.Run();
