using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services
{
    [PublicAPI]
    public interface INavigator
    {
        string BaseUri { get; }

        string Uri { get; }

        void NavigateTo(string target, bool forceLoad = false);

        Task OpenInNewTabAsync(string target);

        Uri ToAbsoluteUri(string? relativeUri);
    }
}