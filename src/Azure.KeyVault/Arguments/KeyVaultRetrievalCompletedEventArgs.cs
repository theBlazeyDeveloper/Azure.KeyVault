using System;
using System.Collections.Generic;

namespace Azure.KeyVault
{
    public class KeyVaultRetrievalCompletedEventArgs : EventArgs
    {
        public bool IsComplete { get; set; }

        public IEnumerable<ISecret> Secrets { get; }

        public KeyVaultRetrievalCompletedEventArgs(bool isComplete, IEnumerable<ISecret> secrets)
        {
            Secrets = secrets;
            IsComplete = isComplete;
        }
    }
}
