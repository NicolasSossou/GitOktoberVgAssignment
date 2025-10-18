namespace Infrastructure.Models;

public class ProductCreateRequest
{
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    
}
