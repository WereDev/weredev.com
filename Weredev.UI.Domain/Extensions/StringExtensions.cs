using System.Linq;
using System.Text.RegularExpressions;

namespace Weredev.UI.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CreateKey(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            var key = Regex.Replace(value.Trim(), "[^a-zA-Z0-9]", "_");
            key = Regex.Replace(key, "_+", "_");
            return key.ToLower();
        }

        public static string GetCountryName(this string value)
        {
            if (value == null)
                return value;
            value = value.Trim();
            if (value == string.Empty || !value.Contains(','))
                return value;

            var nameParts = value.Split(',');
            var countryName = nameParts[nameParts.Length - 1].Trim();
            return countryName;
        }

        public static string GetCityName(this string value)
        {
            if (value == null)
                return value;
            value = value.Trim();
            if (value == string.Empty || !value.Contains(','))
                return value;

            var nameParts = value.Split(',');
            var cityName = string.Join(", ", nameParts.Take(nameParts.Length - 1));
            return cityName;
        }
    }
}
