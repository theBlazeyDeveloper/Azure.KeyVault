using Azure.KeyVault;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegistryExtensions
    {
        public static void AddKeyVault(this IServiceCollection services, Action<KeyVaultOptions> options)
        {
            services.Configure(options);

            services.TryAddSingleton<IKeyClient>(sp =>
            {
                var o = sp.GetRequiredService<IOptions<KeyVaultOptions>>();

                if (o.Value.VaultUri == null)
                    throw new NullReferenceException(nameof(o.Value.VaultUri));

                return new KeyVaultKeyClient(o.Value);
            });

            services.TryAddSingleton<ISecretClient>(sp =>
            {
                var o = sp.GetRequiredService<IOptions<KeyVaultOptions>>();

                if (o.Value.VaultUri == null)
                    throw new NullReferenceException(nameof(o.Value.VaultUri));

                return new KeyVaultSecretClient(o.Value);
            });

            services.TryAddSingleton<ISecretManager, SecretManager>();
        }
    }
}
