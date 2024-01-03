using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetaClothes.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class Products: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
          return Ok(ProductDataStore.Current.Products);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id) {

            return new JsonResult(ProductDataStore.Current.Products.FirstOrDefault(c=> c.ID == id));
        
        }
    }
}
