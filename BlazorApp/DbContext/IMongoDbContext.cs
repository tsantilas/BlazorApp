using BlazorApp.Models;
using MongoDB.Driver;

namespace BlazorApp.DbContext
{
    public interface IMongoDbContext
    {
        IMongoCollection<Customer> Customers { get; }
    }
}
