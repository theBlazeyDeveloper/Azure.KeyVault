using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace VisAbility.Azure.KeyVault
{
    internal class KeyVaultKeyClient : IKeyClient
    {
        readonly string BaseUrl;

        public KeyVaultKeyClient(KeyVaultOptions o)
        {
            Client = new KeyClient(o.VaultUri, new DefaultAzureCredential());

            VaultUri = Client.VaultUri;

            BaseUrl = ParseBaseUrlForSecretsMethod(Client.VaultUri.AbsoluteUri);
        }

        public KeyClient Client { get; }

        public Uri VaultUri { get; }

        public async Task<IKey> GetAsync(string Name)
        {
            var response = await TryGet(Name);
            var rawResponse = response.GetRawResponse();

            var StatusCode = rawResponse.Status;

            if (StatusCode == (int)HttpStatusCode.OK)
                return new Key(response.Value);

            else
                switch (StatusCode)
                {
                    case (int)HttpStatusCode.BadRequest:
                        ConsoleEx.WriteLineRed($"{HttpStatusCode.BadRequest}: {rawResponse.ReasonPhrase}");
                        break;
                    case (int)HttpStatusCode.Forbidden:
                        ConsoleEx.WriteLineRed($"{HttpStatusCode.Forbidden}: {rawResponse.ReasonPhrase}");
                        break;
                    case (int)HttpStatusCode.NotFound:
                        ConsoleEx.WriteLineRed($"{HttpStatusCode.NotFound}: {rawResponse.ReasonPhrase}");
                        break;

                    default: break;
                }
            return default;
        }

        public async IAsyncEnumerable<IKey> GetAsync()
        {
            await foreach (var k in Client.GetPropertiesOfKeysAsync())
            {
                // Getting a disabled secret will fail, so skip disabled secrets.
                if (!k.Enabled.GetValueOrDefault())
                    continue;

                var key = await GetAsync(k.Name);

                if (key != null)
                    yield return key;
            }
        }

        async Task<Response<KeyVaultKey>> TryGet(string Name) => await Client.GetKeyAsync(Name);

        static string ParseBaseUrlForSecretsMethod(string BaseUrl)
        {
            var TempUrl = BaseUrl;

            return TempUrl.Remove(TempUrl.Length - 8);
        }
    }
}