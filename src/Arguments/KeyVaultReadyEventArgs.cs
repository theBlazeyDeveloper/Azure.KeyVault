using System;

namespace Azure.KeyVault
{
    public class KeyVaultReadyEventArgs : EventArgs
    {
        public bool IsReady { get; set; }

        public KeyVaultReadyEventArgs(bool isReady)
        {
            IsReady = isReady;
        }
    }
}
