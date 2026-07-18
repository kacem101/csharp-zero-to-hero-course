using System;
using System.Collections.Generic;
using System.Linq;

class ShoppingCart
{
    public List<(string Name, double Price, int Qty)> Items = new();

    // Genuinely one expression -> expression-bodied is fine and clearer.
    public int ItemCount() => Items.Sum(i => i.Qty);

    public bool IsEmpty() => Items.Count == 0;

    // Multiple named intermediate steps -> keep a real method body.
    // Forcing this into one expression (like the ComputeTotal example
    // above) would hide the discount/tax logic behind chained calls.
    public double GrandTotal()
    {
        double subtotal = Items.Sum(i => i.Price * i.Qty);
        double discount = Items.Count >= 5 ? subtotal * 0.10 : 0;
        double afterDiscount = subtotal - discount;
        return afterDiscount * 1.19;
    }
}

class Program
{
    static void Main()
    {
        var cart = new ShoppingCart();
        cart.Items.Add(("Keyboard", 50, 1));
        cart.Items.Add(("Mouse", 20, 2));
        Console.WriteLine(cart.ItemCount());   // 3
        Console.WriteLine(cart.IsEmpty());     // False
        Console.WriteLine(cart.GrandTotal());  // subtotal 90, no discount (only 2 distinct items), *1.19
    }
}
