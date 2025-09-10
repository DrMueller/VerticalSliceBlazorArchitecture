using System.Reflection;
using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components
{
    [PublicAPI]
    public class TwoWayBinding
    {
        public PropertyInfo EventCallbackProperty { get; }
        public PropertyInfo ValueProperty { get; }

        public TwoWayBinding(PropertyInfo eventCallbackProperty, PropertyInfo valueProperty)
        {
            Guard.ObjectNotNull(() => eventCallbackProperty);
            Guard.ObjectNotNull(() => valueProperty);
            EventCallbackProperty = eventCallbackProperty;
            ValueProperty = valueProperty;
        }
    }
}