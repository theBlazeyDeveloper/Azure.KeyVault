using Azure.Security.KeyVault.Keys;

namespace VisAbility.Azure.KeyVault
{
    public interface IKeyClient : IClient<IKey>
    {
        KeyClient Client { get; }
    }
}
