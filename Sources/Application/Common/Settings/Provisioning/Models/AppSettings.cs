using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models
{
    [PublicAPI]
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";
        public string AppVersion { get; set; } = null!;
        public string EnvironmentName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
    }
}