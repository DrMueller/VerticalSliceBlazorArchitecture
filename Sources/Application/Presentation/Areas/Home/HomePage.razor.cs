using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Application.Features.LoadSomeData;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;

namespace VerticalSliceBlazorArchitecture.Presentation.Areas.Home
{
    public partial class HomePage
    {
        public const string Path = "/home";
        private const string Path2 = "/";

        [Inject]
        public required IMediationService Mediator { get; set; }

        private string DataText { get; set; } = string.Empty;

        private async Task LoadDataAsync()
        {
            DataText = await Mediator.SendAsync(new LoadSomeDataQuery());
        }
    }
}