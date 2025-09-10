using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Mocks
{
    [PublicAPI]
    public class LoggingServiceMock : ILoggingService
    {
        public string? LoggedError { get; private set; }
        public Exception? LoggedException { get; private set; }
        public string? LoggedWarning { get; private set; }

        public void LogError(string errorMessage)
        {
            LoggedError = errorMessage;
        }

        public void LogException(Exception ex)
        {
            LoggedException = ex;
        }

        public void LogWarning(string warningMessage)
        {
            LoggedWarning = warningMessage;
        }

        public void TrackEvent(string eventInfo)
        {
        }
    }
}