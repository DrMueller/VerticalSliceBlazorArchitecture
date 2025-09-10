using JetBrains.Annotations;
using System.Globalization;
using System.Text.RegularExpressions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Models
{
    [PublicAPI]
    public class AppPath
    {
        private const string RouteVariablesRegex = @"\{(\w+)(?::(\w+))?\}";

        private string NativeValue { get; }

        private AppPath(string nativeValue)
        {
            Guard.StringNotNullOrEmpty(() => nativeValue);

            NativeValue = nativeValue;
        }

        public static AppPath Create(string nativeValue)
        {
            return new AppPath(nativeValue);
        }

        public string Format(params object[] args)
        {
            var matches = Regex.Matches(NativeValue, RouteVariablesRegex);

            if (matches.Count != args.Length)
            {
                throw new ArgumentException("Wrong amount of args.");
            }

            var result = NativeValue;

            for (var i = 0; i < matches.Count; i++)
            {
                var arg = args.ElementAt(i);
                var match = matches.ElementAt(i);

                if (arg is DateTime dt)
                {
                    var str = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    result = result.Replace(match.Value, str);
                }
                else
                {
                    result = result.Replace(match.Value, arg.ToString());
                }
            }

            return result;
        }
    }
}