using Infrastructure.Models;


namespace Infrastructure.Interfaces;

public interface IProductService
{
        void CreateProduct(ProductCreateRequest productCreateRequest);
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(string id);
        void UpdateProduct(ProductUpdateRequest productUpdateRequest);
        void DeleteProduct(string id);
        void SaveToFile();
}
