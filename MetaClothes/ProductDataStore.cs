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

                new ProductDto(){ ID = 1, Name = "Shoe"
                , products = new List<ProductsDto>(){ 
                
                    new ProductsDto()
                    {

                        id = 1 , name = "Nike" , cost = 750000 , description = ""

                    }
                
                
                } },

                new ProductDto(){ ID = 2 , Name = "Shirt"
                 , products = new List<ProductsDto>(){

                    new ProductsDto()
                    {

                        id = 2 , name = "LV Shirt" , cost = 450000 , description = "New Brand"

                    }


                }},
                
                new ProductDto(){ ID = 3 , Name = "Pants"
                 , products = new List<ProductsDto>(){

                    new ProductsDto()
                    {

                        id = 3 , name = "Nike" , cost = 500000 , description = ""

                    }


                }},

                new ProductDto(){ ID = 4 , Name = "Jacket"
                 , products = new List<ProductsDto>(){

                    new ProductsDto()
                    {

                        id = 4 , name = "Meta Jacket" , cost = 400000 , description = "Our Brand Is Coming!"

                    }

                    
                }}

            };
        }

    }
}
