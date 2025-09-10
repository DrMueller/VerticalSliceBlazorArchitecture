namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms
{
    public class BlazorFormInput(string tagName, string? id, string? bindingTarget)
    {
        public string BindingProperty
        {
            get
            {
                if (string.IsNullOrEmpty(bindingTarget))
                {
                    return string.Empty;
                }

                return bindingTarget.Replace("@", string.Empty);
            }
        }

        public string? Id { get; } = id;
        public string TagName { get; } = tagName;
    }
}