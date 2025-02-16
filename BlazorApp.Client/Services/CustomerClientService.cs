using System.Net.Http;
using System.Net.Http.Json;
using BlazorApp.Client.IService;
using BlazorApp.Client.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Services
{
    public class CustomerClientService : ICustomerClientService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public CustomerClientService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _http = httpClient;
            _navigationManager = navigationManager;

            _http.BaseAddress = new Uri(_navigationManager.BaseUri);
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
