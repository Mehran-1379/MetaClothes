using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MetaClothes.Controllers
{

    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly string connectionString = "Port=1379;Host=localhost" +
           ";Database=MetaClothes;Username=postgres;" +
           "Persist Security Info=True;Password=13ColonelRhodes79";

        //GET All Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetOrders()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM orders";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var order = new List<string>();

                while (await reader.ReadAsync())
                {

                    var order_id = reader.GetInt32(0).ToString();
                    var costumer_id = reader.GetInt32(1).ToString();
                    var order_date = reader.GetDateTime(2).ToString();
                    var total_amount = reader.GetInt32(3).ToString();
                    var status = reader.GetString(4);
                    order.Add(order_id);
                    order.Add(costumer_id);
                    order.Add(order_date);
                    order.Add(total_amount);
                    order.Add(status);


                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Order By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<string>>> GetOrderbyid(int id)
        {

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM orders WHERE order_id = @order_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@order_Id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                var order = new List<string>();

                while (await reader.ReadAsync())
                {

                    var order_id = reader.GetInt32(0).ToString();
                    var costumer_id = reader.GetInt32(1).ToString();
                    var order_date = reader.GetDateTime(2).ToString();
                    var total_amount = reader.GetInt32(3).ToString();
                    var status = reader.GetString(4);
                    order.Add(order_id);
                    order.Add(costumer_id);
                    order.Add(order_date);
                    order.Add(total_amount);
                    order.Add(status);

                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }



        //Update Order By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDTO order)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE orders SET customer_id = @customer_id, order_date = @order_date, total_amount = @total_amount, status = @status WHERE order_id = @order_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customer_id", order.CustomerId);
                cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                cmd.Parameters.AddWithValue("@total_amount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@order_id", id);

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


        //Add Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO order)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO orders (order_id, customer_id, order_date, total_amount, status) VALUES (@order_id, @customer_id, @order_date, @total_amount, @status)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@order_id", order.OrderId);
                cmd.Parameters.AddWithValue("@customer_id", order.CustomerId);
                cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
                cmd.Parameters.AddWithValue("@total_amount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@status", order.Status);



                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Order creation failed.");
                }

                return StatusCode(201, "Order created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //Delete Order By id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM orders WHERE order_id = @order_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@order_id", id);

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
