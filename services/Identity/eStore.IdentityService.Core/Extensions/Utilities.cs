using eStore.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.Extensions
{
    public static class Utilities
    {
        public static readonly string Base63Alphabet = "_0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static readonly string Base36Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
        public static RuntimeSettings RuntimeSettings { get; set; }

        public static byte[] FixArraySize(byte[] buffer, int desiredSize)
        {
            if (desiredSize > buffer.Length)
            {
                byte[] temp = new byte[desiredSize];
                Array.Copy(buffer, temp, buffer.Length);
                return temp;
            }
            else if (desiredSize < buffer.Length)
            {
                return buffer.Take(desiredSize).ToArray();
            }
            return buffer;
        }

        public static string PasswordSecurityHash(this string value)
        {
            string pswMD5 = CreateHashMD5(value).ToUpper();
            pswMD5 = ComputeSha256Hash(pswMD5).ToUpper();
            return pswMD5;
        }
        public static string CreateHashMD5(this string value)
        {
            value = "SJ837FAHFAH_X837!3999FH2AGKAHHA8347834783QAFHGHGAGKBN" + value;
            MD5 md5 = MD5.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataMd5.Length; i++)
            {
                sb.AppendFormat("{0:x2}", dataMd5[i]);
            }
            return sb.ToString();

        }
        public static string ComputeSha256Hash(this string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string CalculateKeySafeHash(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            using (var hashAlgorithm = SHA1.Create())
            {
                hashAlgorithm.Initialize();
                var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }

        public static string CalculateHash(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            using (var hashAlgorithm = SHA256.Create())
            {
                hashAlgorithm.Initialize();
                var hashBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                return GetBaseString(hashBytes, Base36Alphabet, 0);
            }
        }

        public static string CalculateBinaryHash(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            using (var hashAlgorithm = SHA256.Create())
            {
                hashAlgorithm.Initialize();
                var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }

        //public static string Encrpyt(string data)
        //{
        //    byte[] buffer = Encoding.UTF8.GetBytes(data);
        //    byte[] output = Encrpyt(buffer);
        //    return Convert.ToBase64String(output);
        //}

        public static string Encrpyt(string data)
        {
            var encryptionIV = RuntimeSettings.EncryptionIV;
            var encryptionKey = RuntimeSettings.EncryptionKey;

            byte[] keyByte = Encoding.UTF8.GetBytes(encryptionKey);
            byte[] ivByte = Encoding.UTF8.GetBytes(encryptionIV);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[] output = Encrpyt(buffer, keyByte, ivByte);
            return Convert.ToBase64String(output);
        }

        public static byte[] Encrpyt(byte[] data, byte[] key, byte[] iv)
        {
            key = FixArraySize(key, 32);
            iv = FixArraySize(iv, 16);

            using (var algorithm = Aes.Create())
            {
                algorithm.Key = key;
                algorithm.IV = iv;
                algorithm.Mode = CipherMode.ECB;
                algorithm.Padding = PaddingMode.ISO10126;
                using (var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                        }

                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public static string Decrpyt(string protectedData)
        {
            byte[] secureBuffer = Convert.FromBase64String(protectedData);

            var encryptionIV = RuntimeSettings.EncryptionIV;
            var encryptionKey = RuntimeSettings.EncryptionKey;
            byte[] ivByte = Encoding.UTF8.GetBytes(encryptionIV);
            byte[] keyByte = Encoding.UTF8.GetBytes(encryptionKey);
            byte[] buffer = Decrpyt(secureBuffer, keyByte, ivByte);
            return Encoding.UTF8.GetString(buffer);
        }

        public static byte[] Decrpyt(byte[] data, byte[] key, byte[] iv)
        {
            key = FixArraySize(key, 32);
            iv = FixArraySize(iv, 16);

            using (var algorithm = Aes.Create())
            {
                algorithm.Key = key;
                algorithm.IV = iv;
                algorithm.Mode = CipherMode.ECB;
                algorithm.Padding = PaddingMode.ISO10126;
                using (var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
                {
                    using (var encryptedStream = new MemoryStream(data))
                    {
                        using (var decryptedStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                            {
                                cryptoStream.CopyTo(decryptedStream);
                            }

                            return decryptedStream.ToArray();
                        }
                    }
                }
            }
        }
        public static string GetBaseString(byte[] data, string alphabet, int blockSize)
        {
            if (null == data) return string.Empty;

            StringBuilder buffer = new StringBuilder();

            Array.Resize(ref data, 1 + data.Length);
            BigInteger bigint = new BigInteger(data);

            do
            {
                buffer.Insert(0, alphabet[(int)(bigint % alphabet.Length)]);
                bigint = bigint / (ulong)alphabet.Length;
            } while (0 < bigint);

            while (blockSize > buffer.Length)
            {
                buffer.Insert(0, alphabet[0]);
            }

            return buffer.ToString();
        }
        // preserves order

        public static string GetBaseString(ulong number, string alphabet, int blockSize)
        {
            StringBuilder buffer = new StringBuilder();

            do
            {
                buffer.Insert(0, alphabet[(int)(number % (ulong)alphabet.Length)]);
                number = number / (ulong)alphabet.Length;
            } while (0 < number);

            while (blockSize > buffer.Length)
            {
                buffer.Insert(0, alphabet[0]);
            }

            return buffer.ToString();
        }

    }
}
