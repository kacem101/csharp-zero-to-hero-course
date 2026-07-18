using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PasswordManager.Domain;
using PasswordManager.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

const string vaultPath = "vault.dat";

builder.Services.AddSingleton<IEncryptionService, AesGcmEncryptionService>();
builder.Services.AddSingleton<IVaultRepository>(_ => new FileVaultRepository(vaultPath));
builder.Services.AddSingleton<VaultService>();

using var host = builder.Build();
var vault = host.Services.GetRequiredService<VaultService>();
var repository = host.Services.GetRequiredService<IVaultRepository>();

bool isNewVault = !repository.Exists();
Console.Write(isNewVault ? "No vault found. Set a new master password: " : "Master password: ");
string masterPassword = Console.ReadLine() ?? "";

try
{
    if (isNewVault)
    {
        vault.InitNew(masterPassword);
        vault.Save(masterPassword);
        Console.WriteLine("New vault created and saved.");
    }
    else
    {
        vault.Unlock(masterPassword);
        Console.WriteLine("Vault unlocked.");
    }
}
catch (VaultAuthenticationException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    return;
}
catch (VaultCorruptedException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    return;
}

Console.WriteLine("Commands: add <site> <user> <password> | generate <site> <user> <length> | get <site> | list | save | exit");

while (true)
{
    Console.Write("> ");
    string? line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) continue;

    var parts = line.Split(' ');
    string cmd = parts[0].ToLowerInvariant();

    try
    {
        switch (cmd)
        {
            case "add" when parts.Length == 4:
                vault.AddEntry(parts[1], parts[2], parts[3]);
                Console.WriteLine("Added (not yet saved — run 'save').");
                break;

            case "generate" when parts.Length == 4:
                int length = int.Parse(parts[3]);
                string generated = PasswordGenerator.Generate(length, includeSymbols: true);
                vault.AddEntry(parts[1], parts[2], generated);
                Console.WriteLine($"Generated and added: {generated}");
                break;

            case "get" when parts.Length == 2:
                var entry = vault.GetEntry(parts[1]);
                Console.WriteLine(entry is null
                    ? "(not found)"
                    : $"{entry.Site}: {entry.Username} / {entry.Password}");
                break;

            case "list":
                foreach (var site in vault.ListSites()) Console.WriteLine($"  {site}");
                break;

            case "save":
                vault.Save(masterPassword);
                Console.WriteLine("Vault saved.");
                break;

            case "exit":
                return;

            default:
                Console.WriteLine("Unknown command or wrong number of arguments.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid number format.");
    }
}
