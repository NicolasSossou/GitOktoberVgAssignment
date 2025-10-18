using System;
using Infrastructure.Models;
using Infrastructure.Services;


var fileService = new FileService();
var productService = new ProductService(fileService);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Product Manager ===");
    Console.WriteLine("1. Add New Product");
    Console.WriteLine("2. Show All Products");
    Console.WriteLine("3. Save Products To File");
    Console.WriteLine("4. Update A Product");
    Console.WriteLine("5. Delete A Product");
    Console.WriteLine("6. Exit");
    Console.Write("Choose Option (1-6): ");

    var choice = Console.ReadLine();

    switch (choice)
    {
   case "1":
        Console.Clear();
        Console.WriteLine("=== Add New Product ===");

        Console.Write("Enter Product Name: ");
        string? productName = Console.ReadLine();

        Console.Write("Enter Product Price: ");
        string? productPriceInput = Console.ReadLine();

        Console.Write("Enter Product Category: ");
        string? productCategoryInput = Console.ReadLine();

        Console.Write("Enter Manufacturer Name: ");
        string? productManufacturerInput = Console.ReadLine();


        if (!string.IsNullOrWhiteSpace(productName) && decimal.TryParse(productPriceInput, out decimal productPrice))
        {
            
            var newProduct = new ProductCreateRequest();

           
            newProduct.ProductTitle = productName;
            newProduct.ProductPrice = productPrice;

           
            var category = new ProductCategory();
            if (string.IsNullOrWhiteSpace(productCategoryInput))
            {
                category.Name = "";
            }
            else
            {
                category.Name = productCategoryInput.Trim();
            }

            var manufacturer = new ProductManufacturer();
            if (string.IsNullOrWhiteSpace(productManufacturerInput))
            {
                manufacturer.Name = "";
            }
            else
            {
                manufacturer.Name = productManufacturerInput.Trim();
            }

            
            newProduct.Category = category;
            newProduct.Manufacturer = manufacturer;

            try
            {
                productService.CreateProduct(newProduct);
                Console.WriteLine("Product has been added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid Product Name Or Price. Please Try Again.");
        }

        Console.WriteLine("Press Any Key To Continue...");
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
            Console.WriteLine("Products Saved To File successfully!");
            Console.ReadKey();
            break;

        case "4":
            Console.Write("Enter Product ID For UpdateRequest");
            string? updateId = Console.ReadLine();

            Console.Write("Enter new name (Leave Empty To Keep Current Name)");
            string? newName = Console.ReadLine();

            Console.Write("Enter new price (Leave Empty To Keep Current Price)");
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

                Console.WriteLine("Product updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
            break;

        case "5":
            Console.Write("Enter ID For Product To Be Deleted");
            string? deleteId = Console.ReadLine();

            try
            {
                productService.DeleteProduct(deleteId!);
                Console.WriteLine("Product Deleted Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
            break;

        case "6":
            Console.WriteLine("Exiting Program...");
            return;

        default:
            Console.WriteLine("Invalid Choice, Please Try Again.");
            Console.ReadKey();
            break;
    }
}
