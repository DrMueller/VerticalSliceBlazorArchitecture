﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services.Implementation
{
    [UsedImplicitly]
    public class Navigator(NavigationManager navManager, IJSRuntime jsRuntime) : INavigator
    {
        public string BaseUri => navManager.BaseUri;
        public string Uri => navManager.Uri;

        public void NavigateTo(string target, bool forceLoad)
        {
            navManager.NavigateTo(target, forceLoad);
        }

        public async Task OpenInNewTabAsync(string target)
        {
            await jsRuntime.InvokeVoidAsync("open", target, "_blank");
        }

        public Uri ToAbsoluteUri(string? relativeUri)
        {
            return navManager.ToAbsoluteUri(relativeUri);
        }
    }
}