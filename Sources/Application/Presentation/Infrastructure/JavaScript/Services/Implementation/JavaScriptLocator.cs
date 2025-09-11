using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services.Implementation
{
    [UsedImplicitly]
    internal class JavaScriptLocator(ICachingController cachingController) : IJavaScriptLocator
    {
        public async Task<string> LocateAbsoluteJsFilePathAsync(string absolutePath)
        {
            return await AppendCacheSuffixAsync(absolutePath);
        }

        public async Task<string> LocateJsFilePathAsync(ComponentBase comp)
        {
            var type = comp.GetType();

            var assemblyFullName = type.Assembly.FullName;
            var assemblyName = type.Assembly.FullName!.Substring(0, assemblyFullName!.IndexOf(','));
            var relativeNamespace = type.FullName!.Replace(assemblyName, string.Empty);

            if (type.IsGenericType)
            {
                relativeNamespace = relativeNamespace.Substring(0, relativeNamespace.IndexOf('`'));
            }

            var path = relativeNamespace.Replace(".", "/");
            path += ".razor.js";
            path = await AppendCacheSuffixAsync(path);

            return path;
        }

        private async Task<string> AppendCacheSuffixAsync(string path)
        {
            var cacheSuffix = await cachingController.LoadCachingSuffixAsync();

            return path + cacheSuffix;
        }
    }
}