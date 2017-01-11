using HasherSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HasherSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class PBKDF2Hasher : Hasher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public override string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = PBKDF2(password, salt, _config.Iterations, _config.HashBytesLength);

            string saltString = Convert.ToBase64String(salt);
            string hashString = Convert.ToBase64String(hash);

            return Hasher.FormatHash(_config.Algorithm, _config.Iterations, _config.HashBytesLength, saltString, hashString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public override bool ValidatePassword(string password, string validPasswordHash, out bool requireHashUpdate)
        {
            byte[] salt;
            byte[] hash;
            int iterations;
            string algorith;
            requireHashUpdate = false;

            try
            {
                //TODO: 
                ParseHashSections(Hasher.SplitHash(validPasswordHash), out algorith, out iterations, out salt, out hash);

                //TODO: 
                if (algorith != _config.Algorithm ||
                    iterations != _config.Iterations ||
                    salt.Length != _config.SaltBytesLength ||
                    hash.Length != _config.HashBytesLength)
                {
                    requireHashUpdate = true;
                }


                //TODO: 
                byte[] passwordHash = PBKDF2(password, salt, iterations, hash.Length);

                //TODO: 
                return SlowEquals(passwordHash, hash);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="password">The password</param>
        /// <param name="salt">The RNG salt</param>
        /// <param name="outputBytes">The number of pseudo-random key bytes generated to return.</param>
        /// <returns></returns>
        public static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            return pbkdf2.GetBytes(outputBytes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="algorith"></param>
        /// <param name="iterations"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static void ParseHashSections(string[] sections, out string algorith, out int iterations, out byte[] salt, out byte[] hash)
        {
            //TODO: 
            if ((algorith = sections[Hasher.ALGORITHM_INDEX]) != "HMACSHA1")
            {
                //TODO: Create custom exception.
                throw new ArgumentException("Unsupported hashing algorith", "algorith");
            }

            //TODO: 
            if (!Int32.TryParse(sections[Hasher.ITERATION_INDEX], out iterations) || iterations <= 0)
            {
                //TODO: Create custom exception.
                throw new ArgumentException("Invalid number of iterations.", "iterations");
            }

            //TODO: 
            try
            {
                salt = Convert.FromBase64String(sections[Hasher.SALT_INDEX]);
            }
            catch (Exception ex)
            {
                //TODO: Create custom exception.
                throw new HashFormatException("Invalid salt.", "salt", ex);
            }

            //TODO: 
            try
            {
                hash = Convert.FromBase64String(sections[Hasher.HASH_INDEX]);
            }
            catch (Exception ex)
            {
                //TODO: Create custom exception
                throw new HashFormatException("Invalid Hash.", "hash", ex);
            }
        }
    }
}
