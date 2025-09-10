using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components
{
    public class BlazorComponentMarkup
    {
        private const string NamespacePrefix = "Applicatiom";
        private readonly string _nativeContent;

        public string FileNamespace
        {
            get
            {
                var relativePath = FilePath.Substring(FilePath.IndexOf(NamespacePrefix, StringComparison.OrdinalIgnoreCase) + NamespacePrefix.Length + 1);

                relativePath = relativePath.Replace("\\", "//");
                relativePath = relativePath.Replace("//", ".");

                return relativePath;
            }
        }

        private string FilePath { get; }

        public BlazorComponentMarkup(string filePath, string nativeContent)
        {
            _nativeContent = nativeContent;
            Guard.StringNotNullOrEmpty(() => filePath);

            FilePath = filePath;
        }

        public bool ContainsRegex(string regex)
        {
            return Regex.IsMatch(_nativeContent, regex);
        }

        public bool CotainsText(string str)
        {
            return _nativeContent.Contains(str, StringComparison.OrdinalIgnoreCase);
        }

        public IHtmlDocument Parse()
        {
            var parser = new HtmlParser();

            return parser.ParseDocument(_nativeContent);
        }
    }
}