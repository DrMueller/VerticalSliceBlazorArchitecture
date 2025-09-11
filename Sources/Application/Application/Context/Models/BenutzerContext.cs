using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.Application.Context.Models
{
    [PublicAPI]
    public class BenutzerContext
    {
        public string IdpId { get; init; } = string.Empty;

        public string EmailAddress { get; init; } = string.Empty;

        public string Name { get; init; } = string.Empty;
    }
}