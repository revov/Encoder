using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommonCryptography
{
    public static class CryptoRepository
    {
        public static string CalculateHash(string input, HashAlgorithm hashAlgorithm, bool isInBase64)
        {
            Encoding encoder = new UTF8Encoding();
            byte[] buffer = encoder.GetBytes(input);
            buffer = hashAlgorithm.ComputeHash(buffer);

            return isInBase64 ?
                Convert.ToBase64String(buffer)
                : BitConverter.ToString(buffer).Replace("-", "").ToLower();
        }

        public static string CalculateHMACSHA384(byte[] input)
        {
            Encoding encoding = new UTF8Encoding();
            byte[] key = encoding.GetBytes(ConfigurationManager.AppSettings["HMACSHA384Key"]);
            HashAlgorithm hashAlgo = new HMACSHA384(key);
            byte[] hash = hashAlgo.ComputeHash(input);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static void Compress(Stream inputStream, Stream outputStream)
        {
            using (GZipStream gzip = new GZipStream(outputStream, CompressionMode.Compress))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    gzip.Write(buffer, 0, bytesRead);
                }
            }
        }

        public static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                byte[] buffer = new byte[4096];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public static void Decompress(Stream inputStream, Stream outputStream)
        {
            using (GZipStream stream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, bytesRead);
                }
            }
        }

    }
}
