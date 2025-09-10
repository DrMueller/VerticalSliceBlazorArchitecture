using VerticalSliceBlazorArchitecture.Common.Logging.Services.Models;

namespace VerticalSliceBlazorArchitecture.Common.Logging.Services
{
    public interface ILogInfoProvider
    {
        LogInfo ProvideLogInfo();
    }
}