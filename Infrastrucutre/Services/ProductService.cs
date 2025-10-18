 using Infrastructure.Models;
using Infrastrucutre.Interfaces;
using Infrastrucutre.Models;
using System.Text.Json;
namespace Infrastructure.Services;

using Infrastrucutre.Interfaces;
using Infrastructure.Models;
using System.Text.Json;

public class ProductService : IProductService
{
    private readonly IFileService _fileService;
    private List<Product> _products;
    private const string FilePath = "products.json";

    public ProductService(IFileService fileService)
    {
        _fileService = fileService;
        _products = _fileService.Load<Product>(FilePath).ToList();
    }
    public void CreateProduct(ProductCreateRequest productUpdateRequest)
    {
      
        if (string.IsNullOrWhiteSpace(productUpdateRequest.ProductTitle))
            throw new ArgumentException("Produktnamn får inte vara tomt.");

      
        if (_products.Any(p =>
            p.ProductTitle.Equals(productUpdateRequest.ProductTitle, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Produkten finns redan.");

      
        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            ProductTitle = productUpdateRequest.ProductTitle,
            ProductPrice = productUpdateRequest.ProductPrice
        };

        _products.Add(product);
        SaveToFile();
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _products;
    }
    public Product? GetProductById(string id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
    public void UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        var product = _products.FirstOrDefault(p => p.Id == productUpdateRequest.Id);
        if (product == null)
            throw new InvalidOperationException("Produkten hittades inte.");

        if (!string.IsNullOrWhiteSpace(productUpdateRequest.ProductTitle))
            product.ProductTitle = productUpdateRequest.ProductTitle;

        if (productUpdateRequest.ProductPrice.HasValue)
            product.ProductPrice = productUpdateRequest.ProductPrice.Value;

        if (productUpdateRequest.Category != null)
            product.Category = productUpdateRequest.Category;

        if (productUpdateRequest.Manufacturer != null)
            product.Manufacturer = productUpdateRequest.Manufacturer;

        SaveToFile();
    }
    public void DeleteProduct(string id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            throw new InvalidOperationException("Produkten hittades inte.");

        _products.Remove(product);
        SaveToFile();
    }
    public void SaveToFile()
    {
        _fileService.Save(FilePath, _products);
    }
}

