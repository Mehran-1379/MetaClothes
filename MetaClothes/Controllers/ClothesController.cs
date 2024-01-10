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

                var sql = "UPDATE clothes SET Name = @Name, Price = @Price WHERE ID = @ID";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Name", clothes.Name);
                cmd.Parameters.AddWithValue("@Price", clothes.Price);
                cmd.Parameters.AddWithValue("@ID", clothes.ID);

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

                var sql = "INSERT INTO Clothes (Name, Price) VALUES (@Name, @Price)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Name", clothes.Name);
                cmd.Parameters.AddWithValue("@Price", clothes.Price);

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

                var sql = "DELETE FROM Clothes WHERE ID = @ID";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", id);

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
