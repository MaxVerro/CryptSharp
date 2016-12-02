using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptSharp
{
    /// <summary>
    /// 
    /// </summary>
    class PBKDF2Encrypter : Encrypter
    {
        /// <summary>
        /// 
        /// </summary>
        public override int HashBytesLength
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Iterations
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int SaltBytesLength
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Algorithm
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public override string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = PBKDF2(password, salt, Iterations, HashBytesLength);

            string saltString = Convert.ToBase64String(salt);
            string hashString = Convert.ToBase64String(hash);

            return Encrypter.FormatHash(Algorithm, Iterations, HashBytesLength, saltString, hashString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public override bool ValidatePassword(string password, string validPasswordHash)
        {
            byte[] salt;
            byte[] hash;
            int iterations;
            string algorith;
            
            ParseHashSections(Encrypter.SplitHash(validPasswordHash), out algorith, out iterations, out salt, out hash);

            byte[] passwordHash = PBKDF2(password, salt, iterations, hash.Length);

            return ValidatePasswordInteral(passwordHash, hash);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private bool ValidatePasswordInteral(byte[] password, byte[] hash)
        {
            return SlowEquals(password, hash);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="outputBytes">The number of pseudo-random key bytes generated to return.</param>
        /// <returns></returns>
        public static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
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
        public static bool ParseHashSections(string[] sections, out string algorith, out int iterations, out byte[] salt, out byte[] hash)
        {
            bool valid = true;

            if ((algorith = sections[Encrypter.ALGORITHM_INDEX]) == "HMACSHA1")
            {
                valid = false;
            }

            if (!Int32.TryParse(sections[Encrypter.ALGORITHM_INDEX], out iterations) || iterations <= 0)
            {
                valid = false;
            }

            try
            {
                salt = Convert.FromBase64String(sections[Encrypter.SALT_INDEX]);
            }
            catch (Exception)
            {
                salt = new byte[0];
                valid = false;
            }

            try
            {
                hash = Convert.FromBase64String(sections[Encrypter.HASH_INDEX]);
            }
            catch (Exception)
            {
                hash = new byte[0];
                valid = false;
            }

            return valid;
        }
    }
}
