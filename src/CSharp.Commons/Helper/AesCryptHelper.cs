using System.Security.Cryptography;

namespace CSharp.Commons.Helper
{

    public class AesCryptHelper
    {
        public static byte[] CreateKey()
        {
            return Aes.Create().Key;
        }
        public static byte[] CreateIv()
        {
            return Aes.Create().IV;
        }

        public static string Encrypt(string plainInput, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainInput)) throw new ArgumentNullException("Input");
            if (key?.Length <= 0) throw new ArgumentNullException("Key");
            if (iv?.Length <= 0) throw new ArgumentNullException("IV");

            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream streamEncrypt = new();
                using CryptoStream streamCrypto = new(streamEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter writer = new(streamCrypto))
                {
                    writer.Write(plainInput);
                }
                encrypted = streamEncrypt.ToArray();
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherInput, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(cipherInput)) throw new ArgumentNullException("cipherInput");
            if (key?.Length <= 0) throw new ArgumentNullException("Key");
            if (iv?.Length <= 0) throw new ArgumentNullException("IV");

            var cipherByte = Convert.FromBase64String(cipherInput);
            string result = string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using MemoryStream streamDecrypt = new(cipherByte);
                using CryptoStream streamDecrypto = new(streamDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader reader = new(streamDecrypto);
                result = reader.ReadToEnd();
            }

            return result;
        }

    }
}
