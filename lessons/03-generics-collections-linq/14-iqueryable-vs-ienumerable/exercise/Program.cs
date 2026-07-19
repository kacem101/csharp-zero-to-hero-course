// Assume: IQueryable<Order> Orders (backed by a database)

// TODO 1: Write the QUERYABLE (efficient) version of: "get the total
// amount of all orders placed by customer 42, only for orders over $50."
// Keep filtering as IQueryable for as long as possible; only call
// .ToList() (or .Sum() directly) at the very end.

// TODO 2 (bug hunt): identify exactly which line converts this from a
// database-side query into an in-memory one, and why that's expensive.
var allOrders = Orders.ToList();
var customerOrders = allOrders.Where(o => o.CustomerId == 42);
var bigOrders = customerOrders.Where(o => o.Amount > 50);
var total = bigOrders.Sum(o => o.Amount);

// TODO 3: Rewrite TODO 2's query to be fully IQueryable until the final
// materialization.
