 using Infrastructure.Models;
using Infrastrucutre.Models;
using System.Text.Json;
namespace Infrastructure.Services;

public class ProductService
{
    private List<Product> _productList = new();
    private readonly FileService _fileService;

    public ProductService(FileService fileService)
    {
        _fileService = fileService;

        var json = _fileService.GetJsonContentFromFile();
        if (!string.IsNullOrWhiteSpace(json))
        {
            var loaded = JsonSerializer.Deserialize<List<Product>>(json);
            if (loaded is not null)
                _productList = loaded;
        }
    }

    public void CreateProduct(ProductCreateRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            ProductTitle = request.ProductTitle,
            ProductPrice = request.ProductPrice
        };

        _productList.Add(product);
        SaveToFile();
    }

    public IEnumerable<Product> GetAllProducts() => _productList;

    public void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_productList, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        _fileService.SaveJsonContentToFile(json);
    }
    public void DeleteProduct(ProductUpdateRequest productUpdateRequest)
    {
        // Find product by matching Id
        var product = _productList.FirstOrDefault(p => p.Id == productUpdateRequest.Id);

        if (product is null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        _productList.Remove(product);
        SaveToFile();  // Save changes after deletion

        Console.WriteLine("Product deleted.");
    }
}