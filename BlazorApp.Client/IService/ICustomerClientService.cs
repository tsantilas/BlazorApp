﻿using BlazorApp.Client.Models;
using System.Threading.Tasks;

namespace BlazorApp.Client.IService
{
    public interface ICustomerClientService
    {
        Task<(List<CustomerDto>, int)> GetCustomersAsync(int pageNumber, int pageSize);
        Task<CustomerDto> GetCustomerByIdAsync(string id);
        Task CreateCustomerAsync(CustomerDto customer);
        Task UpdateCustomerAsync(CustomerDto customer);
        Task DeleteCustomerAsync(string customerId);
    }
}
