using FluentAssertions;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services;
using VerticalSliceBlazorArchitecture.Presentation.Shell;
using VerticalSliceBlazorArchitecture.Presentation.Shell.Benutzer;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services;
using VerticalSliceBlazorArchitecture.Testing.Common.Reflections;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Blazor.Components
{
    public class ComponentConsistencyTests
    {
        [Fact]
        public void ComponentMarkups_DoNotContainCode()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.HasMarkup)
                .Where(f => f.Markup!.CotainsText("@code"))
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void ComponentMarkups_DoNotUseInlineJavascript()
        {
            const string JavaScriptPattern = @"<script[^>]*>.*<\/script>";

            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.ComponentType != typeof(App)) // App has all the script references
                .Where(f => f.HasMarkup)
                .Where(f => f.Markup!.ContainsRegex(JavaScriptPattern))
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void ComponentMarkups_DoNotUseInlineStyles()
        {
            // Insert components, which need dynamic css, here
            var skippedComponents = new List<Type>
            {
                typeof(App)
            };

            const string StylePattern = @"style\s*=\s*""(.*?)""";

            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => !skippedComponents.Contains(f.ComponentType))
                .Where(f => f.HasMarkup)
                .Where(f => f.Markup!.ContainsRegex(StylePattern))
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Components_DoNotHaveRouteAttribute()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => !f.IsPage)
                .Where(f => f.RouteAttributes.Any())
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Components_DoNotInjectInMarkups()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.HasMarkup)
                .Where(f => f.Markup!.CotainsText("@inject"))
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Components_HaveValidEventHandlers()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.HasInvalidEventCallbacks)
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Components_InjectOnlyInterfaces()
        {
            var skippedComponents = new List<Type>
            {
                typeof(BenutzerMenu)
            };

            var failingComponents = BlazorComponentFactory.CreateAll()
                .Where(f => !skippedComponents.Contains(f.ComponentType))
                .Where(f => f.Injections.Any(g => g.PropertyType is { IsClass: true, IsInterface: false }));

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Components_WithIsolatedJavaScript_InjectLocator()
        {
            // Arrange
            var componentsWithIsolation = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.HasIsolatedJavascript)
                .ToList();

            // Act
            var componentsWithoutLocator = componentsWithIsolation
                .Where(f => f.Injections.All(g => g.PropertyType != typeof(IJavaScriptLocator)));

            // Assert
            componentsWithoutLocator.Should().BeEmpty();
        }

        [Fact]
        public void NonNullableRefParameters_HaveEditorRequiredAttribute()
        {
            var allComponents = BlazorComponentFactory.CreateAll();

            var failingPropsInfo = new List<string>();

            foreach (var component in allComponents)
            {
                var failingProps = component.AllProperties
                    .Where(f =>
                        f.HasParameterAttribute() &&
                        !f.HasEditorRequiredAttribute() &&
                        f.IsRefType()
                        && !f.IsNullable());

                failingPropsInfo.AddRange(failingProps.Select(f => $"{component.Name}.{f.Name}"));
            }

            failingPropsInfo.Should().BeEmpty();
        }

        [Fact]
        public void PagePaths_AreLowerCase()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.IsPage)
                .Where(f => f.Path != f.Path!.ToLowerInvariant())
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Pages_HavePathConstant()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.IsPage)
                .Where(f => string.IsNullOrEmpty(f.Path))
                .ToList();

            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Pages_HaveRouteAttribute()
        {
            var failingComponents = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.IsPage)
                .Where(f => !f.RouteAttributes.Any())
                .ToList();

            failingComponents.Should().BeEmpty();
        }
    }
}