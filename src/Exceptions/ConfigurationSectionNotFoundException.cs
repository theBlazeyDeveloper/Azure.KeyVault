using System;

namespace Azure.KeyVault
{
    internal class ConfigurationSectionNotFoundException : Exception
    {
        public ConfigurationSectionNotFoundException(string sectionName) 
            : base($"{sectionName} section not found in appsettings.json file") { }
    }
}
