

using System;
using System.Collections.Generic; 


//Methods to display cart items
void DisplayCart(List<string> items, string title)
{

    Console.WriteLine($"\n Items in the {title}: has {items.Count} items"  );
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

//Method to add an item to the cart
void AddToCart(List<string> cart, string item)
{
    if (!string.IsNullOrWhiteSpace(item))
    {
        cart.Add(item);
        Console.WriteLine($"Added '{item}' to the cart.");
    }
    else
    {
        Console.WriteLine("Item cannot be empty.");
    }
}

List<string> mycart = new List<string>();
DisplayCart(mycart, "My Cart");

// Add items to the cart
AddToCart(mycart, "Apple");
DisplayCart(mycart, "My Cart");
AddToCart(mycart, "Banana");
DisplayCart(mycart, "My Cart");
AddToCart(mycart, "Orange");
DisplayCart(mycart, "My Cart");


// Key: Product Code (string), Value: Price (decimal)

// Key  Product Code (string), Value: Price (decimal)
Dictionary<string, decimal> productPrices = new Dictionary<string, decimal>
{
    { "P001", 1.99m },
    { "P002", 0.99m },
    { "P003", 2.49m }
}
;
void DisplayProductPrices(Dictionary<string, decimal> prices )
{
    Console.WriteLine("\nProduct Prices:");
    foreach (var product in prices)
    {
        Console.WriteLine($"Product Code: {product.Key}, Price: {product.Value:C}");
    }
}

void AddOrUpdateProductPrice(Dictionary<string, decimal> catalog, string productCode, decimal newprice)
{
    if (catalog.ContainsKey(productCode))
    {
        catalog[productCode] = newprice;
        Console.WriteLine($"Updated price for {productCode} to {newprice:C}");
    }
    else
    {
        catalog.Add(productCode, newprice);
        Console.WriteLine($"Added new product {productCode} with price {newprice:C}");
    }
}
DisplayProductPrices(productPrices);
// Add or update product prices
AddOrUpdateProductPrice(productPrices, "P001", 2.19m);
AddOrUpdateProductPrice(productPrices, "P004", 3.99m);
DisplayProductPrices(productPrices);