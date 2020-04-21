using System;
using System.Security.Cryptography;

namespace Application.Cryptography.Services
{
    /// <summary>
    /// Provides password hashing methods.
    /// </summary>
    public class CryptoService
    {
        /// <summary>
        /// Generate random salt to be used for hashing users' passwords and comparing hashes.
        /// </summary>
        /// <returns>A byte array containing the 16-bit salt.</returns>
        public byte[] GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            return salt;
        }

        /// <summary>
        /// Calculate the salted hash of a password.
        /// </summary>
        /// <param name="password">The input password to hash.</param>
        /// <param name="salt">The salt to combine with the password when hashing.</param>
        /// <returns>A byte array containing the 36-bit salted hash.</returns>
        public byte[] CalculateHash(string password, byte[] salt)
        {
            return new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20);
        }

        /// <summary>
        /// Combine a salt and a hash together into a base64-encoded string.
        /// </summary>
        /// <param name="salt">The 16-bit salt.</param>
        /// <param name="hash">The 20-bit hash.</param>
        /// <returns>The base64-encoded string containing the salted hash.</returns>
        public string CompressHash(byte[] salt, byte[] hash)
        {
            var saltAndHash = new byte[36];
            Array.Copy(salt, 0, saltAndHash, 0, 16);
            Array.Copy(hash, 0, saltAndHash, 16, 20);

            return Convert.ToBase64String(saltAndHash);
        }

        /// <summary>
        /// Extract the 16-bit salt from a base64-encoded string containing a salted hash.
        /// </summary>
        /// <param name="compressedHash">The base64-encoded hash string.</param>
        /// <returns>A byte array containing the 16-bit salt.</returns>
        public byte[] DecompressSalt(string compressedHash)
        {
            var hashBytes = Convert.FromBase64String(compressedHash);

            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            return salt;
        }
    }
}
