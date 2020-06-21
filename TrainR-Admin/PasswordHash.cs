using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TrainR_Admin
{
    public class PasswordHash
    {
        static string Key { get; set; } = "A!9HHhigWi%zDn@Fl0+@Y4YP2@Nob009X";

        
        public static string Encrypt(string text)
        {
            using var md5 = new MD5CryptoServiceProvider();
            using var tdes = new TripleDESCryptoServiceProvider
            {
                Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            using var transform = tdes.CreateEncryptor();
            byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        
        public static string Decrypt(string cipher)
        {
            using var md5 = new MD5CryptoServiceProvider();
            using var tdes = new TripleDESCryptoServiceProvider
            {
                Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            using var transform = tdes.CreateDecryptor();
            byte[] cipherBytes = Convert.FromBase64String(cipher);
            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return UTF8Encoding.UTF8.GetString(bytes);
        }

    }
}
