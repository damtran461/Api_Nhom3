using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.RegularExpressions;

namespace stc.dto.mce
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public static JObject ToSnakeCase(this JObject original)
        {
            var newObj = new JObject();

            foreach (var property in original.Properties())
            {
                var newPropertyName = property.Name.ToSnakeCase();
                newObj[newPropertyName] = property.Value;
            }

            return newObj;
        }

        public static string[] ReadExcelData(this string value,  string propertyName, bool isAllowNull = false)
        {
            if (isAllowNull == true)
            {
                return value?.Split(':');
            }

            if (string.IsNullOrEmpty(value?.Trim()))
            {
                throw new DataException($"{propertyName} không được để trống");
            }

            return value.Split(':');
        }

    }
}
