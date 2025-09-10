namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms
{
    public class BlazorFormLabel(string? forId, string? className)
    {
        public string? ClassName { get; } = className;
        public string? ForId { get; } = forId;
    }
}