using Azure.Security.KeyVault.Secrets;

namespace Azure.KeyVault
{
    public interface ISecretClient : IClient<ISecret>
    {
        SecretClient Client { get; }
    }
}
