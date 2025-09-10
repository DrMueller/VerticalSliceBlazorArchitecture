using JetBrains.Annotations;
using Microsoft.JSInterop;
using VerticalSliceBlazorArchitecture.Common.Extensions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Disposing;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage.Implementation
{
    [PublicAPI]
    internal sealed class LocalStorageProxy(
        IJavaScriptLocator jsLocator,
        IJSRuntime jsRuntime) : ILocalStorageProxy
    {
        public const string GetItemMethod = "getItem";
        public const string JavaScriptStoragePath = "./js/localstorage.js";
        public const string SetItemMethod = "setItem";

        private Lazy<IJSObjectReference> _accessorJsRef = new();

        public async ValueTask DisposeAsync()
        {
            await ComponentDisposeHandler.HandleDisposeAsync(async () =>
            {
                if (_accessorJsRef.IsValueCreated)
                {
                    await _accessorJsRef.Value.DisposeAsync();
                }
            });
        }

        public async Task<Maybe<bool>> GetBoolAsync(string key)
        {
            await WaitForReferenceAsync();
            var value = await _accessorJsRef.Value
                .InvokeAsync<string?>(GetItemMethod, key)
                .MapAsync(MaybeFactory.CreateFromNullable);

            return value.Map(bool.Parse);
        }

        public async Task SetItemAsync(string key, string value)
        {
            await WaitForReferenceAsync();
            await _accessorJsRef.Value.InvokeVoidAsync(SetItemMethod, key, value);
        }

        private async Task WaitForReferenceAsync()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                var jsPath = await jsLocator.LocateAbsoluteJsFilePathAsync(JavaScriptStoragePath);
                _accessorJsRef = new Lazy<IJSObjectReference>(await jsRuntime.InvokeAsync<IJSObjectReference>("import", jsPath));
            }
        }
    }
}