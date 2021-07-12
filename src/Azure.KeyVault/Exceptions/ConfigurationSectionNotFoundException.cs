using System;

namespace VisAbility.Azure.KeyVault
{
    internal class ConfigurationSectionNotFoundException : Exception
    {
        public ConfigurationSectionNotFoundException(string SectionName) : base($"{SectionName} section not found in appsettings.json file") { }
    }
}
