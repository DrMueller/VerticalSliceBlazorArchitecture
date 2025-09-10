using System.Reflection;
using VerticalSliceBlazorArchitecture.Presentation.Shell.Layout;
using VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Consistency;
using VerticalSliceBlazorArchitecture.UnitTests.Presentation.Infrastructure.Logging.Services;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies
{
    internal static class AssemblyProvider
    {
        internal static class Implementations
        {
            internal static Assembly Applicatiom { get; } = typeof(MainLayout).Assembly;
        }

        internal static class Testing
        {
            internal static Assembly QualityTests { get; } = typeof(TypeNamingTests).Assembly;
            internal static Assembly UnitTests { get; } = typeof(LogInfoProviderUnitTests).Assembly;
        }
    }
}