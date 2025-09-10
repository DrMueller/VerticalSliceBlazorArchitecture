using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Testing.Components;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services.Servants;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services
{
    public static class BlazorComponentFactory
    {
        private static readonly Lazy<IReadOnlyCollection<BlazorComponent>> _lazyComponents = new(CreateAllInternal);

        public static IReadOnlyCollection<BlazorComponent> CreateAll()
        {
            return _lazyComponents.Value;
        }

        private static IReadOnlyCollection<BlazorComponent> CreateAllInternal()
        {
            var result = new List<BlazorComponent>();

            var allTypes = FetchComponentTypes();
            var allMarkups = BlazorComponentMarkupFactory.CreateAll();

            foreach (var type in allTypes)
            {
                var markup = allMarkups.SingleOrDefault(f => f.FileNamespace == type.GetBlazorFileName());
                var forms = CreateForms(markup);

                var comp = new BlazorComponent(
                    type,
                    markup,
                    forms,
                    BlazorQuickGridFactory.CreateAll(markup));

                result.Add(comp);
            }

            return result;
        }

        private static IReadOnlyCollection<BlazorForm> CreateForms(BlazorComponentMarkup? markup)
        {
            if (markup == null)
            {
                return new List<BlazorForm>();
            }

            return BlazorFormFactory.CreateAll(markup);
        }

        private static IReadOnlyCollection<Type> FetchComponentTypes()
        {
            var testPages = new List<Type>
            {
                typeof(TestExceptionPage)
            };

            var componentType = typeof(ComponentBase);

            return AssemblyProvider.Implementations.Applicatiom
                .GetTypes()
                .Where(f => !testPages.Contains(f))
                .Where(f => f.IsAssignableTo(componentType))
                .ToList();
        }
    }
}