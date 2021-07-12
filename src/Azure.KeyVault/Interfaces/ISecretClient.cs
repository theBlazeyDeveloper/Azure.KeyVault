using Azure.Security.KeyVault.Secrets;

namespace VisAbility.Azure.KeyVault
{
    public interface ISecretClient : IClient<ISecret>
    {
        SecretClient Client { get; }
    }
}
