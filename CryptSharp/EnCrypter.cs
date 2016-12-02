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
    public abstract class Encrypter
    {
        /**
         * 
         * 
         * 
         **/
        #region Constants

        /// <summary>
        /// 
        /// </summary>
        public const int ALGORITHM_INDEX = 0;

        /// <summary>
        /// 
        /// </summary>
        public const int ITERATION_INDEX = 1;

        /// <summary>
        /// 
        /// </summary>
        public const int SIZE_INDEX = 2;

        /// <summary>
        /// 
        /// </summary>
        public const int SALT_INDEX = 3;

        /// <summary>
        /// 
        /// </summary>
        public const int HASH_INDEX = 4;

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public abstract int SaltBytesLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract int HashBytesLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract int Iterations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string Algorithm { get; set; }



        #region Abstract Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public abstract string HashPassword(string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public abstract bool ValidatePassword(string password, string hash);

        #endregion

        #region Virtual Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltBytesLength];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        #endregion

        #region Static Methods


        /// <summary>
        /// Checks if two byte array are equal. Compares every char to prevent timing attacks.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;

            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }

            return diff == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorith"></param>
        /// <param name="iterations"></param>
        /// <param name="hashBytesLength"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string FormatHash(string algorith, int iterations, int hashBytesLength, string salt, string hash)
            => String.Format("{0}|{1}|{2}|{3}|{4}", algorith, iterations, hashBytesLength, salt, hash);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="algorith"></param>
        /// <param name="iterations"></param>
        /// <param name="hashBytesLength"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string[] SplitHash(string hash)
            => hash.Split('|');


        #endregion
    }
}
