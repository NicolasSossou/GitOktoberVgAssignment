namespace Infrastructure.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductTitle { get; set; } = "";
        public decimal ProductPrice { get; set; }
        public string ArticleNumber { get; set; } = "";
        public ProductCategory? Category { get; set; }
        public ProductManufacturer? Manufacturer { get; set; }
    }
}
