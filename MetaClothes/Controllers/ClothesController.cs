using Microsoft.AspNetCore.Mvc;
using Npgsql;
using MetaClothes.Models;

namespace MetaClothes.Controllers
{

    [ApiController]
    [Route("api/Clothes")]
    public class Clothes : ControllerBase
    {

        private readonly string connectionString = "Port=1379;Host=localhost" +
            ";Database=MetaClothes;Username=postgres;" +
            "Persist Security Info=True;Password=13ColonelRhodes79";
        //GET+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetClothes()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM Clothes";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var Clothes = new List<string>();

                while (await reader.ReadAsync())
                {
                    // Adjust index based on your column order or use column names directly
                    var ClothesName = reader.GetString(1);
                    Clothes.Add(ClothesName);
                }

                return Ok(Clothes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }


        //PUT+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClothes(int id, [FromBody] ClothesDto clothes)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE clothes SET name = @name, price = @price, category = @category , image = @image WHERE id = @id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", clothes.name);
                cmd.Parameters.AddWithValue("@price", clothes.price);
                cmd.Parameters.AddWithValue("@category", clothes.category);
                cmd.Parameters.AddWithValue("@image", clothes.image);
                cmd.Parameters.AddWithValue("@id", clothes.id);

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


        //POST+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [HttpPost]
        public async Task<IActionResult> CreateClothes([FromBody] ClothesDto clothes)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO Clothes (id,name,price,category,image) VALUES (@id,@name,@price,@category,@image)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", clothes.id);
                cmd.Parameters.AddWithValue("@name", clothes.name);
                cmd.Parameters.AddWithValue("@price", clothes.price);
                cmd.Parameters.AddWithValue("@category", clothes.category);
                cmd.Parameters.AddWithValue("@image", clothes.image);

                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Clothe creation failed.");
                }

                return StatusCode(201, "Clothe created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //DELETE+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClothes(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM Clothes WHERE id = @id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);

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
