using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Application.Context.Models;

namespace VerticalSliceBlazorArchitecture.Application.Context.Services
{
    [PublicAPI]
    public interface IBenutzerContextState
    {
        Task<bool> CheckBenutzerExistsAsync();

        Task ClearAsync();

        Task<BenutzerContext> GetBenutzerAsync();

        Task SetAsync(BenutzerContext benutzerContext);
    }
}