using System.Reflection;
using Bunit;
using Bunit.TestDoubles;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services.Servants
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    internal static class RenderedComponentFactory
    {
        private static readonly Lazy<TestContext> _lazyContext = new(() =>
        {
            var context = new TestContext();
            context.JSInterop.Mode = JSRuntimeMode.Loose;
            context.Services.AddFallbackServiceProvider(new MockServiceProvider());
            context.AddTestAuthorization();

            return context;
        });
        private static readonly MethodInfo _createMethod = typeof(RenderedComponentFactory).GetMethod(nameof(CreateInternal))!;

        public static IRenderedComponent<IComponent> Create(Type type)
        {
            return CreateGenerically(type);
        }

        public static IRenderedComponent<T> CreateInternal<T>() where T : ComponentBase
        {
            var actualComponent = _lazyContext.Value.RenderComponent<T>();

            return actualComponent;
        }

        private static IRenderedComponent<IComponent> CreateGenerically(Type type)
        {
            var genericMethod = _createMethod.MakeGenericMethod(type);

            var comp = genericMethod.Invoke(null, []);

            return (IRenderedComponent<IComponent>)comp!;
        }
    }
}