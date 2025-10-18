namespace Infrastructure.Models;

public class ProductCreateRequest
{
    public string ProductTitle { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
}
