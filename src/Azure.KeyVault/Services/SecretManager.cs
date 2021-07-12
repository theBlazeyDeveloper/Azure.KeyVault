using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisAbility.Azure.KeyVault
{
    internal class SecretManager : ISecretManager
    {
        bool isReady;
        int AddedSecrets;
        int UpdatedSecrets;
        readonly ISecretClient Client;
        readonly IList<CallbackDelegate> Callbacks = new List<CallbackDelegate>();
        readonly static ConcurrentDictionary<string, ISecret> SecretDictionary = new ConcurrentDictionary<string, ISecret>();

        public SecretManager(ISecretClient Client)
        {
            this.Client = Client;
        }

        public bool IsBusy
        {
            get => isReady;
            set
            {
                isReady = value;

                if (value)
                    OnManagerReady?.Invoke(this, new KeyVaultReadyEventArgs(!IsBusy));
            }
        }
        public event KeyVaultReadyHandler OnManagerReady;
        public event KeyVaultProgressHandler OnProgressUpdated;
        public event KeyVaultRetrievalCompletedHandler OnCompleted;

        public IEnumerable<ISecret> Secrets { get => SecretDictionary.Values; }
        public async ValueTask RetrieveSecretsAsync(CallbackDelegate Callback = null)
        {
            if (Callback != null)
                AddCallBack(Callback);

            if (!IsBusy)
                await StartGettingSecrets();

            else
                Console.WriteLine("Already aquiring secrets...");
        }

        async ValueTask StartGettingSecrets()
        {
            try
            {
                IsBusy = true;

                await AddSecrets();

                await InvokeCallbacks();

                ConsoleEx.WriteLineGreen($"{AddedSecrets} secrets added {UpdatedSecrets} secrets updated");

                IsBusy = false;

                ConsoleEx.WriteLineRed("All Secrets have been added/updated");
            }
            catch (Exception ex)
            {
                ConsoleEx.WriteLineRed(Ex: ex);
            }
        }
        void AddCallBack(CallbackDelegate d) => Callbacks.Add(d);
        async ValueTask InvokeCallbacks()
        {
            foreach (var c in Callbacks)
                await c?.Invoke(SecretDictionary.Values);
        }
        async ValueTask AddSecrets()
        {
            ResetCount();

            ConsoleEx.WriteLineYellow("Adding secrets...");

            await foreach (var item in Client.GetAsync())
            {
                if (SecretDictionary.TryAdd(item.Name, item))
                    AddedSecrets++;

                else if (SecretDictionary.TryUpdate(item.Name, item, item))
                    UpdatedSecrets++;

                OnProgressUpdated?.Invoke(this, new KeyVaultProgressEventArgs(AddedSecrets, UpdatedSecrets, item));
            }

            OnCompleted?.Invoke(this, new KeyVaultRetrievalCompletedEventArgs(true, SecretDictionary.Values));
        }
        void ResetCount() { AddedSecrets = 0; UpdatedSecrets = 0; }
    }
}