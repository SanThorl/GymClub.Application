using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GymClub.Shared
{
    public static partial class DevCode
    {
        public static string? ToJson<T>(this T? obj, bool format = false)
        {
            if (obj == null) return string.Empty;
            string? result;
            if (obj is string)
            {
                result = obj.ToString();
                goto Result;
            }

            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.sssZ" };
            result = format
                ? JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings)
                : JsonConvert.SerializeObject(obj, settings);
        Result:
            return result;
        }

        public static T? ToObject<T>(this string? jsonStr)
        {
            try
            {
                if (jsonStr != null)
                {
                    var test = JsonConvert.DeserializeObject<T>(jsonStr,
                        new JsonSerializerSettings { DateParseHandling = DateParseHandling.DateTimeOffset });
                    return test;
                }
            }
            catch
            {
                return (T)Convert.ChangeType(jsonStr, typeof(T))!;
            }

            return default;
        }

        public static string ToHex(byte[] bytes, bool upperCase)
        {
            var result = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                result.Append(b.ToString(upperCase ? "X2" : "x2"));
            }
            return result.ToString();
        }

        public static string EncodedBySalted(string decodeString)
        {
            decodeString = decodeString.ToLower()
                .Replace("a", "@")
                .Replace("i", "!")
                .Replace("l", "1")
                .Replace("e", "3")
                .Replace("o", "0")
                .Replace("s", "$")
                .Replace("n", "&");

            return decodeString;
        }

        public static string SHA256HexHashString(this string userName, string password)
        {
            userName = userName.Trim();
            password = password.Trim();

            string saltedCode = EncodedBySalted(userName);

            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.Default.GetBytes(saltedCode + password));
            var hashString = ToHex(hash, false);

            return hashString;
        }
    }
}
