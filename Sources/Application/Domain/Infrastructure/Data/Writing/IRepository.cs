using System.Diagnostics.CodeAnalysis;

namespace VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing
{
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker interface for easier generic handling")]
    public interface IRepository;
}