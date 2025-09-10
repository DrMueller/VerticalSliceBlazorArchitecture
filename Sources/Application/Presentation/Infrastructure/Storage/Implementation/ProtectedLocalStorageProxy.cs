using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage.Implementation
{
    [UsedImplicitly]
    internal class ProtectedLocalStorageProxy(ProtectedLocalStorage storage) : IProtectedLocalStorageProxy
    {
        public async ValueTask DeleteAsync(string key)
        {
            await storage.DeleteAsync(key);
        }

        public async ValueTask<Maybe<TValue>> GetAsync<TValue>(string key)
        {
            var storeResult = await storage.GetAsync<TValue>(key);

            return MaybeFactory.CreateFromNullable(storeResult.Value);
        }

        public async ValueTask SetAsync(string key, object value)
        {
            await storage.SetAsync(key, value);
        }
    }
}