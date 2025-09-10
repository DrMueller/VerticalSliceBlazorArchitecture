using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services
{
    [PublicAPI]
    public interface IJavaScriptLocator
    {
        Task<string> LocateAbsoluteJsFilePathAsync(string absolutePath);

        Task<string> LocateJsFilePathAsync(ComponentBase comp);
    }
}