using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MetaClothes.Controllers
{

    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly string connectionString = "Port=1379;Host=localhost" +
           ";Database=MetaClothes;Username=postgres;" +
           "Persist Security Info=True;Password=13ColonelRhodes79";

        //GET All Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetCarts()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM cart";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var cart = new List<string>();

                while (await reader.ReadAsync())
                {
                  
                    var cart_id = reader.GetInt32(0).ToString();
                    var costumer_id = reader.GetInt32(1).ToString();
                    var item_id = reader.GetInt32(2).ToString();
                    var quantity = reader.GetInt32(3).ToString();
                    cart.Add(cart_id);
                    cart.Add(costumer_id);
                    cart.Add(item_id);
                    cart.Add(quantity);


                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Cart By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<string>>> GetCartbyid(int id)
        {

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM cart WHERE cart_id = @cart_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@cart_Id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                var cart = new List<string>();

                while (await reader.ReadAsync())
                {
                   
                    var cart_id = reader.GetInt32(0).ToString();
                    var costumer_id = reader.GetInt32(1).ToString();
                    var item_id = reader.GetInt32(2).ToString();
                    var quantity = reader.GetInt32(3).ToString();
                    cart.Add(cart_id);
                    cart.Add(costumer_id);
                    cart.Add(item_id);
                    cart.Add(quantity);

                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }



        //Update Cart By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] CartDTO cart)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE cart SET customer_id = @customer_id, item_id = @item_id, quantity = @quantity WHERE cart_id = @cart_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@customer_id", cart.CustomerId);
                cmd.Parameters.AddWithValue("@item_id", cart.ItemId);
                cmd.Parameters.AddWithValue("@quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@cart_id", id);

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


        //Add Cart
        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartDTO cart)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO cart (cart_id, customer_id, item_id, quantity) VALUES (@cart_id, @customer_id, @item_id, @quantity)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@cart_id", cart.CartId);
                cmd.Parameters.AddWithValue("@customer_id", cart.CustomerId);
                cmd.Parameters.AddWithValue("@item_id", cart.ItemId);
                cmd.Parameters.AddWithValue("@quantity", cart.Quantity);



                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Cart creation failed.");
                }

                return StatusCode(201, "Cart created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //Delete Cart By id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM cart WHERE cart_id = @cart_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@cart_id", id);

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
