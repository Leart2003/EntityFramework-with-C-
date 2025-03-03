using ConsoleApp11;
using System;
using System.Linq;

class Program
{
    private readonly AppDbContext _context;
   
    
    static void Main(string[] args)
    {
        
        

         var context = new AppDbContext();

        Console.WriteLine("Choose an operation: (C)reate, (R)ead, (U)pdate, (D)elete");
        var choice = Console.ReadLine()?.ToUpper();

        switch (choice)
        {
            case "Create":
                CreateProduct(context);
                break;
            case "Read":
                ReadProducts(context);
                break;
            case "Update":
                UpdateProduct(context);
                break;
            case "Delete":
                DeleteProduct(context);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void CreateProduct(AppDbContext context)
    {
        Console.WriteLine("Enter product name:");
        var name = Console.ReadLine();
        
        Console.WriteLine("Enter product price:");
        var price = decimal.Parse(Console.ReadLine() ?? "0");

        var product = new Product { Name = name, Price = price };
        context.Products.Add(product);
        context.SaveChanges();

        Console.WriteLine("Product added successfully.");
    }

    static void ReadProducts(AppDbContext context)
    {
        var products = context.Products.ToList();
        Console.WriteLine("Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
        }
    }

    static void UpdateProduct(AppDbContext context)
    {
        Console.WriteLine("Enter the product ID to update:");
        var id = int.Parse(Console.ReadLine() ?? "0");

        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine("Enter new product name:");
        product.Name = Console.ReadLine();

        Console.WriteLine("Enter new product price:");
        product.Price = decimal.Parse(Console.ReadLine() ?? "0");

        context.SaveChanges();
        Console.WriteLine("Product updated successfully.");
    }

    static void DeleteProduct(AppDbContext context)
    {
        Console.WriteLine("Enter the product ID to delete:");
        var id = int.Parse(Console.ReadLine()?? "0");

        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        context.Products.Remove(product);
        context.SaveChanges();
        Console.WriteLine("Product deleted successfully.");
    }
}
