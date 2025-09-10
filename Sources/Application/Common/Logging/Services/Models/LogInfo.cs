using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.Common.Logging.Services.Models
{
    public class LogInfo
    {
        public const string AnonymousEmail = "Anonymous";

        public string BenutzerEmail { get; }

        public LogInfo(string benutzerEmail)
        {
            Guard.StringNotNullOrEmpty(() => benutzerEmail);

            BenutzerEmail = benutzerEmail;
        }

        public static LogInfo CreateAnonymous()
        {
            return new LogInfo(AnonymousEmail);
        }
    }
}