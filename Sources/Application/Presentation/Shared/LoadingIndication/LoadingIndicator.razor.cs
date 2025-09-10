using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.Presentation.Shared.LoadingIndication
{
    public partial class LoadingIndicator
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [EditorRequired]
        public bool IsLoading { get; set; }
    }
}