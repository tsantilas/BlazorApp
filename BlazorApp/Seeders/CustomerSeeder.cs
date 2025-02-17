using BlazorApp.DbContext;
using BlazorApp.Models;
using MongoDB.Driver;

namespace BlazorApp.Seeders
{
    public class CustomerSeeder
    {
        private readonly IMongoDbContext _context;

        public CustomerSeeder(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            var anyCustomers = await _context.Customers
                .Find(_ => true)
                .AnyAsync();

            if (!anyCustomers)
            {
                var initialCustomers = new List<Customer>
                {
                    new Customer
                    {
                        CompanyName = "Tech Innovations Inc.",
                        ContactName = "Michael Chen",
                        Address = "888 Tech Valley Rd",
                        City = "San Jose",
                        Region = "CA",
                        PostalCode = "95112",
                        Country = "USA",
                        Phone = "+1 408-555-0199"
                    },
                    new Customer
                    {
                        CompanyName = "Global Solutions Ltd.",
                        ContactName = "Sarah Müller",
                        Address = "Kurfürstendamm 123",
                        City = "Berlin",
                        PostalCode = "10719",
                        Country = "Germany",
                        Phone = "+49 30 55501234"
                    },
                    new Customer
                    {
                        CompanyName = "Oceanic Imports",
                        ContactName = "David Tanaka",
                        Address = "1-2-3 Shibuya",
                        City = "Tokyo",
                        PostalCode = "150-0002",
                        Country = "Japan",
                        Phone = "+81 3-5555-6789"
                    },
                    new Customer
                    {
                        CompanyName = "Northern Logistics",
                        ContactName = "Emma Johansson",
                        Address = "Kungsgatan 45",
                        City = "Stockholm",
                        PostalCode = "111 35",
                        Country = "Sweden",
                        Phone = "+46 8-555 123 45"
                    },
                    new Customer
                    {
                        CompanyName = "Desert Solar Co.",
                        ContactName = "Ahmed Al-Mansoori",
                        Address = "Sheikh Zayed Rd",
                        City = "Dubai",
                        PostalCode = "00000",
                        Country = "UAE",
                        Phone = "+971 4-555 1234"
                    },
                    new Customer
                    {
                        CompanyName = "Alpine Adventures",
                        ContactName = "Sophie Martin",
                        Address = "Rue du Mont-Blanc 7",
                        City = "Geneva",
                        PostalCode = "1201",
                        Country = "Switzerland",
                        Phone = "+41 22-555-0199"
                    },
                    new Customer
                    {
                        CompanyName = "Pacific Foods",
                        ContactName = "Olivia Wilson",
                        Address = "100 Queen Street",
                        City = "Auckland",
                        PostalCode = "1010",
                        Country = "New Zealand",
                        Phone = "+64 9-555 1234"
                    },
                    new Customer
                    {
                        CompanyName = "Savannah Trading",
                        ContactName = "Kwame Nkrumah",
                        Address = "Independence Ave 15",
                        City = "Accra",
                        PostalCode = "GA-100",
                        Country = "Ghana",
                        Phone = "+233 30-555-1234"
                    },
                    new Customer
                    {
                        CompanyName = "Mediterranean Logistics",
                        ContactName = "Carlos González",
                        Address = "Carrer de Mallorca 401",
                        City = "Barcelona",
                        Region = "Catalonia",
                        PostalCode = "08013",
                        Country = "Spain",
                        Phone = "+34 93 555 67 89"
                    },
                    new Customer
                    {
                        CompanyName = "Singapore Tech Hub",
                        ContactName = "Li Wei",
                        Address = "1 Raffles Place",
                        City = "Singapore",
                        PostalCode = "048616",
                        Country = "Singapore",
                        Phone = "+65 6555 1234"
                    },
                    new Customer
                    {
                        CompanyName = "Amazonia Exports",
                        ContactName = "Ana Silva",
                        Address = "Av. Paulista 2001",
                        City = "São Paulo",
                        PostalCode = "01311-300",
                        Country = "Brazil",
                        Phone = "+55 11 5555-9876"
                    },
                    new Customer
                    {
                        CompanyName = "Cape Energy Solutions",
                        ContactName = "Thabo Mbeki",
                        Address = "Long Street 42",
                        City = "Cape Town",
                        PostalCode = "8001",
                        Country = "South Africa",
                        Phone = "+27 21 555 1234"
                    }
                };

                await _context.Customers.InsertManyAsync(initialCustomers);
            }
        }
    }
}
