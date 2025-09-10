using FluentAssertions;
using MediatR;
using NetArchTest.Rules;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Consistency
{
    public class TypeNamingTests
    {
        [Fact]
        public void BusinessObjects_EndWithBo()
        {
            var result = Types.InAssembly(AssemblyProvider.Implementations.Applicatiom)
                .That().ResideInNamespaceContaining("BusinessObjects")
                .Should()
                .HaveNameEndingWith("Bo")
                .GetResult();

            result.FailingTypes.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Commands_EndWithCommand()
        {
            var result = Types.InAssembly(AssemblyProvider.Implementations.Applicatiom)
                .That().ImplementInterface(typeof(ICommand))
                .Or().ImplementInterface(typeof(ICommand<>))
                .Should()
                .HaveNameEndingWith("Command")
                .GetResult();

            result.FailingTypes.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Handlers_EndWithQueryOrCommandHandler()
        {
            var result = Types.InAssembly(AssemblyProvider.Implementations.Applicatiom)
                .That().ImplementInterface(typeof(IRequestHandler<>))
                .Should().HaveNameEndingWith("QueryHandler")
                .Or().HaveNameEndingWith("CommandHandler")
                .GetResult();

            result.FailingTypes.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Queries_EndWithQuery()
        {
            var queryTypes = Types.InAssembly(AssemblyProvider.Implementations.Applicatiom)
                .That().ImplementInterface(typeof(IQuery<>))
                .GetTypes()
                .ToList();

            var nongenericTypeNames = queryTypes.Where(f => !f.IsGenericType)
                .Select(f => f.Name)
                .ToList();

            var genericTypeNames = queryTypes.Where(f => f.IsGenericType)
                .Select(f => f.Name.Split('`')[0])
                .ToList();

            var allTypeNames = nongenericTypeNames.Concat(genericTypeNames).ToList();

            allTypeNames.Should().OnlyContain(f => f.EndsWith("Query"));
        }

        [Fact]
        public void Repositories_EndWithRepository()
        {
            var result = Types.InAssembly(AssemblyProvider.Implementations.Applicatiom)
                .That().ResideInNamespaceContaining("Repositories")
                .And().DoNotResideInNamespaceContaining("Servants")
                .And().AreNotAbstract()
                .Should().HaveNameEndingWith("Repository")
                .GetResult();

            result.FailingTypes.Should().BeNullOrEmpty();
        }
    }
}