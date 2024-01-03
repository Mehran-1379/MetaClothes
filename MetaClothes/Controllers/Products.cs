using Microsoft.AspNetCore.Mvc;

namespace MetaClothes.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class Products: ControllerBase
    {
        [HttpGet]
        public ActionResult GetProducts()
        {
           return new JsonResult(ProductDataStore.Current.Products);
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id) {

            return new JsonResult(ProductDataStore.Current.Products.FirstOrDefault(c=> c.ID == id));
        
        }
    }
}
