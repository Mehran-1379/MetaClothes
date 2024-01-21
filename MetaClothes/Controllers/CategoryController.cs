using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MetaClothes.Controllers
{

    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly string connectionString = "Port=1379;Host=localhost" +
            ";Database=MetaClothes;Username=postgres;" +
            "Persist Security Info=True;Password=13ColonelRhodes79";

        //GET All Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM Categories";
                await using var cmd = new NpgsqlCommand(sql, connection);

                await using var reader = await cmd.ExecuteReaderAsync();

                var Categories = new List<string>();

                while (await reader.ReadAsync())
                {
                    var categoryid = reader.GetInt32(0).ToString();
                    var categoryname = reader.GetString(1);
                    Categories.Add(categoryid);
                    Categories.Add(categoryname);
                   

                }

                return Ok(Categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }

        }

        //Get Category By Id

        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<string>>> GetCategorybyid(int id)
        {

            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "SELECT * FROM categories WHERE category_id = @category_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@category_Id", id);
                await using var reader = await cmd.ExecuteReaderAsync();

                var Categories = new List<string>();

                while (await reader.ReadAsync())
                {
                    var categoryid = reader.GetInt32(0).ToString();
                    var categoryname = reader.GetString(1);
                    Categories.Add(categoryid);
                    Categories.Add(categoryname);

                }

                return Ok(Categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }


        }



        //Update Category By Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO category)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "UPDATE categories SET category_name = @category_name WHERE category_id = @category_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@category_name", category.CategoryName);
                cmd.Parameters.AddWithValue("@category_id", id);

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


        //Add Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "INSERT INTO categories (category_id, category_name) VALUES (@category_id, @category_name)";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@category_id", category.CategoryId);
                cmd.Parameters.AddWithValue("@category_name", category.CategoryName);



                var rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return StatusCode(500, "Error: Category creation failed.");
                }

                return StatusCode(201, "Category created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




        //Delete Category By id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();

                var sql = "DELETE FROM categories WHERE category_id = @category_id";
                await using var cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@category_id", id);

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
