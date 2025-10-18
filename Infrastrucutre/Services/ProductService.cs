using Infrastructure.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Services
{
    public class ProductService :   IProductService
    {
        private IFileService _fileService = new FileService();
        private List<Product> _products = new List<Product>();

        private const string FilePath = @"C:\products.json";

        public ProductService(FileService fileService)
        {
            _products = _fileService.Load<Product>(FilePath).ToList();
            _fileService = fileService;
        }

        public void CreateProduct(ProductCreateRequest productCreateRequest)
        {
            if (string.IsNullOrWhiteSpace(productCreateRequest.ProductTitle))
                throw new ArgumentException("Product Name Cannot Be Empty Or False.");

            foreach (Product item in _products)
            {
                if (item.ProductTitle.ToLower() == productCreateRequest.ProductTitle.ToLower())
                {
                    throw new InvalidOperationException("Product Name Already Exists.");
                }
            }


            Product newProduct = new Product();
            newProduct.Id = Guid.NewGuid().ToString();
            newProduct.ProductTitle = productCreateRequest.ProductTitle;
            newProduct.ProductPrice = productCreateRequest.ProductPrice;
            newProduct.Manufacturer = productCreateRequest.Manufacturer;
            newProduct.Category = productCreateRequest.Category;

            _products.Add(newProduct);
            SaveToFile();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(string id)
        {
            foreach (Product product in _products)
            {
                if (product.Id == id)
                    return product;
            }
            return null!;
        }

        
        public void UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            Product? productToUpdate = null;

            foreach (Product product in _products)
            {
                if (product.Id == productUpdateRequest.Id)
                {
                    productToUpdate = product;
                    break;
                }
            }

            if (productToUpdate == null)
                throw new InvalidOperationException("Product not found.");

            if (!string.IsNullOrWhiteSpace(productUpdateRequest.ProductTitle))
                productToUpdate.ProductTitle = productUpdateRequest.ProductTitle;

            if (productUpdateRequest.ProductPrice.HasValue)
                productToUpdate.ProductPrice = productUpdateRequest.ProductPrice.Value;

            SaveToFile();
        }
        public void DeleteProduct(string id)
        {
            Product? productToDelete = null;

            foreach (Product product in _products)
            {
                if (product.Id == id)
                {
                    productToDelete = product;
                    break;
                }
            }

            if (productToDelete == null)
                throw new InvalidOperationException("Product not found.");

            _products.Remove(productToDelete);
            SaveToFile();
        }

        public void SaveToFile()
        {
            _fileService.Save(FilePath, _products);
        }
    }
}
