using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;

namespace Azure.KeyVault
{
    public record Secret : ISecret
    {
        public Secret(KeyVaultSecret s)
        {
            Id = s.Id;
            Value = s.Value;
            Name = s.Name;
            ContentType = s.Properties.ContentType;
            Properties = s.Properties ?? new SecretProperties(s.Name);

            if (Properties.Tags != null)
            {
                HasTags = Properties.Tags.Count > 0;

                if (HasTags)
                    foreach (var item in s.Properties.Tags)
                        Tags.Add(new Tag(item.Key, item.Value));
            }
        }

        public Uri Id { get; set; }

        public string Value { get; }

        public string Name { get; }

        public string ContentType { get; }

        public bool HasTags { get; }

        public SecretProperties Properties { get; }

        public IList<Tag> Tags { get; } = new List<Tag>();
    }
}