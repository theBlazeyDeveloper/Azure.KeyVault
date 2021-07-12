using System;

namespace VisAbility.Azure.KeyVault
{
    public class KeyVaultReadyEventArgs : EventArgs
    {
        public bool IsReady { get; set; }

        public KeyVaultReadyEventArgs(bool IsReady)
        {
            this.IsReady = IsReady;
        }
    }
}
