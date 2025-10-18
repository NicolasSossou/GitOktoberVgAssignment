namespace Infrastructure.Models;

public class ProductManufacturer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductManufacturerName { get; set; } = null!;
}