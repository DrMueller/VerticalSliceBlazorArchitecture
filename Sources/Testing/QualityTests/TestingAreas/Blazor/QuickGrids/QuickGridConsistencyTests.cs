using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Blazor.QuickGrids
{
    public class QuickGridConsistencyTests
    {
        [Fact]
        public void Components_WithQuickGrid_EndWithGrid()
        {
            var componentsWithWithQuickgrid = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.QuickGrids.Any())
                .ToList();

            var failingComponents = componentsWithWithQuickgrid
                .Where(f => !f.Name.EndsWith("Grid"))
                .ToList();

            var failingStr = string.Join(',', failingComponents.Distinct());
            failingStr.Should().BeEmpty();
        }

        [Fact]
        public void PropertyColumns_WithColumnOptions_ContainCssClass()
        {
            var components = BlazorComponentFactory.CreateAll();
            var failingComponents = new List<string>();

            foreach (var comp in components)
            {
                foreach (var grid in comp.QuickGrids)
                {
                    foreach (var column in grid.Grid.PropertyColumns)
                    {
                        if (column.ColumnOptions != null && string.IsNullOrEmpty(column.CssClass))
                        {
                            failingComponents.Add(comp.Name);
                        }
                    }
                }
            }

            var failingStr = string.Join(',', failingComponents.Distinct());
            failingStr.Should().BeEmpty();
        }

        [Fact]
        public void QuickGrids_ContainPageSizeChooser()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Select(f => (f.Name, f.QuickGrids))
                .Where(f => f.QuickGrids.Any(g => g.PageSizeChooser == null))
                .Select(f => f.Name)
                .ToList();

            var failingStr = string.Join(',', failingComponents.Distinct());
            failingStr.Should().BeEmpty();
        }

        [Fact]
        public void QuickGrids_ContainPaginator()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Select(f => (f.Name, f.QuickGrids))
                .Where(f => f.QuickGrids.Any(g => g.Paginator == null))
                .Select(f => f.Name)
                .ToList();

            var failingStr = string.Join(',', failingComponents.Distinct());
            failingStr.Should().BeEmpty();
        }

        [Fact]
        public void QuickGrids_HaveTheCorrectClasses()
        {
            const string ExpectedClasses = "table table-striped table-hover table-bordered has-row-click";
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Select(f => (f.Name, f.QuickGrids))
                .Where(f => f.QuickGrids.Any(g => g.Grid.CssClass != ExpectedClasses))
                .Select(f => f.Name)
                .ToList();

            var failingStr = string.Join(',', failingComponents.Distinct());
            failingStr.Should().BeEmpty();
        }
    }
}