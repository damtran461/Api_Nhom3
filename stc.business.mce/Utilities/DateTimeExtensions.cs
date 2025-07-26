using System;
using System.IdentityModel.Tokens.Jwt;

namespace stc.business.mce.Utilities
{
    public static class DateTimeExtensions
    {
        public static long GetTimeStamp(this DateTime obj)
        {
            return (long)(obj.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();

            return dtDateTime;
        }

        public static DateTime GetExpiryToken(this string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken);
            var tokenObject = jsonToken as JwtSecurityToken;

            return tokenObject.ValidTo;
        }

        public static DateTime ToUtcVN(this DateTime input)
        {
            return input + new TimeSpan(7, 0, 0);
        }

        public static DateTime UnixTimeStampToDateTime(this int unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dtDateTime;
        }

        public static int ToUnixTimeStamp(this DateTime dateTime)
        {
            var timestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            return timestamp;
        }

        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds = 0)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
    }
}
