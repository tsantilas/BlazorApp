using System.Net.Http.Json;
using BlazorApp.Client.IService;
using BlazorApp.Client.Models;

namespace BlazorApp.Client.Services
{
    public class CustomerClientService : ICustomerClientService
    {
        private readonly HttpClient _http;
        private readonly string _apiBaseUrl;

        public CustomerClientService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task CreateCustomerAsync(CustomerDto customer)
        {
            await _http.PostAsJsonAsync($"{_apiBaseUrl}/api/Customers", customer);
        }

        public async Task UpdateCustomerAsync(CustomerDto customer)
        {
            await _http.PutAsJsonAsync($"{_apiBaseUrl}/api/Customers/{customer.Id}", customer);
        }

        public async Task DeleteCustomerAsync(string customerId)
        {
            await _http.DeleteAsync($"{_apiBaseUrl}/api/Customers/{customerId}");
        }

    }
}
