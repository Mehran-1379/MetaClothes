using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MetaClothes.Controllers
{

    [Route("api/Payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly string connectionString = "Port=1379;Host=localhost" +
           ";Database=MetaClothes;Username=postgres;" +
           "Persist Security Info=True;Password=13ColonelRhodes79";

        //GET All Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Getpayment()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM payment";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var payment = new List<string>();

                while (await reader.ReadAsync())
                {

                    var payment_id = reader.GetInt32(0).ToString();
                    var order_id = reader.GetInt32(1).ToString();
                    var payment_date = reader.GetDateTime(2).ToString();
                    var amount = reader.GetInt32(3).ToString();
                    var method = reader.GetString(4);
                    payment.Add(payment_id);
                    payment.Add(order_id);
                    payment.Add(payment_date);
                    payment.Add(amount);
                    payment.Add(method);


                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Payment By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<string>>> GetPaymentbyid(int id)
        {

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM payment WHERE payment_id = @payment_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@payment_id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                var payment = new List<string>();

                while (await reader.ReadAsync())
                {

                    var payment_id = reader.GetInt32(0).ToString();
                    var order_id = reader.GetInt32(1).ToString();
                    var payment_date = reader.GetDateTime(2).ToString();
                    var amount = reader.GetInt32(3).ToString();
                    var method = reader.GetString(4);
                    payment.Add(payment_id);
                    payment.Add(order_id);
                    payment.Add(payment_date);
                    payment.Add(amount);
                    payment.Add(method);

                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }



        //Update Payment By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentDTO payment)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE payment SET order_id = @order_id, payment_date = @payment_date, amount = @amount, payment_method = @payment_method WHERE payment_id = @payment_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@order_id", payment.OrderId);
                cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@amount", payment.Amount);
                cmd.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@payment_id", id);

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


        //Add Payment
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDTO payment)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO payment (payment_id, order_id, payment_date, amount, payment_method) VALUES (@payment_id, @order_id, @payment_date, @amount, @payment_method)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@payment_id", payment.PaymentId);
                cmd.Parameters.AddWithValue("@order_id", payment.OrderId);
                cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@amount", payment.Amount);
                cmd.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);



                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Payment creation failed.");
                }

                return StatusCode(201, "Payment created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //Delete Payment By id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM payment WHERE payment_id = @payment_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@payment_id", id);

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
