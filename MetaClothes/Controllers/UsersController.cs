using Microsoft.AspNetCore.Mvc;
using MetaClothes.Models;
using Npgsql;
using MetaClothes.Tools;
namespace MetaClothes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string connectionString = "Port=1379;Host=localhost" +
            ";Database=MetaClothes;Username=postgres;" +
            "Persist Security Info=True;Password=13ColonelRhodes79";

        //public UsersController(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> userLogin([FromBody] CustomerDTO user)
        {

            String password = Password.hashpassword(user.Password);
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            var sql = "SELECT * FROM customers WHERE email = @email AND password = @password";
            await using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);

            if(cmd == null)
            {
                return BadRequest("This Username or password is incorrect!");
            }


            return Ok(cmd);

        }

    }
}
