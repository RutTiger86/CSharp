using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace CSharp.Authorization.OAuth.Helpers
{
    public class CryptographyHelper
    {

        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns></returns>
        public static string RandomDataBase64url(uint length)
        {
            var data = new byte[length];
            RandomNumberGenerator.Create().GetBytes(data);
            return Base64UrlEncodeNoPadding(data);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string.
        /// </summary>
        /// <param name="inputStirng"></param>
        /// <returns></returns>
        public static byte[] Sha256(string inputStirng)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
            return SHA256.HashData(bytes);
        }


        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }


        public static string GetHash(string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = Sha256(input); ;

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var stringBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return stringBuilder.ToString();
        }

        public static string GetPbkdf2(string input, string salt, int it_count, int length)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = KeyDerivation.Pbkdf2(input, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA256, it_count, length);

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var stringBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return stringBuilder.ToString();
        }


    }
}
