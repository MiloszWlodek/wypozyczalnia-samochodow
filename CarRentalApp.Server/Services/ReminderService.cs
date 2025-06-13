using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CarRentalApp.Server.Models;

namespace CarRentalApp.Server.Services
{
    public class ReminderService : BackgroundService
    {
        private readonly IServiceProvider _sp;

        public ReminderService(IServiceProvider sp) => _sp = sp;

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                using var scope = _sp.CreateScope();
                var email  = scope.ServiceProvider.GetRequiredService<EmailService>();
                var store  = scope.ServiceProvider.GetRequiredService<ReservationStore>();

                var tomorrow = DateTime.UtcNow.Date.AddDays(1);
                var dueList  = store.Reservations
                    .Where(r => r.ReturnDue.Date == tomorrow)
                    .ToList();

                foreach (var r in dueList)
                {
                    var html = $"<p>Przypomnienie: jutro ({r.ReturnDue:yyyy-MM-dd}) musisz oddać auto {r.CarId}.</p>";
                    await email.SendAsync(r.UserEmail, "Przypomnienie o zwrocie", html);
                }

                // odczekujemy 24h
                await Task.Delay(TimeSpan.FromHours(24), ct);
            }
        }
    }
}