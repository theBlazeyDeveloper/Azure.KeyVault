using Microsoft.Extensions.Configuration;
using System;

namespace Azure.KeyVault
{
    public class KeyVaultOptions
    {
        const string _sectionName = "KeyVault";
        const string _valueName = "BaseUri";

        public Uri VaultUri { get; set; }

        /// <summary>
        /// Add a section in your appsettings.json file called 
        /// "KeyVault" : {
        ///     "BaseUri" : "https://[Your Key Vault name here].vault.azure.net"
        /// } 
        /// </summary>
        /// <param name="config">Instance of IConfiguration</param>
        public void GetKeyVaultFromAppSettings(IConfiguration config)
        {
            var keyVaultSection = config.GetSection(_sectionName);

            if (keyVaultSection == null)
                throw new ConfigurationSectionNotFoundException(_sectionName);

            if (keyVaultSection.Exists())
            {
                var baseUri = keyVaultSection.GetValue<string>(_valueName);

                if (!string.IsNullOrEmpty(baseUri))
                    VaultUri = new Uri(baseUri);
            }
        }

        /// <summary>
        /// Sets the Base Uri for the Key Vault. This is the Uri without /Secrets or /Keys at the end
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public KeyVaultOptions SetVaultUri(string Uri)
        {
            VaultUri = new Uri(Uri);

            return this;
        }
    }
}
