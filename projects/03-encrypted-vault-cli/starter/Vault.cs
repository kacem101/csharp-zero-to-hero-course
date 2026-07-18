using System;
using System.Collections.Generic;

public class Vault
{
    private readonly Dictionary<string, string> _entries = new();

    public void Add(string key, string value) => throw new NotImplementedException();
    public string? Get(string key) => throw new NotImplementedException();
    public void Remove(string key) => throw new NotImplementedException();
    public IEnumerable<string> ListKeys() => throw new NotImplementedException();

    public static Vault LoadOrCreate(string path, string masterPassword) => throw new NotImplementedException();
    public void Save(string path, string masterPassword) => throw new NotImplementedException();
}
