using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.Common.InformationHandling
{
    public class InformationEntry
    {
        public string Message { get; }
        public InformationType Type { get; }

        public InformationEntry(InformationType type, string message)
        {
            Guard.StringNotNullOrEmpty(() => message);

            Type = type;
            Message = message;
        }
    }
}