using System.Net.Http;
using System.Net.Http.Json;
using BlazorApp.Client.IService;
using BlazorApp.Client.Models;

namespace BlazorApp.Client.Services
{
    public class CustomerClientService : ICustomerClientService
    {
        private readonly HttpClient _http;

        public CustomerClientService(HttpClient http)
        {
            _http = http;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(string id)
        {
            return await _http.GetFromJsonAsync<CustomerDto>($"api/Customers/{id}");
        }

        public async Task CreateCustomerAsync(CustomerDto customer)
        {
            await _http.PostAsJsonAsync("api/Customers", customer);
        }

        public async Task UpdateCustomerAsync(CustomerDto customer)
        {
            await _http.PutAsJsonAsync($"api/Customers/{customer.Id}", customer);
        }

        public async Task DeleteCustomerAsync(string customerId)
        {
            await _http.DeleteAsync($"api/Customers/{customerId}");
        }
    }
}
