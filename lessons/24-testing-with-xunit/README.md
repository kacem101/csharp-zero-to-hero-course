# Lesson 24 — Testing with xUnit

## Why this matters
Without tests, every refactor is a guess and every bug fix risks silently breaking something else. xUnit is the standard testing framework in the .NET ecosystem, and it directly rewards the design habits from Lesson 10 (Dependency Inversion) — code built around interfaces is exactly the code that's easy to test.

## The concept

### Project setup
```bash
dotnet new xunit -o MyApp.Tests
cd MyApp.Tests
dotnet add reference ../MyApp/MyApp.csproj   # reference the project under test
dotnet test                                    # runs all tests (not `dotnet run`)
```

### `[Fact]` — a single, unconditional test
```csharp
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();
        // Act
        int result = calculator.Add(2, 3);
        // Assert
        Assert.Equal(5, result);
    }
}
```
**Arrange-Act-Assert** is the standard shape: set up your inputs, call the thing you're testing, check the outcome. The test name (`MethodName_Scenario_ExpectedBehavior`) should describe the behavior well enough that a failing test tells you what broke without opening the code.

### `[Theory]` + `[InlineData]` — one test, many inputs
```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(-1, 1, 0)]
[InlineData(0, 0, 0)]
public void Add_VariousInputs_ReturnsExpectedSum(int a, int b, int expected)
{
    var calculator = new Calculator();
    Assert.Equal(expected, calculator.Add(a, b));
}
```
This runs the same test body three times, once per `InlineData` row — avoids copy-pasting near-identical `[Fact]` methods.

### Testing exceptions
```csharp
[Fact]
public void Divide_ByZero_ThrowsDivideByZeroException()
{
    var calculator = new Calculator();
    Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
}
```

### Testing code with dependencies — this is where Lesson 10 pays off
```csharp
// From Lesson 10:
// interface IMessageSender { void Send(string to, string msg); }
// class NotificationService { ... constructor takes IMessageSender ... }

class FakeSender : IMessageSender
{
    public List<(string To, string Msg)> Sent = new();
    public void Send(string to, string msg) => Sent.Add((to, msg));
}

[Fact]
public void Notify_ValidUser_SendsCorrectMessage()
{
    var fake = new FakeSender();
    var service = new NotificationService(fake); // inject the FAKE, not a real EmailSender

    service.Notify("belkacem@example.com", "Hello");

    Assert.Single(fake.Sent);
    Assert.Equal("belkacem@example.com", fake.Sent[0].To);
}
```
No real email is ever sent, no network call happens, and the test runs in milliseconds — this is only possible because `NotificationService` depends on an *interface*, not a concrete `EmailSender`. If it had been hardcoded to `new EmailSender()` internally (the Dependency Inversion violation from Lesson 10), this test would be impossible to write cleanly.
