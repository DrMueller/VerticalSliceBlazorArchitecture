using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;

namespace VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services
{
    public interface ISettingsProvider
    {
        AppSettings AppSettings { get; }
    }
}