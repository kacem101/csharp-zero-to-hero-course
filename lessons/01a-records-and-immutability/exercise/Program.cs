// TODO 1: Define a positional record `Coordinate(double Lat, double Lng)`.
// Create two instances with identical values and prove == returns true
// while ReferenceEquals returns false.

// TODO 2: Define `record Employee(string Name, string Department, decimal
// Salary)`. Create one, then use a with-expression to give a 15% raise,
// storing the result in a new variable. Print both the original and
// raised salary to prove the original wasn't mutated.

// TODO 3: Records support deconstruction. Deconstruct a Coordinate into
// two separate `double lat, double lng` variables using pattern
// `var (lat, lng) = coordinate;` and print them.

// TODO 4 (bug hunt): why won't this compile, and what's the correct fix?
var emp = new Employee("Yanis", "Sales", 2000);
emp.Salary = 2200; // ???
