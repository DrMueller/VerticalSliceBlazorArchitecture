using System.Reflection;
using Microsoft.AspNetCore.Components;
using Moq;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services.Servants
{
    public class MockServiceProvider : IServiceProvider
    {
        private static readonly MethodInfo _makeGenericMockMethod = typeof(MockServiceProvider).GetMethod(nameof(CreateGenericMockInternal))!;

        public T CreateGenericMockInternal<T>()
            where T : class
        {
            var mock = new Mock<T>
            {
                DefaultValue = DefaultValue.Mock
            };

            return mock.Object;
        }

        public object? GetService(Type serviceType)
        {
            if (serviceType == typeof(IComponentActivator))
            {
                // Bunit allows to inject custom IComponentActivator and creates it itself, if it is null
                return null;
            }

            var preparedMock = PrepareMock(serviceType);

            var mock = preparedMock
                .Reduce(() =>
                {
                    var genericMethod = _makeGenericMockMethod.MakeGenericMethod(serviceType);
                    var genericMock = genericMethod.Invoke(this, []);

                    return genericMock;
                });

            return mock;
        }

        private static Maybe<object?> PrepareMock(Type serviceType)
        {
            if (serviceType == typeof(INavigator))
            {
                var mock = new Mock<INavigator>();
                mock.Setup(f => f.BaseUri).Returns(string.Empty);
                mock.Setup(f => f.Uri).Returns(string.Empty);

                return new Some<object?>(mock.Object);
            }

            return None.Value;
        }
    }
}