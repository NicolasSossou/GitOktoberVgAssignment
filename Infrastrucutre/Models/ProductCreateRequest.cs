namespace Infrastructure.Models;

public class ProductCreateRequest
{
    public string CategoryId { get; set; } = null!;

    public string ManufacturerId { get; set; } = null!;
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public ProductCategory? Category { get; set; }
    public ProductManufacturer? Manufacturer { get; set; }
}
