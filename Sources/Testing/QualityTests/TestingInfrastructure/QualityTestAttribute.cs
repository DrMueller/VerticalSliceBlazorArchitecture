namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class QualityTestAttribute(string rationale) : Attribute
    {
        public string Rationale { get; } = rationale;
    }
}