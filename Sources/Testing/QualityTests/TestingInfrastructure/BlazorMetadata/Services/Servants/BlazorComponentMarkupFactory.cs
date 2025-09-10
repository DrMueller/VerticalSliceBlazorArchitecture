using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services.Servants
{
    internal static class BlazorComponentMarkupFactory
    {
        private static readonly Lazy<IReadOnlyCollection<BlazorComponentMarkup>> _lazyMarkups = new(FetchAllInternal);

        public static IReadOnlyCollection<BlazorComponentMarkup> CreateAll()
        {
            return _lazyMarkups.Value;
        }

        private static IReadOnlyCollection<BlazorComponentMarkup> FetchAllInternal()
        {
            var sourcesDir = SourcesDirectoryLocator.GetSourcesDirectory();
            var razorFiles = Directory
                .GetFiles(sourcesDir.FullName, "*.razor", SearchOption.AllDirectories)
                .ToList();

            var markups = razorFiles.Select(f => new BlazorComponentMarkup(f, File.ReadAllText(f))).ToList();

            markups.Should().NotBeEmpty("Bug");

            return markups;
        }
    }
}