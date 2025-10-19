using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IProductService
    {
        bool CreateProduct(ProductCreateRequest productCreateRequest);
        List<Product> GetAllProducts();
        Product? GetProductById(string id);
        bool UpdateProduct(ProductUpdateRequest productUpdateRequest);
        bool DeleteProduct(string id);
        void SaveToFile();
        void LoadFromFile();
    }
}
