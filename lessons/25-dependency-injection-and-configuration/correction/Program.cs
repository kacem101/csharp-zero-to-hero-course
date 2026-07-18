using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

public interface IMessageSender { void Send(string to, string msg); }
public class EmailSender : IMessageSender
{
    public void Send(string to, string msg) => Console.WriteLine($"Emailed {to}: {msg}");
}

public class NotificationOptions { public string DefaultSender { get; set; } = ""; }

public class NotificationService
{
    private readonly IMessageSender _sender;
    private readonly NotificationOptions _options;
    public NotificationService(IMessageSender sender, IOptions<NotificationOptions> options)
    {
        _sender = sender;
        _options = options.Value;
    }
    public void Notify(string user, string msg)
    {
        Console.WriteLine($"(sending as {_options.DefaultSender})");
        _sender.Send(user, msg);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        // TODO 2 — in-memory config instead of a real appsettings.json
        builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Notification:DefaultSender"] = "no-reply@myapp.com"
        });
        builder.Services.Configure<NotificationOptions>(builder.Configuration.GetSection("Notification"));

        // TODO 1
        builder.Services.AddSingleton<IMessageSender, EmailSender>();
        builder.Services.AddSingleton<NotificationService>();

        using var host = builder.Build();
        var service = host.Services.GetRequiredService<NotificationService>();
        service.Notify("belkacem@example.com", "Hello from DI");
    }
}

// TODO 3 answer: a Singleton is created ONCE and shared by every caller
// for the life of the app. If it holds per-user mutable state like
// `CurrentUserId`, every user sharing that instance would silently see
// (and overwrite) each other's data — user A's request could complete
// using user B's ID if requests interleave. This class of bug is subtle
// because it often doesn't show up in single-user local testing. Fix:
// register it as Scoped (one instance per request/logical scope) or
// Transient (a fresh instance every time), e.g.:
//   builder.Services.AddScoped<IUserContext, UserContext>();
// never AddSingleton for anything holding per-request/per-user state.
