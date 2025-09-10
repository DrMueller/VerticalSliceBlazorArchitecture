using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.QuickGrids;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components
{
    public class BlazorComponent
    {
        public IReadOnlyCollection<PropertyInfo> AllProperties =>
            ComponentType.GetProperties()
                .ToList();

        public Type ComponentType { get; }

        public IReadOnlyCollection<BlazorForm> Forms { get; }

        // We say two types of Eventcallbacks are valid
        // - Eventcallbacks starting with "On"
        // - Eventcallbacks used in twoway binding (as they can't start with on)
        public bool HasInvalidEventCallbacks
        {
            get
            {
                var eventsWithoutOn = EventCallbacks
                    .Where(f => !f.Name.StartsWith("On"));

                var twoWayBindingProps = TwoWayBindings
                    .Select(f => f.EventCallbackProperty);

                var hasFailingProps = eventsWithoutOn.Except(twoWayBindingProps).Any();

                return hasFailingProps;
            }
        }

        public bool HasIsolatedJavascript
        {
            get
            {
                // There doesn't seem a way to check if a component has isolated javascript
                // as a proximate value, we check if a reference is there
                var hasRuntimeDep = UsesType(typeof(IJSObjectReference));

                return hasRuntimeDep;
            }
        }

        public bool HasMarkup => Markup != null;

        public IReadOnlyCollection<PropertyInfo> Injections { get; private set; }

        public bool IsDisposable => ComponentType.IsAssignableTo(typeof(IAsyncDisposable));

        public bool IsPage => ComponentType.Name.EndsWith("Page");
        public BlazorComponentMarkup? Markup { get; }

        public string Name => ComponentType.Name;

        public string? Path
        {
            get
            {
                var pathField = ComponentType.GetField("Path", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                return pathField?.GetValue(null)?.ToString();
            }
        }

        public IReadOnlyCollection<BlazorQuickGridContainer> QuickGrids { get; }

        public IReadOnlyCollection<RouteAttribute> RouteAttributes => ComponentType.GetCustomAttributes<RouteAttribute>().ToList();

        private IReadOnlyCollection<PropertyInfo> EventCallbacks
        {
            get
            {
                return ComponentType.GetProperties()
                    .Where(g => g.PropertyType.IsGenericType && g.PropertyType.GetGenericTypeDefinition() == typeof(EventCallback<>))
                    .ToList();
            }
        }

        private IReadOnlyCollection<TwoWayBinding> TwoWayBindings
        {
            get
            {
                var changedEventCallbacks = EventCallbacks.Where(f => f.Name.EndsWith("Changed"));

                var bindings = new List<TwoWayBinding>();

                foreach (var cb in changedEventCallbacks)
                {
                    var valuePropertyName = cb.Name.Replace("Changed", string.Empty);
                    var prop = AllProperties.SingleOrDefault(f => f.Name == valuePropertyName);

                    if (prop != null)
                    {
                        bindings.Add(new TwoWayBinding(cb, prop));
                    }
                }

                return bindings;
            }
        }

        public BlazorComponent(
            Type componentType,
            BlazorComponentMarkup? markup,
            IReadOnlyCollection<BlazorForm> forms,
            IReadOnlyCollection<BlazorQuickGridContainer> quickGrids)
        {
            Guard.ObjectNotNull(() => componentType);
            Guard.ObjectNotNull(() => forms);
            Guard.ObjectNotNull(() => quickGrids);

            ComponentType = componentType;
            Markup = markup;
            Forms = forms;
            QuickGrids = quickGrids;

            var injecAttrType = typeof(InjectAttribute);
            Injections = ComponentType
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == injecAttrType))
                .ToList();
        }

        public override string ToString()
        {
            return Name;
        }

        public bool UsesGenericType(Type type)
        {
            return GetFieldAndPropTypes()
                .Any(f => f.IsGenericType && f.GetGenericTypeDefinition() == type);
        }

        public bool UsesType(Type type)
        {
            return GetFieldAndPropTypes().Any(f => f == type);
        }

        private IReadOnlyCollection<Type> GetFieldAndPropTypes()
        {
            var propTypes = ComponentType
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Select(f => f.PropertyType);

            var fieldTypes = ComponentType
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Select(f => f.FieldType);

            return propTypes.Union(fieldTypes).ToList();
        }
    }
}