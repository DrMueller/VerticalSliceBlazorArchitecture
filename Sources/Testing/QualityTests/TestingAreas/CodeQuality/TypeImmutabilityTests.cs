using FluentAssertions;
using NetArchTest.Rules;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Asserts;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CodeQuality
{
    public class TypeImmutabilityTests
    {
        [Fact]
        public void Commands_Are_Immutable()
        {
            var commandTypes = Types.InAssembly(typeof(IMediationService).Assembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .GetTypes()
                .ToList();

            TypeAsserter.AssertAreImmutable(commandTypes);
        }

        [Fact]
        public void Queries_Are_Immutable()
        {
            var queryTypes = Types.InAssembly(typeof(IMediationService).Assembly)
                .That()
                .ImplementInterface(typeof(IQuery<>))
                .GetTypes()
                .ToList();

            queryTypes.Should().NotBeNullOrEmpty();
            TypeAsserter.AssertAreImmutable(queryTypes);
        }

        [Fact]
        public void Responses_Are_Immutable()
        {
            var responseTypes = Types.InAssembly(typeof(IMediationService).Assembly)
                .That()
                .ResideInNamespaceContaining("Response")
                .GetTypes()
                .ToList();

            responseTypes.Should().NotBeNullOrEmpty();
            TypeAsserter.AssertAreImmutable(responseTypes);
        }
    }
}