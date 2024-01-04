using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetaClothes.Controllers
{

    [Route("api/products/{productid}/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]

        public ActionResult<IEnumerable<ProductsDto>> 
            GetProduct(int productid)
        {

            var product = 
                ProductDataStore.Current.Products
                .FirstOrDefault(c=> c.ID ==  productid);

            if (product == null)
            {

                return NotFound();
            }

            return Ok(product.products);
        }

    }
}
