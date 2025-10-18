using Infrastructure.Models;
using Infrastructure.Services;
using Infrastrucutre.Models;

var fileService = new FileService();
var productService = new ProductService(fileService);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Product Manager ===");
    Console.WriteLine("1. Add new product");
    Console.WriteLine("2. Show all products");
    Console.WriteLine("3. Save products to file");
    Console.WriteLine("4. Update a product");
    Console.WriteLine("5. Delete a product");
    Console.WriteLine("6. Exit");
    Console.Write("Choose option (1-6): ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter Product Name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            string? priceInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name) && decimal.TryParse(priceInput, out decimal price))
            {
                try
                {
                    var newProduct = new ProductCreateRequest
                    {
                        ProductTitle = name,
                        ProductPrice = price
                        Category = new ProductCategory { Name = categoryName ?? "Unknown" },
                        Manufacturer = new ProductManufacturer { Name = manufacturerName ?? "Unknown" }
                    };

                    productService.CreateProduct(newProduct);
                    Console.WriteLine("Product added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error: {ex.Message}");
                }
                //ChatGpt Hjälpte med alla {ex,Message}//
            }
            else
            {
                Console.WriteLine(" Invalid. Please try again...");
            }

            Console.ReadKey();
            break;

        case "2":
            Console.WriteLine("--- Product List ---");
            var products = productService.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.ProductTitle}");
                Console.WriteLine($"Price: {product.ProductPrice} SEK");
                Console.WriteLine("------------------------------");
            }
            Console.ReadKey();
            break;

        case "3":
            productService.SaveToFile();
            Console.WriteLine(" Products saved to file successfully!");
            Console.ReadKey();
            break;

        case "4":
            Console.Write("Enter Product ID For UpdateRequest");
            string? updateId = Console.ReadLine();

            Console.Write("Enter new name (leave empty to keep current name)");
            string? newName = Console.ReadLine();

            Console.Write("Enter new price (leave empty to keep current)");
            string? newPriceStr = Console.ReadLine();

            decimal? newPrice = null;
            if (decimal.TryParse(newPriceStr, out decimal parsedPrice))
                newPrice = parsedPrice;

            try
            {
                productService.UpdateProduct(new ProductUpdateRequest
                {
                    Id = updateId!,
                    ProductTitle = string.IsNullOrWhiteSpace(newName) ? null : newName,
                    ProductPrice = newPrice
                });

                Console.WriteLine(" Product updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
            }

            Console.ReadKey();
            break;

        case "5":
            Console.Write("Enter ID of Product For Deletion");
            string? deleteId = Console.ReadLine();

            try
            {
                productService.DeleteProduct(deleteId!);
                Console.WriteLine("Product deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error: {ex.Message}");
            }

            Console.ReadKey();
            break;

        case "6":
            Console.WriteLine("Exiting program...");
            return;

        default:
            Console.WriteLine("Invalid choice, please try again.");
            Console.ReadKey();
            break;
    }
}
