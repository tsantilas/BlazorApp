using Microsoft.AspNetCore.Mvc;
using BlazorApp.Models;
using BlazorApp.IService;

namespace BlazorApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (customers, totalCount) = await _customerService.GetAllCustomersAsync(pageNumber, pageSize);
            return Ok(new { Data = customers, TotalCount = totalCount });
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Customer customer)
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
                return NotFound();

            customer.Id = id;
            await _customerService.UpdateCustomerAsync(id, customer);
            return NoContent();
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
