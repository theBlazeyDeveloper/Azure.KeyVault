using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.KeyVault
{
    public interface ISecretManager
    {
        ValueTask RetrieveSecretsAsync(CallbackDelegate Callback = null);

        bool IsBusy { get; }

        IEnumerable<ISecret> Secrets { get; }

        event KeyVaultReadyHandler OnManagerReady;

        event KeyVaultProgressHandler OnProgressUpdated;

        event KeyVaultRetrievalCompletedHandler OnCompleted;
    }
}