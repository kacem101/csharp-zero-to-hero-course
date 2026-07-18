# Correction — Lesson 26: Solution & Project Structure

Full command sequence:

```bash
mkdir TaskTracker && cd TaskTracker
dotnet new sln -n TaskTracker

dotnet new classlib -o src/TaskTracker.Domain
dotnet new console  -o src/TaskTracker.Console
dotnet new xunit    -o tests/TaskTracker.Tests

dotnet sln add src/TaskTracker.Domain/TaskTracker.Domain.csproj
dotnet sln add src/TaskTracker.Console/TaskTracker.Console.csproj
dotnet sln add tests/TaskTracker.Tests/TaskTracker.Tests.csproj

dotnet add src/TaskTracker.Console/TaskTracker.Console.csproj reference src/TaskTracker.Domain/TaskTracker.Domain.csproj
dotnet add tests/TaskTracker.Tests/TaskTracker.Tests.csproj reference src/TaskTracker.Domain/TaskTracker.Domain.csproj
```

`src/TaskTracker.Domain/TaskItem.cs`:
```csharp
namespace TaskTracker.Domain;

public record TaskItem(string Title, bool IsDone);

public static class TaskOperations
{
    public static TaskItem MarkDone(TaskItem task) => task with { IsDone = true };
}
```

`src/TaskTracker.Console/Program.cs` (replace the default `dotnet new console` boilerplate):
```csharp
using TaskTracker.Domain;

var task = new TaskItem("Write the DevSec report", IsDone: false);
var updated = TaskOperations.MarkDone(task);

Console.WriteLine(task);    // TaskItem { Title = Write the DevSec report, IsDone = False }
Console.WriteLine(updated); // TaskItem { Title = Write the DevSec report, IsDone = True }
```

`tests/TaskTracker.Tests/TaskOperationsTests.cs` (delete the default `UnitTest1.cs` first):
```csharp
using TaskTracker.Domain;
using Xunit;

public class TaskOperationsTests
{
    [Fact]
    public void MarkDone_IncompleteTask_ReturnsNewTaskMarkedDone()
    {
        var original = new TaskItem("Sample", IsDone: false);

        var result = TaskOperations.MarkDone(original);

        Assert.True(result.IsDone);
        Assert.False(original.IsDone); // original untouched — records are immutable (Lesson 01a)
        Assert.Equal(original.Title, result.Title);
    }
}
```

Then, from the `TaskTracker/` root:
```bash
dotnet build   # all three projects compile
dotnet test    # the [Fact] above runs and passes
```

Resulting tree:
```
TaskTracker/
├── TaskTracker.sln
├── src/
│   ├── TaskTracker.Domain/
│   │   ├── TaskTracker.Domain.csproj
│   │   └── TaskItem.cs
│   └── TaskTracker.Console/
│       ├── TaskTracker.Console.csproj
│       └── Program.cs
└── tests/
    └── TaskTracker.Tests/
        ├── TaskTracker.Tests.csproj
        └── TaskOperationsTests.cs
```

**Common mistakes to watch for:**
- Letting `Domain` reference `Console` or `Infrastructure` "just this once" — even one such reference breaks the whole point of the layered structure and tends to spread.
- Forgetting `dotnet sln add` — the project can build individually but won't be included when you run `dotnet build`/`dotnet test` from the solution root.
- Mixing a NuGet `PackageReference` and a project `ProjectReference` up conceptually — a package reference is an external, versioned dependency; a project reference is to code you own in the same solution.
