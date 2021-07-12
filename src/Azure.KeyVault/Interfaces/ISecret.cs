using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;

namespace VisAbility.Azure.KeyVault
{
    public interface ISecret
    {
        Uri Id { get; }
        string Value { get; }
        string Name { get; }
        string ContentType { get; }
        bool HasTags { get; }
        SecretProperties Properties { get; }
        IList<Tag> Tags { get; }
    }
}
