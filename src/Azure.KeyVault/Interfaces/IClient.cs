using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisAbility.Azure.KeyVault
{
    public interface IClient<T>
    {
        Uri VaultUri { get; }

        Task<T> GetAsync(string Name);
        IAsyncEnumerable<T> GetAsync();
    }
}
