using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Text.Json;

namespace Infrastructure.Services;

public class ProductService : IProductService
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
    public void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_productList, new JsonSerializerOptions { WriteIndented = true });
        _fileService.SaveJsonContentToFile(json);

    }
    public bool CreateProduct(ProductCreateRequest productRequest)
    {
        if (string.IsNullOrWhiteSpace(productRequest.ProductTitle))
            return false;

        foreach (var product in _productList)
        {
            if (product.ProductTitle == productRequest.ProductTitle)
                return false;
        }
        var newProduct = new Product
        {
            Id = Guid.NewGuid().ToString(),
            ProductTitle = productRequest.ProductTitle,
            ProductPrice = productRequest.ProductPrice,
            Category = new ProductCategory { Name = productRequest.CategoryId },
            Manufacturer = new ProductManufacturer { Name = productRequest.ManufacturerId }
        };

        _productList.Add(newProduct);
        SaveToFile();
        return true;
    }


    public IEnumerable<Product> GetAllProducts()
    {
        return _productList;
    }

    public Product GetProductById(Guid id)
    {
        string productId = id.ToString();
        foreach (var product in _productList)
        {
            if (product.Id == productId)
                return product;
        }
        return null!;
    }

    public Product GetProductByArticleNumber(string articleNumber)
    {
        foreach (var product in _productList)
        {
            if (product.ArticleNumber == articleNumber)
                return product;
        }
        return null!;
    }

    public Product GetProductByProductName(string productTitle)
    {
        foreach (var product in _productList)
        {
            if (product.ProductTitle == productTitle)
                return product;
        }
        return null!;
    }

    public Product GetProductByProductPrice(decimal productPrice)
    {
        foreach (var product in _productList)
        {
            if (product.ProductPrice == productPrice)
                return product;
        }
        return null!;
    }

    public bool SaveJsonContentToFile()
    {
        var json = JsonSerializer.Serialize(_productList, new JsonSerializerOptions { WriteIndented = true });
        return _fileService.SaveJsonContentToFile(json);
    }

    public bool AddProductsToList(ProductCreateRequest productRequest)
    {
        if (string.IsNullOrWhiteSpace(productRequest.ProductTitle))
            return false;

        var newProduct = new Product
        {
            Id = Guid.NewGuid().ToString(),
            ProductTitle = productRequest.ProductTitle,
            ProductPrice = productRequest.ProductPrice
        };

        _productList.Add(newProduct);
        SaveToFile();
        return true;
    }

    public bool UpdateProduct(Guid id, ProductUpdateRequest productUpdate)
    {
        string idString = id.ToString();
        foreach (var product in _productList)
        {
            if (product.Id == idString)
            {
                if (string.IsNullOrWhiteSpace(productUpdate.ProductTitle))
                    return false;

                product.ProductTitle= productUpdate.ProductTitle;
                product.ProductPrice = productUpdate.ProductPrice;

                SaveToFile();
                return true;
            }
        }
        return false;
    }

    public bool DeleteProduct(Guid productId)
    {
        string idString = productId.ToString();

        foreach (var product in _productList)
        {
            if (product.Id == idString)
            {
                _productList.Remove(product);

                SaveToFile();

                return true;
            }
        }

        return false;
    }
    

   