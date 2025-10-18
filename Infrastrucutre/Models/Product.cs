namespace Infrastructure.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductTitle { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
}