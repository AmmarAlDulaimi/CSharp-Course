using CapstoneSaleProject.Models;
using CapstoneSaleProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

class Program
{
    static IConfiguration config = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory()) // Use AppContext.BaseDirectory for compatibility
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .Build();

    // Read settings
    static string connectionString = config.GetConnectionString("SalesDb");
    static string logFilePath = config["Logging:LogFilePath"];

    // Initialize repositories
    static ProductRepository productRepo = new ProductRepository(connectionString);
    static SaleRepository saleRepo = new SaleRepository(connectionString);

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Sales Management Console App ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Add Sale");
            Console.WriteLine("3. View Sales");
            Console.WriteLine("4. Check Inventory");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddProduct(); break;
                case "2": RecordSale(); break;
                case "3": ViewSales(); break;
                case "4": CheckInventory(); break;
                case "5": LogAction("Application exited."); return;
                default: Console.WriteLine("Invalid choice."); LogAction($"Invalid menu choice: {choice}"); break;
            }
        }
    }

    // Logging method
    static void LogAction(string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
    }

    // Add a new product
    static void AddProduct()
    {
        try
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();
            Console.Write("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter initial stock: ");
            int stock = int.Parse(Console.ReadLine());

            productRepo.AddProduct(new Product { Name = name, Price = price, Stock = stock });
            Console.WriteLine($"The Product added \" {name},added successfully.");
            LogAction($"Product added: {name}, Price: {price:C}, Stock: {stock}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding product.");
            LogAction($"Error adding product: {ex.Message}");
        }
    }

    // Record a sale
    static void RecordSale()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        int productId = productRepo.GetProductId(name);
        if (productId == -1)
        {
            Console.WriteLine("Product not found.");
            LogAction($"Attempted sale for unknown product: {name}");
            return;
        }

        try
        {
            Console.Write("Enter quantity sold: ");
            int qty = int.Parse(Console.ReadLine());
            int currentStock = productRepo.GetProductStock(productId);

            if (qty > currentStock)
            {
                Console.WriteLine("Not enough stock.");
                LogAction($"Sale failed for {name}: insufficient stock.");
                return;
            }

            saleRepo.RecordSale(productId, qty);
            productRepo.UpdateProductStock(productId, currentStock - qty);

            Console.WriteLine("Sale recorded successfully.");
            LogAction($"Sale recorded: {qty} x {name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(" Error recording sale.");
            LogAction($"Error recording sale: {ex.Message}");
        }
    }

    // View all sales
    static void ViewSales()
    {
        var sales = saleRepo.GetAllSales();
        decimal grandTotal = 0;

        Console.WriteLine("\n--- Sales Records ---");
        foreach (var sale in sales)
        {
            Console.WriteLine($"{sale.Quantity} x {sale.ProductName} @ {sale.Price:C} = {sale.Total:C}");
            grandTotal += sale.Total;
        }

        Console.WriteLine($"Grand Total: {grandTotal:C}");
        LogAction($"Viewed sales. Grand Total: {grandTotal:C}");
    }

    // Check current inventory
    static void CheckInventory()
    {
        var products = productRepo.GetAllProducts();

        Console.WriteLine("\n--- Inventory ---");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name} | Price: {product.Price:C} | Stock: {product.Stock}");
        }

        LogAction("Checked inventory.");
    }

}