using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetaClothes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return Ok(customers);
        }

        // GET api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomerById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/customers
        [HttpPost]
        public ActionResult<CustomerDTO> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            var createdCustomer = _customerService.CreateCustomer(customerDTO);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerDTO customerDTO)
        {
            if (id != customerDTO.CustomerId)
            {
                return BadRequest();
            }

            if (!_customerService.CustomerExists(id))
            {
                return NotFound();
            }

            _customerService.UpdateCustomer(customerDTO);
            return NoContent();
        }

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }

}
