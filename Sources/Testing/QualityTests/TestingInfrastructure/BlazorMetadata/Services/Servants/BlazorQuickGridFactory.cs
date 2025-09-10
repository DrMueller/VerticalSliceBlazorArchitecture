using AngleSharp.Dom;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.QuickGrids;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services.Servants
{
    internal static class BlazorQuickGridFactory
    {
        public static IReadOnlyCollection<BlazorQuickGridContainer> CreateAll(BlazorComponentMarkup? markup)
        {
            if (markup == null)
            {
                return [];
            }

            using var parsed = markup.Parse();

            return parsed
                .GetElementsByClassName("quickgrid-main")
                .Select(CreateQuickGrid)
                .ToList();
        }

        private static IReadOnlyCollection<BlazorQuickGridColumn> CreateAllColumns(IElement gridElement)
        {
            var propertyColumnElements = gridElement.GetElementsByTagName("PropertyColumn");

            var propertyColumns = new List<BlazorQuickGridPropertyColumn>();

            foreach (var element in propertyColumnElements)
            {
                var optionsElements = element.GetElementsByTagName("ColumnOptions").SingleOrDefault();
                BlazorQuickGridColumnOptions? options = null;

                if (optionsElements != null)
                {
                    options = new BlazorQuickGridColumnOptions();
                }

                var propCssClass = element.GetAttribute("Class");
                var propertyColumn = new BlazorQuickGridPropertyColumn(options, propCssClass);
                propertyColumns.Add(propertyColumn);
            }

            var templateColumn = gridElement
                .GetElementsByTagName("TemplateColumn")
                .Select(_ => new BlazorQuickGridTemplateColumn())
                .ToList();

            var allColumns = propertyColumns.Concat<BlazorQuickGridColumn>(templateColumn).ToList();

            return allColumns;
        }

        private static BlazorQuickGridGrid CreateGrid(IElement gridElement)
        {
            var allColumns = CreateAllColumns(gridElement);
            var gridCssClass = gridElement.GetAttribute("Class");

            return new BlazorQuickGridGrid(gridCssClass, allColumns);
        }

        private static BlazorQuickGridContainer CreateQuickGrid(IElement quickGridElement)
        {
            var gridElement = quickGridElement.GetElementsByTagName("quickgrid").Single();
            var grid = CreateGrid(gridElement);
            var pageSizeChooser = quickGridElement.GetElementsByTagName("pagesizechooser").SingleOrDefault();
            var paginator = quickGridElement.GetElementsByTagName("apppaginator").SingleOrDefault();

            return new BlazorQuickGridContainer(grid, pageSizeChooser, paginator);
        }
    }
}