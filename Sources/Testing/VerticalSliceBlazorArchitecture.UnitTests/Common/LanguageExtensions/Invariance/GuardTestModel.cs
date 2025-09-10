namespace VerticalSliceBlazorArchitecture.UnitTests.Common.LanguageExtensions.Invariance
{
    public class GuardTestModel(string? testString, object testObject, IEnumerable<object> testCollection, int testInt = 0)
    {
        public IEnumerable<object> TestCollection { get; } = testCollection;
        public int TestInt { get; } = testInt;
        public object TestObject { get; } = testObject;
        public string? TestString { get; } = testString;
    }
}