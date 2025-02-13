using BlazorApp.Models;

namespace BlazorApp.IService
{
    public interface ICustomerService
    {
        Task<(List<Customer> Customers, long TotalCount)> GetAllCustomersAsync(int pageNumber, int pageSize);
        Task<Customer> GetCustomerByIdAsync(string id);
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(string id, Customer customer);
        Task DeleteCustomerAsync(string id);
    }
}
