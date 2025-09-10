using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.Presentation.Shared.Infos
{
    public partial class InfoList
    {
        [Parameter]
        public IReadOnlyCollection<string> InfoEntries { get; set; } = null!;
    }
}