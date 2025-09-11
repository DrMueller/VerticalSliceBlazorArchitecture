using FluentAssertions;
using MediatR;
using NetArchTest.Rules;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.Authorization
{
    public partial class AuthorizationTests(QualityTestFixture fixture) : QualityTestBase(fixture)
    {
        [Fact]
        public void Commands_WhichUseUnitOfWork_InjectAccessService()
        {
            // Arrange
            var ressourcenCommandTypes = Types.InAssembly(AssemblyProvider.Implementations.Applicatiom)
                .That().ImplementInterface(typeof(IRequestHandler<,>))
                .GetTypes()
                .ToList();

            ressourcenCommandTypes.Should().NotBeNullOrEmpty();

            // Act
            var failingTypes =
                ressourcenCommandTypes
                    .Select(AssertAuthServiceInjected)
                    .SelectSome();

            // Assert
            failingTypes.Should().BeEmpty();
        }
    }
}