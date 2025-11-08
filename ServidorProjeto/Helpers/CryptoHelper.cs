using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ServidorProjeto.Helpers
{
    public static class CryptoHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Key; // precisa ter 16, 24 ou 32 bytes
            aes.IV = IV;   // precisa ter 16 bytes

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cipherText)
        {
            // Se o valor não for Base64, retorne ele puro
            if (!IsBase64(cipherText))
                return cipherText;

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            using var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(cipherText);
            var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private static bool IsBase64(string value)
        {
            value = value.Trim();

            return (value.Length % 4 == 0) &&
                   Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,2}$");
        }

    }
}
