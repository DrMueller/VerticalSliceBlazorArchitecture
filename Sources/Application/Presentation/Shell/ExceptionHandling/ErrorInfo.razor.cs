using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Presentation.Areas.Home;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services;

namespace VerticalSliceBlazorArchitecture.Presentation.Shell.ExceptionHandling
{
    [UsedImplicitly]
    public partial class ErrorInfo
    {
        [Parameter]
        [EditorRequired]
        public AppError? AppError { get; set; }

        [Inject]
        public required INavigator Navigator { get; set; }

        private void GoHome()
        {
            Navigator.NavigateTo(HomePage.Path, true);
        }
    }
}