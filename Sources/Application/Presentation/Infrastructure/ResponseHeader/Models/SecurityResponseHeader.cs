using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ResponseHeader.Models
{
    public class SecurityResponseHeader
    {
        private static readonly List<string> _allowedImageSources =
        [
            "data:",
            "https://wms.geo.admin.ch/"
        ];

        public static IEnumerable<SecurityResponseHeader> AllHeaders => new List<SecurityResponseHeader>
        {
            ContentSecurityPolicy,
            ContentTypeOptions
        };

        public static SecurityResponseHeader ContentSecurityPolicy { get; } = new("Content-Security-Policy", $"default-src 'self'; script-src 'self' 'unsafe-inline'; img-src 'self' {string.Join(" ", _allowedImageSources)}; style-src 'self' 'unsafe-inline'; font-src 'self'; frame-ancestors 'none'; worker-src 'self' blob:");
        public static SecurityResponseHeader ContentTypeOptions { get; } = new("X-Content-type-options", "nosniff");

        public string Name { get; }
        public string Value { get; }

        private SecurityResponseHeader(string name, string value)
        {
            Guard.StringNotNullOrEmpty(() => name);
            Guard.StringNotNullOrEmpty(() => value);

            Name = name;
            Value = value;
        }
    }
}