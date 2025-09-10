using FluentAssertions;
using Microsoft.JSInterop;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Blazor.Components
{
    public class ComponentDisposeTests
    {
        [Fact]
        public void Components_UsingDotnetObjectReference_AreDisposable()
        {
            var componentsWithDotnetRef = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.UsesGenericType(typeof(DotNetObjectReference<>)))
                .ToList();

            var failingComponents = componentsWithDotnetRef.Where(f => !f.IsDisposable);
            failingComponents.Should().BeEmpty();
        }

        [Fact]
        public void Components_UsingJsObjectReference_AreDisposable()
        {
            var componentsWithObjRef = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.UsesType(typeof(IJSObjectReference)))
                .ToList();

            var failingComponents = componentsWithObjRef.Where(f => !f.IsDisposable);
            failingComponents.Should().BeEmpty();
        }
    }
}