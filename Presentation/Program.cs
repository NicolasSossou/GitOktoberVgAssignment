using Infrastructure.Models;
using Infrastructure.Services;

var productService = new ProductService();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Product Manager ===");
    Console.WriteLine("1. Add New Product");
    Console.WriteLine("2. Show All Products");
    Console.WriteLine("3. Update Product");
    Console.WriteLine("4. Delete Product");
    Console.WriteLine("5. Save Products");
    Console.WriteLine("6. Exit");
    Console.Write("Choose option (1-6): ");
    string? choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Clear();
        Console.WriteLine("=== Add Product ===");

        Console.Write("Enter Product Name: ");
        string? title = Console.ReadLine();

        Console.Write("Enter Product Price: ");
        string? price = Console.ReadLine();
        decimal.TryParse(price, out decimal newPrice);

        Console.Write("Enter Category Name: ");
        string? categoryName = Console.ReadLine();

        Console.Write("Enter Manufacturer Name: ");
        string? manufacturerName = Console.ReadLine();

        var category = new ProductCategory { Name = string.IsNullOrWhiteSpace(categoryName) ? "Unknown" : categoryName };
        var manufacturer = new ProductManufacturer { Name = string.IsNullOrWhiteSpace(manufacturerName) ? "Unknown" : manufacturerName };

        var newProduct = new ProductCreateRequest
        {
            ProductTitle = title ?? "",
            ProductPrice = newPrice,
            Category = category,
            Manufacturer = manufacturer
        };

        productService.CreateProduct(newProduct);
        Console.WriteLine("Press Any Key To Continue");
        Console.ReadKey();
    }

    else if (choice == "2")
    {
        Console.Clear();
        var products = productService.GetAllProducts();

        if (products.Count == 0)
            Console.WriteLine("No Product Found.");
        else
        {
            foreach (var p in products)
            {
                Console.WriteLine($"ID: {p.Id}");
                Console.WriteLine($"Name: {p.ProductTitle}");
                Console.WriteLine($"Price: {p.ProductPrice} SEK");
                Console.WriteLine($"Category: {p.Category?.Name}");
                Console.WriteLine($"Manufacturer: {p.Manufacturer?.Name}");
                Console.WriteLine("--------------------------------");
            }
        }
        Console.ReadKey();
    }

    else if (choice == "3")
    {
        Console.Clear();
        Console.Write("Enter Product ID To Update: ");
        string? updateId = Console.ReadLine();

        Console.Write("Enter New Product Name");
        string? newName = Console.ReadLine();

        Console.Write("Enter New Product Price");
        string? newPriceInput = Console.ReadLine();
        decimal.TryParse(newPriceInput, out decimal newPrice);

        var updateRequest = new ProductUpdateRequest
        {
            Id = updateId ?? "",
            ProductTitle = string.IsNullOrWhiteSpace(newName) ? null : newName,
            ProductPrice = newPriceInput == "" ? null : newPrice
        };

        productService.UpdateProduct(updateRequest);
        Console.ReadKey();
    }

    else if (choice == "4")
    {
        Console.Clear();
        Console.Write("Enter ID Of Product To Delete");
        string? deleteId = Console.ReadLine();

        productService.DeleteProduct(deleteId ?? "");
        Console.ReadKey();
    }

    else if (choice == "5")
    {
        productService.SaveToFile();
        Console.ReadKey();
    }

    else if (choice == "6")
    {
        Console.WriteLine("Exiting Program...");
        break;
    }

    else
    {
        Console.WriteLine("Invalid. Please Try Again...");
        Console.ReadKey();
    }
}
