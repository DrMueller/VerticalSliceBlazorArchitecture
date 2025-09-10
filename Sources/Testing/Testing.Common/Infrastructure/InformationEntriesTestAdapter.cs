using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Infrastructure
{
    public static class InformationEntriesTestAdapter
    {
        public static void AssertForbidden(this InformationEntries infoEntries)
        {
            infoEntries.ErrorMessages.Single().Should().Be("Forbidden");
        }
    }
}