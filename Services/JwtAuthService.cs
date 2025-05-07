using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using CarRentalApp.Models;

namespace CarRentalApp.Services
{
    public class JwtAuthService
    {
        private readonly HttpClient _http;
        private readonly LocalStorageService _storage;

        public JwtAuthService(HttpClient http, LocalStorageService storage)
        {
            _http = http;
            _storage = storage;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var body = new { username, password };
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            try
            {
                var response = await _http.PostAsync("/api/auth/login", content);
                if (!response.IsSuccessStatusCode) return false;

                var result = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(result);
                var token = json.RootElement.GetProperty("token").GetString();

                await _storage.SetItemAsync("jwt", token);
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login failed: " + ex.Message);
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _storage.RemoveItemAsync("jwt");
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }
}
