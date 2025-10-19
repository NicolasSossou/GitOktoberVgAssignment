using Infrastructure.Interfaces;
using Infrastructure.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private List<Product> productList = [];
        private string filePath = @"C:\products.json";

        public ProductService()
        {
            LoadFromFile();
        }

        public void SaveToFile()
        {
            if (productList.Count == 0)
            {
                Console.WriteLine("No products to save.");
                return;
            }

            string json = JsonSerializer.Serialize(productList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            Console.WriteLine("Products saved to file.");
        }

        public void LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                productList = [];
                return;
            }

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                productList = [];
                return;
            }

            List<Product>? loaded = JsonSerializer.Deserialize<List<Product>>(json);
            productList = loaded ?? [];
        }

        public bool CreateProduct(ProductCreateRequest productCreateRequest)
        {
            if (string.IsNullOrWhiteSpace(productCreateRequest.ProductTitle))
            {
                Console.WriteLine("Product Needs To Have A Real Name");
                return false;
            }

            foreach (var existingProduct in productList)
            {
                if (existingProduct.ProductTitle.Equals(productCreateRequest.ProductTitle, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("This Product Already Exists");
                    return false;
                }
            }

            var newProduct = new Product
            {
                Id = Guid.NewGuid().ToString(),
                ProductTitle = productCreateRequest.ProductTitle,
                ProductPrice = productCreateRequest.ProductPrice,
                Category = productCreateRequest.Category,
                Manufacturer = productCreateRequest.Manufacturer
            };

            productList.Add(newProduct);
            SaveToFile();
            Console.WriteLine("Product Added Successfully!");
            return true;
        }

        public List<Product> GetAllProducts()
        {
            return productList;
        }

        public Product? GetProductById(string id)
        {
            foreach (var product in productList)
            {
                if (product.Id == id)
                    return product;
            }
            return null;
        }

        public bool UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            foreach (var product in productList)
            {
                if (product.Id == productUpdateRequest.Id)
                {
                    if (!string.IsNullOrWhiteSpace(productUpdateRequest.ProductTitle))
                        product.ProductTitle = productUpdateRequest.ProductTitle;

                    if (productUpdateRequest.ProductPrice.HasValue)
                        product.ProductPrice = productUpdateRequest.ProductPrice.Value;

                    SaveToFile();
                    Console.WriteLine("Product Updated Successfully!");
                    return true;
                }
            }

            Console.WriteLine("Product Does Not Exist");
            return false;
        }

       
        public bool DeleteProduct(string id)
        {
            foreach (var product in productList)
            {
                if (product.Id == id)
                {
                    productList.Remove(product);
                    SaveToFile();
                    Console.WriteLine("Product Deleted Successfully!");
                    return true;
                }
            }

            Console.WriteLine("Product Does Not Exist");
            return false;
        }
    }
}
