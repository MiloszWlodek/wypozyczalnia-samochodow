using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        using var http = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };

        // -- Rejestracja --
        var regDto = new
        {
            username = "testuser",
            email = "testuser@example.com",
            password = "Haslo123!"
        };
        var regResp = await http.PostAsJsonAsync("api/auth/register", regDto);
        Console.WriteLine($"Register: {regResp.StatusCode}");

        // -- Logowanie --
        var loginDto = new
        {
            username = "testuser",
            password = "Haslo123!"
        };
        var loginResp = await http.PostAsJsonAsync("api/auth/login", loginDto);
        Console.WriteLine($"Login: {loginResp.StatusCode}");
        if (loginResp.IsSuccessStatusCode)
        {
            var data = await loginResp.Content.ReadFromJsonAsync<LoginResponse>();
            Console.WriteLine($"Token: {data.token}");
        }
    }

    class LoginResponse
    {
        public string Token { get; set; }
        public object User { get; set; }
    }
}
