using Azure.Security.KeyVault.Keys;
using System;
using System.Collections.Generic;

namespace VisAbility.Azure.KeyVault
{
    public class Key : IKey
    {
        public Key(KeyVaultKey k)
        {
            Id = k.Id;
            Name = k.Name;
            KeyType = k.KeyType;
            Properties = k.Properties;
            JsonKey = k.Key;

            if (Properties.Tags != null)
            {
                HasTags = Properties.Tags.Count > 0;

                if (HasTags)
                    foreach (var item in k.Properties.Tags)
                        Tags.Add(new Tag(item.Key, item.Value));
            }
        }

        public Uri Id { get; }
        public string Name { get; }
        public bool HasTags { get; }
        public KeyType KeyType { get; }
        public KeyProperties Properties { get; }
        public JsonWebKey JsonKey { get; }
        public IList<Tag> Tags { get; } = new List<Tag>();
    }
}
