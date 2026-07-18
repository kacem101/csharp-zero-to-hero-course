using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PasswordManager.Domain;
using PasswordManager.Infrastructure;

// TODO:
// 1. Build a Host with Host.CreateApplicationBuilder(args).
// 2. Register IEncryptionService -> AesGcmEncryptionService (Singleton).
// 3. Register IVaultRepository -> a FileVaultRepository pointed at "vault.dat"
//    (you'll need a factory registration since it takes a constructor arg).
// 4. Register VaultService (Singleton).
// 5. Resolve VaultService from the built host.
// 6. Run the CLI loop: init/unlock, add, get, list, generate, save, exit.
//    Catch VaultAuthenticationException / VaultCorruptedException with
//    clean error messages, never a raw stack trace.
