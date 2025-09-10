using System.Text;
using JetBrains.Annotations;
using Lamar.Diagnostics;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.DependencyInjection
{
    [Collection(DependencyInjectionTestsCollectionFixture.CollectionName)]
    [UsedImplicitly]
    public partial class DependencyInjectionTests(DependencyInjectionTestsFixture fixture)
    {
        private static IReadOnlyCollection<Type> GetAllConstructorTypes(Type type)
        {
            return type.GetConstructors()
                .SelectMany(constructor => constructor.GetParameters().Select(parameterInfo => parameterInfo.ParameterType))
                .Where(paramType => !paramType.IsGenericType || paramType.GetGenericTypeDefinition() != typeof(Func<>)) // Funcs are resolved directly, not working with the test below
                .Select(paramType => paramType.IsGenericType ? paramType.GetGenericArguments()[0] : paramType)
                .ToList();
        }

        private static IReadOnlyCollection<Type> GetImplementationsOfLifetime(IEnumerable<InstanceRef> registrations, ServiceLifetime lifetime)
        {
            const string NamespaceMicrosoft = "Microsoft";

            return registrations
                .Where(registration => registration.Lifetime == lifetime)
                .Select(registration => registration.ImplementationType)
                .Where(f => !f.FullName!.StartsWith(NamespaceMicrosoft))
                .Distinct()
                .ToList();
        }

        private static IReadOnlyCollection<Type> GetInterfacesOfLifetime(IEnumerable<InstanceRef> registrations, params ServiceLifetime[] lifetimes)
        {
            return registrations
                .Where(registration => lifetimes.Contains(registration.Lifetime))
                .Select(registration => registration.ServiceType)
                .Distinct()
                .ToList();
        }

        private void AssertInjectionScoping(
            ServiceLifetime lifeTimeToTest,
            params ServiceLifetime[] notAllowedLifeTimes)
        {
            var registrations = fixture.Registrations.Value;
            var typesToCheck = GetImplementationsOfLifetime(registrations, lifeTimeToTest).ToList();
            var forbiddenInterfaces = GetInterfacesOfLifetime(registrations, notAllowedLifeTimes).ToList();

            var sb = new StringBuilder();

            foreach (var typeToCheck in typesToCheck)
            {
                var ctorTypes = GetAllConstructorTypes(typeToCheck);
                var typesUsed = ctorTypes.Intersect(forbiddenInterfaces).ToList();

                foreach (var type in typesUsed)
                {
                    sb.AppendLine($"{typeToCheck.FullName} -> {type.FullName}");
                }
            }

            var str = sb.ToString();

            if (!string.IsNullOrEmpty(str))
            {
                Assert.Fail(str);
            }
        }
    }
}