namespace Infrastructure.Models;

public class ProductCreateRequest
{
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public ProductCategory? Category { get; set; }
    public ProductManufacturer? Manufacturer { get; set; }
}
