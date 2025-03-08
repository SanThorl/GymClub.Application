using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GymClub.Shared
{
    public static class PasswordHash
    {
        public static string ToHex(byte[] bytes, bool upperCase)
        {
            var result = new StringBuilder(bytes.Length * 2);
            foreach(byte b in bytes)
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

        public  static string SHA256HexHashString(this string userName, string password)
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
