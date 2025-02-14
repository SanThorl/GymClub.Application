using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
