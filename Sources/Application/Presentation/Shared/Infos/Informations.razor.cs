using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;

namespace VerticalSliceBlazorArchitecture.Presentation.Shared.Infos
{
    public partial class Informations
    {
        [Parameter]
        public string? DataTestId { get; set; }

        [Parameter]
        public InformationEntries? Entries { get; set; }

        private bool HasErrors => Entries?.HasErrors ?? false;

        private bool HasInfos => Entries?.InfoMessages.Any() ?? false;

        private bool HasWarnings => Entries?.WarningMessages.Any() ?? false;
    }
}