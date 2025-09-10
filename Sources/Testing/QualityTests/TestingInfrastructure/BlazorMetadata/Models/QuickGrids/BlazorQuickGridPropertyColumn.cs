namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.QuickGrids
{
    public class BlazorQuickGridPropertyColumn(
        BlazorQuickGridColumnOptions? columnOptions,
        string? cssClass)
        : BlazorQuickGridColumn
    {
        public BlazorQuickGridColumnOptions? ColumnOptions { get; } = columnOptions;
        public string? CssClass { get; } = cssClass;
    }
}