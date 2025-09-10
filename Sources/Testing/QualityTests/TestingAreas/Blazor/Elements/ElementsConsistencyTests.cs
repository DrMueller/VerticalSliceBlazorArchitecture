using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Blazor.Elements
{
    public class ElementsConsistencyTests
    {
        [Fact]
        public void AllButtons_HaveTypeDefined()
        {
            var components = BlazorComponentFactory.CreateAll()
                .Where(f => f.HasMarkup);

            var failingComponents = new List<string>();

            foreach (var component in components)
            {
                using var document = component.Markup!.Parse();
                var failingButtons = document.QuerySelectorAll("button")
                    .Where(f => f.GetAttribute("type") == null).ToList();

                if (failingButtons.Any())
                {
                    failingComponents.Add(component.Name);
                }
            }

            failingComponents.Distinct().Should().BeEmpty();
        }
    }
}