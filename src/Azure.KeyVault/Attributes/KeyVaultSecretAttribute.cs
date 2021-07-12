using System;

namespace VisAbility.Azure.KeyVault
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyVaultSecretAttribute : Attribute
    {
        public KeyVaultSecretAttribute()
        { }

        public string Name { get; set; }
    }
}