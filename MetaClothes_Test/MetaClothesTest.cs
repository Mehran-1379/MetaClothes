using MetaClothes.Controllers;
using MetaClothes.Models;
using Microsoft.AspNetCore.Mvc;


namespace MetaClothes_Test
{
    public class MetaClothesTest
    {
        ClothesDto Example = new ClothesDto() { ID = 27, Name = "Test", Price = 50000 };

        Clothes _controller;
        [Fact]
        public async Task GetClothes_ReturnsStatusCode200()
        {

            var Controlling = new Clothes();

           
            var result = await Controlling.GetClothes();

            
            var statusCodeResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task UpdateClothes_ReturnsStatusCode204()
        {
           
            var Controlling = new Clothes();

           
            var result = await Controlling.UpdateClothes(10, Example);

          
            var statusCodeResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CreateClothes_ReturnsStatusCode201()
        {
          
            var Controlling = new Clothes();

         
            var result = await Controlling.CreateClothes(new ClothesDto() { ID = 17, Name = "Test2", Price = 400000 });

         
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeleteClothes_ReturnsStatusCode204()
        {
            
            var Controlling = new Clothes();

            var result = await Controlling.DeleteClothes(6);

            
            var statusCodeResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, statusCodeResult.StatusCode);
        }


    }
}
