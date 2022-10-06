using System;

namespace Azure.KeyVault
{
    public class KeyVaultProgressEventArgs : EventArgs
    {
        public KeyVaultProgressEventArgs(int addedSecrets, int updatedSecrets, ISecret secret)
        {
            Secret = secret;
            AddedSecrets = addedSecrets;
            UpdatedSecrets = updatedSecrets;
        }

        public int AddedSecrets { get; }
        public int UpdatedSecrets { get; }
        public ISecret Secret { get; }
    }
}
