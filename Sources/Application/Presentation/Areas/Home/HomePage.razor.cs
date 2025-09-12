using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Application.Features.LoadSomeData;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;

namespace VerticalSliceBlazorArchitecture.Presentation.Areas.Home
{
    public partial class HomePage
    {
        public const string Path = "/home";
        private const string Path2 = "/";

        [Inject]
        public required IMediationService Mediator { get; set; }

        private InformationEntries? Infos { get; set; }

        private string DataText { get; set; } = string.Empty;

        private async Task LoadDataAsync()
        {
            (Infos, _) = await Mediator.SendAsync(new LoadSomeDataQuery(null!)).ToTupleAsync(() => string.Empty);
            (_, DataText) = await Mediator.SendAsync(new LoadSomeDataQuery("Test1")).ToTupleAsync(() => string.Empty);
        }
    }
}