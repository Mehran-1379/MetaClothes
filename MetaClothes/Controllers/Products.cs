using Microsoft.AspNetCore.Mvc;

namespace MetaClothes.Controllers
{
    [ApiController]
    public class Products: ControllerBase
    {
        public JsonResult GetProducts()
        {
           return new JsonResult(
                new List<object>
                {
                    new {id = 1, name = "Shirt"},
                    new {id = 2, name = "Shoe"}
                }
                );
        }
    }
}
