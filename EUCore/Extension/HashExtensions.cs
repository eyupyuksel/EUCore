using System;
using System.Security.Cryptography;
using System.Text;

namespace EUCore.Extension
{
    public static class HashExtensions
    {
        public static string ToSha256(this string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public static byte[] Sha256(this byte[] input)
        {
            if (input == null)
                return default(byte[]);
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input);
            }
        }
        public static string Sha512(this string input)
        {
            using (var sha = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
