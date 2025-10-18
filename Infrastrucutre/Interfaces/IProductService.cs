using Infrastructure.Models;

namespace Infrastrucutre.Interfaces;

public interface IProductService
{
    void CreateProduct(ProductCreateRequest productCreateRequest);
    IEnumerable<Product> GetAllProducts();
    void SaveToFile();
}
