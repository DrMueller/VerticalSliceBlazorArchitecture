using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage
{
    public interface ILocalStorageProxy : IAsyncDisposable
    {
        Task<Maybe<bool>> GetBoolAsync(string key);
        Task SetItemAsync(string key, string value);
    }
}