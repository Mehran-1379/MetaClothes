namespace MetaClothes.Models
{
    public class ProductDto
    {

        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public int numberofproducts { get
            {

                return products.Count;

            }
        }


        public ICollection<ProductsDto> products { get; set; }
        = new List<ProductsDto>();
    }
}
