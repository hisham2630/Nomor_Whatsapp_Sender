using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nomor_Whatsapp_Sender.Helpers
{
    public static class AESHelper
    {
        /// <summary>
        /// Encrypts plainText using AES-256-CBC with an OpenSSL-compatible
        /// "Salted__" + 8-byte random salt + ciphertext structure,
        /// then Base64-encodes the final byte array.
        /// </summary>
        public static string Encrypt(string plainText, string passphrase)
        {
            byte[] salt = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            (byte[] key, byte[] iv) = EvpBytesToKey(passphrase, salt);

            byte[] cipherBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    }
                    cipherBytes = ms.ToArray();
                }
            }

            byte[] finalBytes = new byte[8 + 8 + cipherBytes.Length];
            Array.Copy(Encoding.ASCII.GetBytes("Salted__"), 0, finalBytes, 0, 8);
            Array.Copy(salt, 0, finalBytes, 8, 8);
            Array.Copy(cipherBytes, 0, finalBytes, 16, cipherBytes.Length);

            return Convert.ToBase64String(finalBytes);
        }

        /// <summary>
        /// Decrypts data that was encrypted with the above Encrypt method (OpenSSL-compatible).
        /// </summary>
        public static string Decrypt(string base64Cipher, string passphrase)
        {
            byte[] cipherData = Convert.FromBase64String(base64Cipher);

            var signature = new byte[8];
            Array.Copy(cipherData, 0, signature, 0, 8);
            string signatureStr = Encoding.ASCII.GetString(signature);
            if (signatureStr != "Salted__")
                throw new ArgumentException("Invalid OpenSSL salted data");

            byte[] salt = new byte[8];
            Array.Copy(cipherData, 8, salt, 0, 8);

            (byte[] key, byte[] iv) = EvpBytesToKey(passphrase, salt);

            byte[] actualCipher = new byte[cipherData.Length - 16];
            Array.Copy(cipherData, 16, actualCipher, 0, actualCipher.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(actualCipher, 0, actualCipher.Length);
                    }
                    byte[] plainBytes = ms.ToArray();
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }

        /// <summary>
        /// Replicates EVP_BytesToKey (MD5-based) for AES-256-CBC:
        /// 32-byte key + 16-byte IV = 48 bytes total.
        /// </summary>
        private static (byte[] Key, byte[] IV) EvpBytesToKey(string passphrase, byte[] salt)
        {
            List<byte> hashes = new List<byte>();
            byte[] passBytes = Encoding.UTF8.GetBytes(passphrase);
            byte[] currentHash = Array.Empty<byte>();

            while (hashes.Count < 48)
            {
                using (MD5 md5 = MD5.Create())
                {
                    int currentLen = currentHash.Length;
                    byte[] combined = new byte[currentLen + passBytes.Length + salt.Length];
                    Buffer.BlockCopy(currentHash, 0, combined, 0, currentLen);
                    Buffer.BlockCopy(passBytes, 0, combined, currentLen, passBytes.Length);
                    Buffer.BlockCopy(salt, 0, combined, currentLen + passBytes.Length, salt.Length);

                    currentHash = md5.ComputeHash(combined);
                }
                hashes.AddRange(currentHash);
            }

            byte[] fullKey = hashes.ToArray();
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            Array.Copy(fullKey, 0, key, 0, 32);
            Array.Copy(fullKey, 32, iv, 0, 16);

            return (key, iv);
        }
    }
}
