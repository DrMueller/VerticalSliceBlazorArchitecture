using System.ComponentModel.DataAnnotations;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services
{
    public static class BlazorViewModelFactory
    {
        private static readonly Lazy<IReadOnlyCollection<Type>> _lazyViewModelTypes = new(LoadViewModelTypesInternal);

        public static IReadOnlyCollection<Type> ViewModelPropertyAttributeTypes { get; } = new List<Type>
        {
            typeof(RequiredAttribute),
            typeof(DisplayAttribute)
        };

        public static IReadOnlyCollection<Type> LoadViewModelTypes()
        {
            return _lazyViewModelTypes.Value;
        }

        private static IReadOnlyCollection<Type> LoadViewModelTypesInternal()
        {
            return AssemblyProvider.Implementations.Applicatiom
                .GetTypes()
                .Where(PropertiesContainViewModelAttributes)
                .ToList();
        }

        private static bool PropertiesContainViewModelAttributes(Type type)
        {
            return type.GetProperties()
                .Any(prop => prop.GetCustomAttributes(false)
                    .Any(attr => ViewModelPropertyAttributeTypes.Contains(attr.GetType())));
        }
    }
}