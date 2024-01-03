namespace MetaClothes.Models
{
    public class ProductDto
    {

        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Cost { get; set; }
        public string? Description { get; set; }
    }
}
