using CarRentalApp.Server.Data;
using CarRentalApp.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientApp", policy =>
    {
        policy
          .WithOrigins("https://localhost:54099")
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("CarRentalDb"));

builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = TokenService.GetValidationParameters();
    });

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowClientApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
