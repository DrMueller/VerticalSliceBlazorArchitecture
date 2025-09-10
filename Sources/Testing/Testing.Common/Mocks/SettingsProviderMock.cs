using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Mocks
{
    public class SettingsProviderMock : ISettingsProvider
    {
        public AppSettings AppSettings => new()
        {
            ConnectionString = "Server=someserver.database.windows.net;Database=server-321;Trusted_Connection=false;User ID=test;Password=123;",
        };
    }
}