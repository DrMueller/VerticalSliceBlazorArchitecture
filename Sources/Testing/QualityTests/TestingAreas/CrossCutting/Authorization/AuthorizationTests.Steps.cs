using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.Authorization
{
    public partial class AuthorizationTests
    {
        private const string AuthServiceSuffix = "AuthorizationService";

        private static Maybe<string> AssertAuthServiceInjected(Type commandType)
        {
            var uowFactoryType = typeof(IUnitOfWorkFactory);

            var ctor = commandType.GetConstructors().Single();
            var containsUowFactoy = ctor.GetParameters().Any(f => f.ParameterType == uowFactoryType);

            if (!containsUowFactoy)
            {
                return None.Value;
            }

            var authService = ctor.GetParameters().SingleOrDefault(f => f.ParameterType.Name.EndsWith(AuthServiceSuffix));

            if (authService == null)
            {
                return $"AuthService not found for {commandType.Name}.";
            }

            var namespaceParts = commandType.Namespace!.Split(".");
            var authServiceAreaName = authService.ParameterType.Name.Replace(AuthServiceSuffix, string.Empty);
            authServiceAreaName = authServiceAreaName.Substring(1); // Cut the leading I

            if (!namespaceParts.Contains(authServiceAreaName))
            {
                return $"Wrong AuthService injected. Found {authServiceAreaName} for {commandType.Name}.";
            }

            return None.Value;
        }
    }
}