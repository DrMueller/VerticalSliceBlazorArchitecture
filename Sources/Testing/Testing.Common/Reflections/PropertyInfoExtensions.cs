using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Reflections
{
    public static class PropertyInfoExtensions
    {
        public static bool HasEditorRequiredAttribute(this PropertyInfo propInfo)
        {
            return Attribute.IsDefined(propInfo, typeof(EditorRequiredAttribute));
        }

        public static bool HasParameterAttribute(this PropertyInfo propInfo)
        {
            return Attribute.IsDefined(propInfo, typeof(ParameterAttribute));
        }

        public static bool IsInitOnly(this PropertyInfo property)
        {
            if (!property.CanWrite)
            {
                return false;
            }

            var setMethod = property.SetMethod;
            var setMethodReturnParameterModifiers = setMethod!.ReturnParameter.GetRequiredCustomModifiers();

            return setMethodReturnParameterModifiers.Contains(typeof(IsExternalInit));
        }

        public static bool IsNullable(this PropertyInfo propInfo)
        {
            var nullabilityContext = new NullabilityInfoContext();
            var info = nullabilityContext.Create(propInfo);

            return info.WriteState is NullabilityState.Nullable;
        }

        public static bool IsRefType(this PropertyInfo propInfo)
        {
            return propInfo.PropertyType.IsClass && propInfo.PropertyType != typeof(string);
        }
    }
}