using System;

namespace VisAbility.Azure.KeyVault
{
    public class KeyVaultProgressEventArgs : EventArgs
    {
        public KeyVaultProgressEventArgs(int AddedSecrets, int UpdatedSecrets, ISecret Secret)
        {
            this.Secret = Secret;
            this.AddedSecrets = AddedSecrets;
            this.UpdatedSecrets = UpdatedSecrets;
        }

        public int AddedSecrets { get; }
        public int UpdatedSecrets { get; }
        public ISecret Secret { get; }
    }
}
