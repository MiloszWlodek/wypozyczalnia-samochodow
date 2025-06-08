using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Text.Json;
using CarRentalApp.Shared.Models;
using System.Net.Http.Headers;

namespace CarRentalApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private const string TOKEN_KEY = "authToken";
        private const string USER_KEY = "currentUser";

        public User? CurrentUser { get; set; }
        public string? Token { get; private set; }

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

public async Task InitializeAsync()
{
    Token = await _js.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
    var userJson = await _js.InvokeAsync<string>("localStorage.getItem", USER_KEY);

    if (!string.IsNullOrEmpty(userJson))
    {
        CurrentUser = JsonSerializer.Deserialize<User>(userJson)!;
    }

    if (!string.IsNullOrEmpty(Token))
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Token);
    }
}

        public bool IsLoggedIn() => CurrentUser != null;

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/auth/register", dto);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(UserLoginDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/auth/login", dto);
            if (!res.IsSuccessStatusCode) return false;

            var data = await res.Content.ReadFromJsonAsync<LoginResponse>()!;
            Token = data.token;
            CurrentUser = data.user;

            await _js.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, Token);
            await _js.InvokeVoidAsync("localStorage.setItem", USER_KEY,
                JsonSerializer.Serialize(CurrentUser));

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            return true;
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
            await _js.InvokeVoidAsync("localStorage.removeItem", USER_KEY);
            CurrentUser = null;
            Token = null;
            _http.DefaultRequestHeaders.Authorization = null!;
        }

        private class LoginResponse
        {
            public string token { get; set; } = default!;
            public User user { get; set; } = default!;
        }
    }
}
