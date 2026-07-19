// TODO 1: Given this class, convert the properties/methods that are
// genuinely one-liners into expression-bodied members, and leave the
// multi-step one as a full method body with clear intermediate steps.
class ShoppingCart
{
    public List<(string Name, double Price, int Qty)> Items = new();

    public int ItemCount()
    {
        int total = 0;
        foreach (var item in Items) total += item.Qty;
        return total;
    }

    public bool IsEmpty()
    {
        return Items.Count == 0;
    }

    public double GrandTotal()
    {
        // Apply 10% discount if there are 5+ distinct items, plus 19% tax.
        double subtotal = 0;
        foreach (var item in Items) subtotal += item.Price * item.Qty;
        double discount = Items.Count >= 5 ? subtotal * 0.10 : 0;
        double afterDiscount = subtotal - discount;
        return afterDiscount * 1.19;
    }
}
