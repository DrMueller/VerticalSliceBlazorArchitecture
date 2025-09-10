using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Testing.Components
{
    // Used for AppErrorBoundaryUnitTests
    public partial class TestThrowingComponent<T>
        where T : Exception
    {
        [Parameter]
        [EditorRequired]
        public required T ExceptionToThrow { get; set; }

        protected override void OnInitialized()
        {
            throw ExceptionToThrow;
        }
    }
}