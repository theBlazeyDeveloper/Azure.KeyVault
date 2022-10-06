using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.KeyVault
{
    public delegate void KeyVaultReadyHandler(object o, KeyVaultReadyEventArgs args);
    public delegate void KeyVaultProgressHandler(object o, KeyVaultProgressEventArgs args);
    public delegate void KeyVaultRetrievalCompletedHandler(object o, KeyVaultRetrievalCompletedEventArgs args);

    public delegate Task CallbackDelegate(IEnumerable<ISecret> secrets);
}
