using CarRentalApp.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace CarRentalApp.Services
{
    public class AuthService
    {
        private readonly LocalStorageService _localStorage;

        public User CurrentUser { get; private set; }

        public AuthService(LocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task LoadUserAsync()
        {
            CurrentUser = await _localStorage.GetItemAsync<User>("loggedUser");
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("loggedUser");
            CurrentUser = null;
        }
    }
}
