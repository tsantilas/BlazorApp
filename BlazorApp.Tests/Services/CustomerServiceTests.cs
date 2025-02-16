using BlazorApp.Service;
using BlazorApp.DbContext;
using BlazorApp.Models;
using Moq;
using Xunit;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<IMongoDbContext> _mockDbContext;
        private readonly Mock<IMongoCollection<Customer>> _mockCollection;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockDbContext = new Mock<IMongoDbContext>();
            _mockCollection = new Mock<IMongoCollection<Customer>>();

            _mockDbContext.Setup(db => db.Customers).Returns(_mockCollection.Object);

            _customerService = new CustomerService(_mockDbContext.Object);
        }

        // 🚀 Test: GetAllCustomersAsync - Returns Correct Data
        [Fact]
        public async Task GetAllCustomersAsync_ReturnsCorrectData()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = "1", CompanyName = "Company A" },
                new Customer { Id = "2", CompanyName = "Company B" }
            };

            var mockCursor = MockCursorWithData(customers);

            _mockCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                It.IsAny<FindOptions<Customer>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor);

            _mockCollection.Setup(c => c.CountDocumentsAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                It.IsAny<CountOptions>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers.Count);

            // Act
            var result = await _customerService.GetAllCustomersAsync(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Customers.Count);
            Assert.Equal("Company A", result.Customers[0].CompanyName);
        }

        // 🚀 Test: GetCustomerByIdAsync - Returns Correct Customer
        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsCorrectCustomer()
        {
            // Arrange
            var customerId = "1";
            var customer = new Customer { Id = customerId, CompanyName = "Company A" };

            var mockCursor = MockCursorWithData(new List<Customer> { customer });

            _mockCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                It.IsAny<FindOptions<Customer>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Company A", result.CompanyName);
        }

        // 🚀 Test: CreateCustomerAsync - Successfully Inserts Customer
        [Fact]
        public async Task CreateCustomerAsync_InsertsCustomer()
        {
            // Arrange
            var newCustomer = new Customer { Id = "3", CompanyName = "Company C" };

            _mockCollection.Setup(c => c.InsertOneAsync(newCustomer, null, default))
                           .Returns(Task.CompletedTask);

            // Act
            await _customerService.CreateCustomerAsync(newCustomer);

            // Assert
            _mockCollection.Verify(c => c.InsertOneAsync(newCustomer, null, default), Times.Once);
        }

        // 🚀 Test: UpdateCustomerAsync - Successfully Updates Customer
        [Fact]
        public async Task UpdateCustomerAsync_UpdatesExistingCustomer()
        {
            // Arrange
            var customerId = "1";
            var updatedCustomer = new Customer { Id = customerId, CompanyName = "Updated Company" };

            _mockCollection.Setup(c => c.ReplaceOneAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                updatedCustomer,
                It.IsAny<ReplaceOptions>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ReplaceOneResult.Acknowledged(1, 1, customerId));

            // Act
            await _customerService.UpdateCustomerAsync(customerId, updatedCustomer);

            // Assert
            _mockCollection.Verify(c => c.ReplaceOneAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                updatedCustomer,
                It.IsAny<ReplaceOptions>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        // 🚀 Test: DeleteCustomerAsync - Successfully Deletes Customer
        [Fact]
        public async Task DeleteCustomerAsync_DeletesExistingCustomer()
        {
            // Arrange
            var customerId = "1";

            _mockCollection.Setup(c => c.DeleteOneAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteResult.Acknowledged(1));

            // Act
            await _customerService.DeleteCustomerAsync(customerId);

            // Assert
            _mockCollection.Verify(c => c.DeleteOneAsync(
                It.IsAny<FilterDefinition<Customer>>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        // 🚀 Helper method to create a mock MongoDB cursor
        private static IAsyncCursor<Customer> MockCursorWithData(List<Customer> customers)
        {
            var mockCursor = new Mock<IAsyncCursor<Customer>>();
            mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                      .Returns(true)
                      .Returns(false);
            mockCursor.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true)
                      .ReturnsAsync(false);
            mockCursor.Setup(c => c.Current).Returns(customers);
            return mockCursor.Object;
        }
    }
}
