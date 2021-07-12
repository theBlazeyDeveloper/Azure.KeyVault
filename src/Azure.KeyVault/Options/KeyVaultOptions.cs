using Microsoft.Extensions.Configuration;
using System;

namespace VisAbility.Azure.KeyVault
{
    public class KeyVaultOptions
    {
        const string SectionName = "KeyVault";
        const string ValueName = "BaseUri";

        public Uri VaultUri { get; set; }

        /// <summary>
        /// Add a section in your appsettings.json file called 
        /// "KeyVault" : {
        ///     "BaseUri" : "https://[Your Key Vault name here].vault.azure.net"
        /// } 
        /// </summary>
        /// <param name="Config">Instance of IConfiguration</param>
        public void GetKeyVaultFromAppSettings(IConfiguration Config)
        {
            var keyVaultSection = Config.GetSection(SectionName);

            if (keyVaultSection == null)
                throw new ConfigurationSectionNotFoundException(SectionName);

            if (keyVaultSection.Exists())
            {
                var baseUri = keyVaultSection.GetValue<string>(ValueName);

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
