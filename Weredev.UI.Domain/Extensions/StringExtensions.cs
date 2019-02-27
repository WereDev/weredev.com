namespace Weredev.UI.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CreateKey(this string value) {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return value.Trim().ToLower().Replace(' ', '_');
        }
    }
}