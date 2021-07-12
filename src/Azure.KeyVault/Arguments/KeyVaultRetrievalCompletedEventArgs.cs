using System;
using System.Collections.Generic;

namespace VisAbility.Azure.KeyVault
{
    public class KeyVaultRetrievalCompletedEventArgs : EventArgs
    {
        public bool IsComplete { get; set; }

        public IEnumerable<ISecret> Secrets { get; }

        public KeyVaultRetrievalCompletedEventArgs(bool IsComplete, IEnumerable<ISecret> Secrets)
        {
            this.Secrets = Secrets;
            this.IsComplete = IsComplete;
        }
    }
}
