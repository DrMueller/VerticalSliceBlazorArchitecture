using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Mocks
{
    public class NavigationManagerMock : NavigationManager
    {
        public NavigationManagerMock()
        {
            Initialize("http://localhost/", "http://localhost/");
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            Uri = ToAbsoluteUri(uri).ToString();
        }
    }
}