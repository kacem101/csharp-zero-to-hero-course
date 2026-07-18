# Lesson 25 — Dependency Injection & Configuration

## Why this matters
Lesson 10 taught you to depend on interfaces instead of concrete classes (Dependency Inversion) — but you still manually `new`'d the implementation and passed it in by hand. Real .NET apps use a **DI container** to do that wiring automatically, and `IConfiguration` to keep settings (and secrets, per Lesson 22a) out of hardcoded values. This is exactly the machinery under ASP.NET Core, and it's available in plain console apps too via the Generic Host.

## The concept

### Manual wiring (what you've done so far) vs container-managed wiring
```csharp
// Manual — you did this in Lesson 10
IMessageSender sender = new EmailSender();
var service = new NotificationService(sender);
```
This doesn't scale once you have dozens of services depending on each other — you'd be manually threading every dependency through every constructor call by hand. A DI container does this automatically: you register "when something asks for `IMessageSender`, give it an `EmailSender`," and the container figures out the rest of the object graph.

### Service lifetimes
| Lifetime | One instance per... | Typical use |
|---|---|---|
| `Singleton` | the whole application | stateless services, configuration, shared caches |
| `Scoped` | one logical "scope" (e.g. one web request) | per-request state, a DB context |
| `Transient` | every single resolution | lightweight, stateless, cheap-to-create services |

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IMessageSender, EmailSender>();
builder.Services.AddSingleton<NotificationService>();

using var host = builder.Build();
var service = host.Services.GetRequiredService<NotificationService>();
service.Notify("belkacem@example.com", "Hello from DI");
```
Note `NotificationService` doesn't change at all from Lesson 10 — it still just takes `IMessageSender` in its constructor. The container is what decides *which* implementation to hand it, and *when* to create it.

### `IConfiguration` — settings without hardcoding
`appsettings.json`:
```json
{ "Notification": { "DefaultSender": "no-reply@myapp.com" } }
```
```csharp
public class NotificationOptions { public string DefaultSender { get; set; } = ""; }

builder.Services.Configure<NotificationOptions>(builder.Configuration.GetSection("Notification"));
```
Then inject `IOptions<NotificationOptions>` anywhere you need it:
```csharp
public class NotificationService
{
    private readonly IMessageSender _sender;
    private readonly NotificationOptions _options;
    public NotificationService(IMessageSender sender, IOptions<NotificationOptions> options)
    {
        _sender = sender;
        _options = options.Value;
    }
}
```
Configuration automatically layers `appsettings.json` with environment-variable overrides — exactly the "load secrets from the environment, not hardcoded" pattern from Lesson 22a, generalized to all settings, not just secrets.
