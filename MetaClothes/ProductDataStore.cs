using MetaClothes.Models;

namespace MetaClothes
{
    public class ProductDataStore
    {
        public List<ProductDto> Products { get; set; }

        public static ProductDataStore Current { get;} = new ProductDataStore();
        public ProductDataStore()
        {
            Products = new List<ProductDto>()
            {

                new ProductDto(){ ID = 1 , Name = "Shoe" , Cost = 450000 , Description = "Beautifull" },

                new ProductDto(){ ID = 2 , Name = "Shirt" , Cost = 300000 , Description = "Beautifull" },
                
                new ProductDto(){ ID = 3 , Name = "Glove" , Cost = 150000 , Description = "Beautifull" },

                new ProductDto(){ ID = 4 , Name = "T-Shrit" , Cost = 350000 , Description = "Beautifull" }

            };
        }

    }
}
