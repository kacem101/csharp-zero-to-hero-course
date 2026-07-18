// CalculatorTests.cs (in MyApp.Tests)
using System;
using System.Collections.Generic;
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        var calculator = new Calculator();
        Assert.Equal(5, calculator.Add(2, 3));
    }

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-1, 1, 0)]
    [InlineData(0, 0, 0)]
    public void Add_VariousInputs_ReturnsExpectedSum(int a, int b, int expected)
    {
        var calculator = new Calculator();
        Assert.Equal(expected, calculator.Add(a, b));
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        var calculator = new Calculator();
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }
}

// From Lesson 10
public interface IMessageSender { void Send(string to, string msg); }
public class NotificationService
{
    private readonly IMessageSender _sender;
    public NotificationService(IMessageSender sender) => _sender = sender;
    public void Notify(string user, string msg) => _sender.Send(user, msg);
}

public class FakeSender : IMessageSender
{
    public List<(string To, string Msg)> Sent = new();
    public void Send(string to, string msg) => Sent.Add((to, msg));
}

public class NotificationServiceTests
{
    [Fact]
    public void Notify_ValidUser_SendsCorrectMessage()
    {
        var fake = new FakeSender();
        var service = new NotificationService(fake);

        service.Notify("belkacem@example.com", "Hello");

        Assert.Single(fake.Sent);
        Assert.Equal("belkacem@example.com", fake.Sent[0].To);
        Assert.Equal("Hello", fake.Sent[0].Msg);
    }
}

// TODO 3 fix: a good test checks OBSERVABLE BEHAVIOR (the return value,
// or — for something with a side effect — what that side effect was,
// like FakeSender.Sent above), never private implementation details.
// Checking a private field via reflection means the test breaks the
// moment you refactor the internals, even if the public behavior is
// completely unchanged — that's the opposite of what a test should
// protect you from. Rewritten to test behavior instead:
public class GoodCalculatorTest
{
    [Fact]
    public void Add_CalledTwice_EachCallReturnsCorrectSum()
    {
        var calc = new Calculator();
        Assert.Equal(5, calc.Add(2, 3));
        Assert.Equal(10, calc.Add(4, 6));
        // no assumption about HOW Calculator tracks anything internally —
        // just that calling Add() produces the right observable result
    }
}
