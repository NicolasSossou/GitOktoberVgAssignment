namespace Infrastructure.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public string ProductCategory { get; set; } = null!;
    public string ProductManufacturer { get; set; } = null!;

}