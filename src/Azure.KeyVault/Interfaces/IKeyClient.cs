using Azure.Security.KeyVault.Keys;

namespace Azure.KeyVault
{
    public interface IKeyClient : IClient<IKey>
    {
        KeyClient Client { get; }
    }
}
