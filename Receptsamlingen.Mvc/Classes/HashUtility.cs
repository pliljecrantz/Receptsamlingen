using System;
using System.Security.Cryptography;
using System.Text;

namespace Receptsamlingen.Mvc.Classes
{
    public static class HashUtility
    {
        private enum HashType
        {
            SHA256,
            SHA384,
            SHA512
        }

        public static bool CheckPassword(string clearTextPassword, string hashedPassword)
        {
            return Hash.VerifyHash(clearTextPassword, HashType.SHA512, hashedPassword);
        }

        public static string HashPassword(string password)
        {
            return Hash.ComputeHash(password, HashType.SHA512, null);
        }

        private static class Hash
        {
            public static string ComputeHash(string password, HashType type, byte[] saltBytes)
            {
                // Create salt if not exists
                if (saltBytes == null)
                {
                    saltBytes = CreateSalt(saltBytes);
                }

                // Convert plain text into a byte array
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);

                // Allocate array, whish will hold plain text and salt
                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

                // Copy plain text bytes into resulting array
                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                // Append salt bytes to the resulting array
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
                }

                // Because we support multiple hashing algorithms, we must define
                // hash object as a common (abstract) base class. We will specify the
                // actual hashing algorithm class later during object creation.
                HashAlgorithm hash = CreateHashAlgorithm(type);

                // Compute hash value of our plain text with appended salt.
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

                //Copy hash bytes into resulting array 
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                // Append salt bytes to the result
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
                }

                // Convert result into a base64-encoded string and return
                return Convert.ToBase64String(hashWithSaltBytes);
            }

            public static bool VerifyHash(string password, HashType type, string hashValue)
            {
                byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

                // We must know the size of hash (without salt)
                int hashSizeInBits = HashBitSize(type);
                int hashSizeInBytes;

                // Convert size of hash from bits to bytes.
                hashSizeInBytes = hashSizeInBits / 8;

                // Make sure that the specified hash value is long enough
                if (hashWithSaltBytes.Length < hashSizeInBytes)
                    return false;

                // Allocate array to hold original salt bytes retrieved from hash
                byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

                // Copy salt from the end of the hash to the new array.
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];
                }

                // Compute a new hash string.
                string expectedHashString = ComputeHash(password, type, saltBytes);

                // If the computed hash matches the specified hash,
                // the plain text value must be correct.
                return (hashValue == expectedHashString);
            }

            private static byte[] CreateSalt(byte[] saltBytes)
            {
                int minSaltSize = 4;
                int maxSaltSize = 5;

                // Generate a random number for the size of the salt
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, wish will hold the salt
                saltBytes = new byte[saltSize];

                // Initialize a random number generator
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values
                rng.GetNonZeroBytes(saltBytes);

                return saltBytes;
            }

            private static int HashBitSize(HashType type)
            {
                int hashSizeInBits;
                switch (type)
                {
                    case HashType.SHA256:
                        hashSizeInBits = 256;
                        break;
                    case HashType.SHA384:
                        hashSizeInBits = 384;
                        break;
                    case HashType.SHA512:
                        hashSizeInBits = 512;
                        break;
                    default:
                        hashSizeInBits = 128;
                        break;
                }
                return hashSizeInBits;
            }

            private static HashAlgorithm CreateHashAlgorithm(HashType type)
            {
                HashAlgorithm hash;
                switch (type)
                {
                    case HashType.SHA256:
                        hash = new SHA256Managed();
                        break;
                    case HashType.SHA384:
                        hash = new SHA384Managed();
                        break;
                    case HashType.SHA512:
                        hash = new SHA512Managed();
                        break;
                    default:
                        hash = new MD5CryptoServiceProvider();
                        break;
                }
                return hash;
            }
        }
    }
}