using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VerticalSliceBlazorArchitecture.Presentation.Shared.Attributes
{
    public class SanitizeAttribute : ValidationAttribute
    {
        private static readonly Regex _dangerousChars = new(
            @"[<>\""\{\}\[\];]|&(?!amp;)(lt|gt|quot|#\d+|#x[0-9a-f]+|);|%3[cbe]|%7b|%7d|%5b|%5d|javascript\s*:[^\s]*\(|on\w+\s*=|\\x[0-9a-fA-F]{2}|\\u[0-9a-fA-F]{4}",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string input)
            {
                if (_dangerousChars.IsMatch(input))
                {
                    var memberNames = string.IsNullOrEmpty(validationContext.MemberName) ? null : new List<string> { validationContext.MemberName };

                    return new ValidationResult(
                        $"{validationContext.DisplayName}: Invalid characters detected",
                        memberNames);
                }
            }

            return ValidationResult.Success;
        }
    }
}