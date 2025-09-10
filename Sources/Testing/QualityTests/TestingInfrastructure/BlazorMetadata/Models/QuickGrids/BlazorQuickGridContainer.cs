using AngleSharp.Dom;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.QuickGrids
{
    public class BlazorQuickGridContainer(
        BlazorQuickGridGrid grid,
        IElement? pageSizeChooser,
        IElement? paginator)
    {
        public BlazorQuickGridGrid Grid { get; } = grid;
        public IElement? PageSizeChooser { get; } = pageSizeChooser;
        public IElement? Paginator { get; } = paginator;
    }
}