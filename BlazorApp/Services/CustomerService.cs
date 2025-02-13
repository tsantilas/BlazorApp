using BlazorApp.DbContext;
using BlazorApp.Models;
using BlazorApp.IService;
using MongoDB.Driver;

namespace BlazorApp.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly MongoDbContext _dbContext;

        public CustomerService(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<Customer> Customers, long TotalCount)> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var customers = await _dbContext.Customers
                                            .Find(c => true)
                                            .Skip(skip)
                                            .Limit(pageSize)
                                            .ToListAsync();
            long totalCount = await _dbContext.Customers.CountDocumentsAsync(c => true);
            return (customers, totalCount);
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _dbContext.Customers
                                   .Find(c => c.Id == id)
                                   .FirstOrDefaultAsync();
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.InsertOneAsync(customer);
        }

        public async Task UpdateCustomerAsync(string id, Customer customer)
        {
            await _dbContext.Customers.ReplaceOneAsync(c => c.Id == id, customer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _dbContext.Customers.DeleteOneAsync(c => c.Id == id);
        }
    }
}
