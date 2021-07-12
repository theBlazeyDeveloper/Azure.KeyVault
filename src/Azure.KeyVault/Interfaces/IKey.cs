using Azure.Security.KeyVault.Keys;
using System;
using System.Collections.Generic;

namespace VisAbility.Azure.KeyVault
{
    public interface IKey
    {
        Uri Id { get; }
        string Name { get; }
        bool HasTags { get; }
        JsonWebKey JsonKey { get; }
        KeyType KeyType { get; }
        KeyProperties Properties { get; }
        IList<Tag> Tags { get; }
    }
}
