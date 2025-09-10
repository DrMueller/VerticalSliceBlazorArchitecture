namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms
{
    public class BlazorFormInputValidationMessage(string forAttr)
    {
        public string TargetProperty
        {
            get
            {
                if (string.IsNullOrEmpty(forAttr))
                {
                    return string.Empty;
                }

                var prop = forAttr.Replace("() => ", string.Empty);

                return prop;
            }
        }
    }
}