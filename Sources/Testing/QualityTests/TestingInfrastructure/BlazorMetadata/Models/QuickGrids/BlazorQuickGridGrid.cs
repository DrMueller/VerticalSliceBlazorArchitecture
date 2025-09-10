namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.QuickGrids
{
    public class BlazorQuickGridGrid(
        string? cssClass,
        IReadOnlyCollection<BlazorQuickGridColumn> columns)
    {
        public string? CssClass { get; } = cssClass;
        public IReadOnlyCollection<BlazorQuickGridPropertyColumn> PropertyColumns => columns.OfType<BlazorQuickGridPropertyColumn>().ToList();
    }
}