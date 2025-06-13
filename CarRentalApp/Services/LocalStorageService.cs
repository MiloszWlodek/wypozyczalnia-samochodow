using Microsoft.JSInterop;
using System.Text.Json;

namespace CarRentalApp.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _js;
        public LocalStorageService(IJSRuntime js) => _js = js;

        public async Task SetItemAsync<T>(string key, T item)
        {
            var json = JsonSerializer.Serialize(item);
            await _js.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", key);
            return json is null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task RemoveItemAsync(string key)
            => await _js.InvokeVoidAsync("localStorage.removeItem", key);
    }
}
