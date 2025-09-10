using System.Text;
using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.Common.Extensions
{
    [PublicAPI]
    public static class StringExtensions
    {
        public static string AppendWithSeparatorIfNotNullOrWhitespace(this string input, string separator, string? appendText)
        {
            if (string.IsNullOrWhiteSpace(appendText))
            {
                return input;
            }

            var sb = string.IsNullOrEmpty(input) 
                ? new StringBuilder() 
                : new StringBuilder(input.Trim());
            
            if (!string.IsNullOrWhiteSpace(sb.ToString()))
            {
                sb.Append(separator);
            }

            sb.Append(appendText);

            return sb.ToString();
        }

        public static string JoinNonEmpty(this string str, string? separator, params string?[] value)
        {
            separator ??= ", ";

            var stringBuilder = new StringBuilder();
            var first = true;

            if (!string.IsNullOrWhiteSpace(str))
            {
                stringBuilder.Append(str);
                first = false;
            }

            foreach (var item in value)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (!first)
                    {
                        stringBuilder.Append(separator);
                    }

                    stringBuilder.Append(item);
                    first = false;
                }
            }

            return stringBuilder.ToString();
        }

        public static bool StartsWithLetter(this string? input)
        {
            var firstChar = input?.FirstOrDefault();

            return firstChar != null && char.IsLetter(firstChar.Value);
        }
    }
}