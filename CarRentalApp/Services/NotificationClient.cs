using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRentalApp.Services
{
  public class NotificationClient
  {
    readonly HttpClient _http;
    public NotificationClient(HttpClient http) => _http = http;

    public Task SendReservationConfirmation(string email, string carId)
      => _http.PostAsync(
           $"/api/notify/reservation?userEmail={Uri.EscapeDataString(email)}&carId={Uri.EscapeDataString(carId)}",
           null);

    public Task SendReturnReminder(string email, string carId, string returnDate)
      => _http.PostAsync(
           $"/api/notify/reminder?userEmail={Uri.EscapeDataString(email)}&carId={Uri.EscapeDataString(carId)}&returnDate={Uri.EscapeDataString(returnDate)}",
           null);
  }
}
