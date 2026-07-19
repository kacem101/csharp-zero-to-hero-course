// TODO 1: Given List<int> numbers = new() { 1,2,3,4,5,6,7,8,9,10 }, write
// a LINQ query that returns the squares of the even numbers, then print
// them. Use method syntax (Where + Select).

// TODO 2: Prove deferred execution to yourself: build a query over a
// List<int> that filters values > some threshold, print "query built",
// THEN mutate the underlying list (Add a new large value) BEFORE
// enumerating the query. Does the new value show up in the results?
// Explain why in a comment.

// TODO 3: Write a LINQ pipeline using GroupBy that takes a list of
// (string Name, string Department) records and returns, for each
// department, the count of employees in it.

// TODO 4 (bug hunt): what's inefficient here, and how would you fix it?
IEnumerable<int> expensiveQuery = numbers.Where(n => n % 2 == 0).Select(n => n * n);
int count = expensiveQuery.Count();      // enumerates once
var list = expensiveQuery.ToList();      // enumerates AGAIN, from scratch
