using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace VisAbility.Azure.KeyVault
{
    internal class KeyVaultSecretClient : ISecretClient
    {
        readonly string BaseUrl;

        public KeyVaultSecretClient(KeyVaultOptions o)
        {
            Client = new SecretClient(o.VaultUri, new DefaultAzureCredential());

            VaultUri = Client.VaultUri;

            BaseUrl = ParseBaseUrlForSecretsMethod(Client.VaultUri.AbsoluteUri);
        }

        public SecretClient Client { get; }
        public Uri VaultUri { get; }

        public async Task<ISecret> GetAsync(string Name)
        {
            var response = await TryGet(Name);
            var rawResponse = response.GetRawResponse();

            var StatusCode = rawResponse.Status;

            if (StatusCode == (int)HttpStatusCode.OK)
                return new Secret(response.Value);

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

        public async IAsyncEnumerable<ISecret> GetAsync()
        {
            await foreach (var s in Client.GetPropertiesOfSecretsAsync())
            {
                // Getting a disabled secret will fail, so skip disabled secrets.
                if (!s.Enabled.GetValueOrDefault())
                    continue;

                var secret = await GetAsync(s.Name);

                if (secret != null)
                    yield return secret;
            }
        }

        async Task<Response<KeyVaultSecret>> TryGet(string Name) => await Client.GetSecretAsync(Name);

        static string ParseBaseUrlForSecretsMethod(string BaseUrl)
        {
            var TempUrl = BaseUrl;

            return TempUrl.Remove(TempUrl.Length - 8);
        }
    }
}