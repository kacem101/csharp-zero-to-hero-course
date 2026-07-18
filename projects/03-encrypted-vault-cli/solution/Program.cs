using System;

class Program
{
    const string VaultPath = "vault.dat";

    static void Main()
    {
        bool isNewVault = !System.IO.File.Exists(VaultPath);
        Console.Write(isNewVault ? "No vault found. Set a new master password: " : "Master password: ");
        string masterPassword = Console.ReadLine() ?? "";

        Vault vault;
        try
        {
            vault = Vault.LoadOrCreate(VaultPath, masterPassword);
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

        if (isNewVault)
        {
            vault.Save(VaultPath, masterPassword);
            Console.WriteLine("New vault created.");
        }
        else
        {
            Console.WriteLine("Vault unlocked.");
        }

        Console.WriteLine("Commands: add <key> <value> | get <key> | list | remove <key> | save | exit");

        while (true)
        {
            Console.Write("> ");
            string? line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(' ', 3);
            string cmd = parts[0].ToLowerInvariant();

            switch (cmd)
            {
                case "add" when parts.Length == 3:
                    vault.Add(parts[1], parts[2]);
                    Console.WriteLine("Added (not yet saved — run 'save').");
                    break;
                case "get" when parts.Length == 2:
                    string? value = vault.Get(parts[1]);
                    Console.WriteLine(value ?? "(not found)");
                    break;
                case "list":
                    foreach (var key in vault.ListKeys()) Console.WriteLine($"  {key}");
                    break;
                case "remove" when parts.Length == 2:
                    vault.Remove(parts[1]);
                    Console.WriteLine("Removed (not yet saved — run 'save').");
                    break;
                case "save":
                    vault.Save(VaultPath, masterPassword);
                    Console.WriteLine("Vault saved.");
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Unknown command or wrong number of arguments.");
                    break;
            }
        }
    }
}
