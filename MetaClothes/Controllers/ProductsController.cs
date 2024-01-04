using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

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

        [HttpGet("{productsid}")]
        public ActionResult<ProductsDto> GetProduc(
            
            int productid , int productsid
            
            )
        {
            var product =
               ProductDataStore.Current.Products
               .FirstOrDefault(c => c.ID == productid);

            if(product == null)
            {
                return NotFound();
            }

            var produc = product.products.FirstOrDefault(p => p.id == productsid);

            if(produc == null)
            {
                return NotFound();
            }

            return Ok(produc);

        }

    }
}
