using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommonCryptography
{
    /// <summary>
    /// My custom AESProvider wrapper.
    /// </summary>
    public sealed class AESCrypter
    {
        // Symmetric algorithm interface is used to store the AES service provider  
        private SymmetricAlgorithm AESProvider = new AesCryptoServiceProvider();
        private UTF32Encoding converter = new UTF32Encoding();

        private const int KEY_SIZE = 256;
        private const string DEFAULT_SALT = "aselrias38490a32safsafsfsfafassf54a68s4sa5g4as5g45sfafs32498jljf";   // Random  
        private readonly byte[] HASH_KEY = new byte[32] { 4, 54, 0, 65, 46, 9, 0, 9, 8, 6, 4, 65, 31, 65, 46, 12, 0, 56, 4, 54, 0, 65, 4, 69, 21, 25, 15, 32, 56, 0, 45, 45 };

        private readonly byte[] key1;
        private readonly byte[] key2;
        private readonly byte[] key3;

        /// <summary>
        /// Initializes a new instance of the <see cref="AESCrypt"/> class.
        /// </summary>
        /// <param name="passphrase">The string used to generate 256-byte AES key</param>
        /// <param name="IV">The string used to generate 16-byte initialization vector</param>
        /// <param name="salt">Optionally sets a dynamic salt</param>
        public AESCrypter(string passphrase, string IV, string salt = DEFAULT_SALT)
        {
            key1 = GetKey(passphrase.Trim());
            key2 = SHA256.Create().ComputeHash(key1);
            key3 = SHA256.Create().ComputeHash(
                SHA512.Create().ComputeHash(
                MD5.Create().ComputeHash(key2)));

            AESProvider.KeySize = KEY_SIZE;
            AESProvider.Mode = CipherMode.ECB;
            AESProvider.Padding = PaddingMode.ISO10126;
            AESProvider.Key = key1;
            salt += DEFAULT_SALT;
            AESProvider.IV = GetIV(salt.Length.ToString() + IV + salt + salt + salt + IV + HASH_KEY.Length.ToString());
        }

        private byte[] GetKey(string x)
        {
            HashAlgorithm sha512 = SHA512.Create();

            byte[] res = converter.GetBytes(x);

            sha512.Initialize();

            for (int i = 0; i < 3; i++)
            {
                res = sha512.ComputeHash(res);
            }

            sha512.Clear();

            HashAlgorithm sha256 = new HMACSHA256(HASH_KEY);

            return sha256.ComputeHash(res);
        }

        private byte[] GetIV(string x)
        {
            byte[] hash = SHA1.Create().ComputeHash(converter.GetBytes(x));
            return hash.Take(16).ToArray();
        }

        public byte[] Encrypt(byte[] plainBytes)
        {
            byte[] secureBytes1, secureBytes2, secureBytes3;
            using (ICryptoTransform encryptor = AESProvider.CreateEncryptor())
                secureBytes1 = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            AESProvider.Key = key2;
            using (ICryptoTransform encryptor = AESProvider.CreateEncryptor())
                secureBytes2 = encryptor.TransformFinalBlock(secureBytes1, 0, secureBytes1.Length);
            AESProvider.Key = key3;
            using (ICryptoTransform encryptor = AESProvider.CreateEncryptor())
                secureBytes3 = encryptor.TransformFinalBlock(secureBytes2, 0, secureBytes2.Length);
            AESProvider.Key = key1;

            return secureBytes3;
        }

        public string Encrypt(string plainText)
        {
                byte[] plainBytes = converter.GetBytes(plainText);
                // Return encrypted bytes as a string in Base64 format
                return Convert.ToBase64String(Encrypt(plainBytes));
        }

        public byte[] Decrypt(byte[] secureBytes)
        {
            byte[] plainBytes3, plainBytes2, plainBytes1;
            
            AESProvider.Key = key3;
            using (ICryptoTransform decryptor = AESProvider.CreateDecryptor())
                plainBytes3 = decryptor.TransformFinalBlock(secureBytes, 0, secureBytes.Length);
            AESProvider.Key = key2;
            using (ICryptoTransform decryptor = AESProvider.CreateDecryptor())
                plainBytes2 = decryptor.TransformFinalBlock(plainBytes3, 0, plainBytes3.Length);
            AESProvider.Key = key1;
            using (ICryptoTransform decryptor = AESProvider.CreateDecryptor())
                plainBytes1 = decryptor.TransformFinalBlock(plainBytes2, 0, plainBytes2.Length);

            return plainBytes1;
        }

        public string Decrypt(string secureText)
        {
            byte[] secureBytes = Convert.FromBase64String(secureText);
            return converter.GetString(Decrypt(secureBytes));
        }

    }
}
