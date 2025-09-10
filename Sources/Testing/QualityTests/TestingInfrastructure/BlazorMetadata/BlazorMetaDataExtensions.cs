namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata
{
    internal static class BlazorMetaDataExtensions
    {
        private const string PresentationNamespacePrefix = ".Applicatiom";

        internal static string GetBlazorFileName(this Type type)
        {
            var typeName = type.Name;

            if (type.IsGenericType)
            {
                typeName = typeName.Substring(0, typeName.IndexOf('`'));
            }

            var presentationNamespace = type.Namespace!.Substring(type.Namespace!.IndexOf(PresentationNamespacePrefix, StringComparison.OrdinalIgnoreCase) + PresentationNamespacePrefix.Length);

            if (presentationNamespace.StartsWith("."))
            {
                presentationNamespace = presentationNamespace.Substring(1);
            }

            return $"{presentationNamespace}.{typeName}.razor";
        }
    }
}