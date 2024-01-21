using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MetaClothes.Controllers
{

    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly string connectionString = "Port=1379;Host=localhost" +
           ";Database=MetaClothes;Username=postgres;" +
           "Persist Security Info=True;Password=13ColonelRhodes79";

        //GET All Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Getproducts()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM products";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var product = new List<string>();

                while (await reader.ReadAsync())
                {

                    var item_id = reader.GetInt32(0).ToString();
                    var item_name = reader.GetString(1);
                    var category_id = reader.GetInt32(2).ToString();
                    var price = reader.GetInt32(3).ToString();
                    var descript = reader.GetString(4);
                    product.Add(item_id);
                    product.Add(item_name);
                    product.Add(category_id);
                    product.Add(price);
                    product.Add(descript);


                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Product By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<string>>> GetProductbyid(int id)
        {

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM products WHERE item_id = @item_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@item_id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                var product = new List<string>();

                while (await reader.ReadAsync())
                {

                    var item_id = reader.GetInt32(0).ToString();
                    var item_name = reader.GetString(1);
                    var category_id = reader.GetInt32(2).ToString();
                    var price = reader.GetInt32(3).ToString();
                    var descript = reader.GetString(4);
                    product.Add(item_id);
                    product.Add(item_name);
                    product.Add(category_id);
                    product.Add(price);
                    product.Add(descript);

                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }



        //Update Product By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductsDTO product)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE products SET item_name = @item_name, category_id = @category_id, price = @price, description = @description WHERE item_id = @item_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@item_name", product.ItemName);
                cmd.Parameters.AddWithValue("@category_id", product.CategoryId);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@item_id", id);

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


        //Add Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductsDTO product)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO payment (item_id, item_name, category_id, price, description) VALUES (@item_id, @item_name, @category_id, @price, @description)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@item_name", product.ItemName);
                cmd.Parameters.AddWithValue("@category_id", product.CategoryId);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@item_id", product.ItemId);



                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Procuct creation failed.");
                }

                return StatusCode(201, "Product created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //Delete Product By id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM product WHERE item_id = @item_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@item_id", id);

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
