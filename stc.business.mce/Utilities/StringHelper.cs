using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace stc.business.mce.Utilities
{
    public static class StringHelper
    {
        public static string GetRandomNumber4Digits()
        {
            var random = new Random();
            var randomNumber = random.Next(1000, 9999).ToString();

            return randomNumber;
        }

        public static string GetRandomNumber5Digits()
        {
            var random = new Random();
            var randomNumber = random.Next(10000, 99999).ToString();

            return randomNumber;
        }

        public static string Md5Hash(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var hash = new StringBuilder();
            var md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        public static string HashHMAC(this string input, string secretKey)
            {
                var encoding = Encoding.UTF8;
                var key = encoding.GetBytes(secretKey);
                var message = encoding.GetBytes(input);
                var hash = new HMACSHA256(key);

                var hashResult = BitConverter.ToString(hash.ComputeHash(message)).ToUpper();
                hashResult = hashResult.Replace("-", "");

                return hashResult;
            }

        public static string CheckSum(this object input, string pattern, List<string> exceptFields = null, string additionalInfo = "")
        {
            var result = string.Empty;
            foreach (var prop in input.GetType().GetProperties())
            {
                if (exceptFields != null && exceptFields.Any())
                {
                    if (!exceptFields.Contains(prop.Name))
                    {
                        result += $"{prop.Name}={prop.GetValue(input)}{pattern}";
                    }
                }
                else
                {
                    result += $"{prop.Name}={prop.GetValue(input)}{pattern}";
                }
            }

            if (result.EndsWith(pattern))
            {
                result.Remove(result.Length - pattern.Length, pattern.Length);
            }

            result += additionalInfo;

            return result;
        }

        public static string ECAppCheckSum(this object input, string functionName, List<string> exceptFields = null, string additionalInfo = "")
        {
            var result = functionName;
            foreach (var prop in input.GetType().GetProperties())
            {
                if (exceptFields != null && exceptFields.Any())
                {
                    if (!exceptFields.Contains(prop.Name))
                    {
                        result += $"{prop.GetValue(input)}";
                    }
                }
                else
                {
                    result += $"{prop.GetValue(input)}";
                }
            }

            result += additionalInfo;

            return result;
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < Constants.VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < Constants.VietnameseSigns[i].Length; j++)
                    str = str.Replace(Constants.VietnameseSigns[i][j], Constants.VietnameseSigns[0][i - 1]);
            }

            return str;
        }

        public static bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone) || (!string.IsNullOrEmpty(phone) && phone.Trim().Length < 10))
            {
                return false;
            }

            var regex = new Regex(@"^(01[2689]|07|08|03|05|09)[0-9]{8}$");

            if (!regex.IsMatch(phone))
            {
                return false;
            }

            return true;
        }

        public static string GenerateFileName(this string fileName)
        {
            string fileNameSaved = DateTime.Now.ToString("yyyyMMdd") + fileName + ".xlsx";
            return fileNameSaved;
        }
    }
}
