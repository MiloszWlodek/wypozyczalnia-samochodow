using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CarRentalApp.Shared.Models;

namespace CarRentalApp.Services
{
    public class ReservationService
    {
        private readonly HttpClient _http;
        public ReservationService(HttpClient http) => _http = http;

        public async Task<List<CarDto>> GetAvailableCarsAsync()
        {
            return await _http.GetFromJsonAsync<List<CarDto>>("api/cars")
                   ?? new List<CarDto>();
        }

        public async Task<bool> CreateReservationAsync(ReservationDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/reservations", dto);
            return res.IsSuccessStatusCode;
        }

        public async Task<List<ReservationDto>> GetReservationsByUserAsync(int userId)
        {
            return await _http.GetFromJsonAsync<List<ReservationDto>>(
                       $"api/reservations/user/{userId}")
                   ?? new List<ReservationDto>();
        }

        public async Task<bool> CancelReservationAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/reservations/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
