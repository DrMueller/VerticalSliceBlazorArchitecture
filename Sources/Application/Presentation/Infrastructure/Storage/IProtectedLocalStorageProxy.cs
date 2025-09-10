using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage
{
    public interface IProtectedLocalStorageProxy
    {
        ValueTask DeleteAsync(string key);
        ValueTask<Maybe<TValue>> GetAsync<TValue>(string key);
        ValueTask SetAsync(string key, object value);
    }
}