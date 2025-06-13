using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarRentalApp;
using CarRentalApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<CarRentalApp.App>("#app");

builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri("https://localhost:5001")
});
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<JwtAuthService>();
builder.Services.AddScoped<ReservationService>();

await builder.Build().RunAsync();
