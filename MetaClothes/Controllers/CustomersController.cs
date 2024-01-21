using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MetaClothes.Controllers
{

    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly string connectionString = "Port=1379;Host=localhost" +
            ";Database=MetaClothes;Username=postgres;" +
            "Persist Security Info=True;Password=13ColonelRhodes79";

        //GET All Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetCustomers()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM customers";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var customers = new List<string>();

                while (await reader.ReadAsync())
                {
                   
                    var customerid = reader.GetInt32(0).ToString();
                    var customername = reader.GetString(1);
                    var customerlast = reader.GetString(2);
                    var customeremail = reader.GetString(3);
                    var customeraddress = reader.GetString(5);
                    customers.Add(customerid);
                    customers.Add(customername);
                    customers.Add(customerlast);
                    customers.Add(customeremail);
                    customers.Add(customeraddress);

                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Customer By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<string>>> GetCustomerbyid(int id)
        {

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM customers WHERE customer_id = @customer_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customer_Id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                var customers = new List<string>();

                while (await reader.ReadAsync())
                {
                    var customerid = reader.GetInt32(0).ToString();
                    var customername = reader.GetString(1);
                    var customerlast = reader.GetString(2);
                    var customeremail = reader.GetString(3);
                    var customeraddress = reader.GetString(5);
                    customers.Add(customerid);
                    customers.Add(customername);
                    customers.Add(customerlast);
                    customers.Add(customeremail);
                    customers.Add(customeraddress);

                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }



        //Update Customer By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO customer)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE customers SET first_name = @first_name, last_name = @last_name, email = @email, password = @password, address = @address WHERE customer_id = @customer_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@first_name", customer.FirstName);
                cmd.Parameters.AddWithValue("@last_name", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@password", customer.Password);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@customer_id", id);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        //Add Customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO customers (customer_id, first_name, last_name, email, password, address) VALUES (@customer_id, @first_name, @last_name, @email, @password, @address)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customer_id", customer.CustomerId);
                cmd.Parameters.AddWithValue("@first_name", customer.FirstName);
                cmd.Parameters.AddWithValue("@last_name", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@password", customer.Password);
                cmd.Parameters.AddWithValue("@address", customer.Address);



                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Customer creation failed.");
                }

                return StatusCode(201, "Customer created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //Delete Customer By id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM customers WHERE customer_id = @customer_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customer_id", id);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




    }

}
