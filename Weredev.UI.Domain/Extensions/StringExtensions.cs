using System.Text.RegularExpressions;

namespace Weredev.UI.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CreateKey(this string value) {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            var key = Regex.Replace(value.Trim(), "[^a-zA-Z0-9]", "_");
            key = Regex.Replace(key, "_+", "_");
            return key.ToLower();
        }
    }
}