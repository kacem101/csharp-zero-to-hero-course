# Lesson 26 — Solution & Project Structure

## Why this matters
Every lesson so far has lived in one `.csproj` with one `Program.cs`. Real applications — including SMAI's five-project layout — are made of multiple projects wired together with clear boundaries: business logic that doesn't know about databases, infrastructure that implements storage/network access, an entry point that wires everything together, and tests that depend only on what they're testing. Getting this structure right early prevents circular dependencies and "everything depends on everything" tangles later.

## The concept

### The building blocks
- **`.sln`** (solution file) — a container that groups multiple `.csproj` projects so tools (and `dotnet` commands) can operate on all of them together.
- **`.csproj`** (project file) — one deployable/buildable unit: a class library, a console app, a test project, etc.
- **Project references** — one project depending on another *within the same solution* (`dotnet add reference`), as opposed to a NuGet package (an external dependency).

### A typical layered structure
```
MyApp/
├── MyApp.sln
├── src/
│   ├── MyApp.Domain/         # pure business logic — no dependencies on anything else
│   │   └── MyApp.Domain.csproj
│   ├── MyApp.Infrastructure/ # database, HTTP clients, file access — depends on Domain
│   │   └── MyApp.Infrastructure.csproj
│   └── MyApp.Console/        # entry point / composition root — depends on Domain + Infrastructure
│       └── MyApp.Console.csproj
└── tests/
    └── MyApp.Tests/          # depends only on the project(s) it's testing
        └── MyApp.Tests.csproj
```
**The dependency direction matters:** `Domain` should never reference `Infrastructure` or `Console` — business rules shouldn't know or care whether data comes from SQL Server, a REST API, or a flat file. This is the Dependency Inversion principle from Lesson 10, applied at the project level instead of just the class level.

### Building it with the CLI
```bash
mkdir MyApp && cd MyApp
dotnet new sln -n MyApp

dotnet new classlib -o src/MyApp.Domain
dotnet new console  -o src/MyApp.Console
dotnet new xunit    -o tests/MyApp.Tests

dotnet sln add src/MyApp.Domain/MyApp.Domain.csproj
dotnet sln add src/MyApp.Console/MyApp.Console.csproj
dotnet sln add tests/MyApp.Tests/MyApp.Tests.csproj

# Console depends on Domain:
dotnet add src/MyApp.Console/MyApp.Console.csproj reference src/MyApp.Domain/MyApp.Domain.csproj
# Tests depend on Domain:
dotnet add tests/MyApp.Tests/MyApp.Tests.csproj reference src/MyApp.Domain/MyApp.Domain.csproj

dotnet build   # builds every project in the solution
dotnet test    # runs every test project in the solution
```

### NuGet packages vs project references
A **project reference** points at another project *you own*, in the same solution. A **NuGet package reference** pulls in an external, versioned dependency (like the `Microsoft.Extensions.Hosting` package used in Lesson 25):
```bash
dotnet add src/MyApp.Console/MyApp.Console.csproj package Microsoft.Extensions.Hosting
```
which adds this to the `.csproj`:
```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
</ItemGroup>
```

### Keeping versions consistent across many projects
On a team project with several `.csproj` files (like SMAI's five-project layout), it's easy for different projects to drift onto different versions of the same package. A `Directory.Build.props` file at the solution root, or **Central Package Management** (a `Directory.Packages.props` file with `<PackageVersion>` entries and `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>`), lets you declare a package's version exactly once for the whole solution — worth knowing exists even if you don't need it on smaller projects yet.
