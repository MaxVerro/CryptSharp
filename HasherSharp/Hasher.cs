using HasherSharp.Configurations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HasherSharp
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Hasher
    {
        #region Constants

        /**
         * 
         * This section should never be modified. If the value of those constants is changed
         * we will not be able to recalculate hash, therefore we won't be able to validate passwords.
		 * 
         **/

        /// <summary>
        /// This value should never be changed.
        /// </summary>
        public const int ALGORITHM_INDEX = 0;

        /// <summary>
        /// This value should never be changed.
        /// </summary>
        public const int ITERATION_INDEX = 1;

        /// <summary>
        /// This value should never be changed.
        /// </summary>
        public const int SIZE_INDEX = 2;

        /// <summary>
        /// This value should never be changed.
        /// </summary>
        public const int SALT_INDEX = 3;

        /// <summary>
        /// This value should never be changed.
        /// </summary>
        public const int HASH_INDEX = 4;

        #endregion

        #region Private/Protected Members

        /// <summary>
        /// RNGCryptoServiceProvider
        /// 
        /// .NET Random Number Generator used to generate strong and random salts.
        /// </summary>
        private RNGCryptoServiceProvider _rngCsp;

        /// <summary>
        /// HasherSharpConfig 
        /// 
        /// 
        /// </summary>
        protected HasherSharpConfig _config;

        #endregion

        #region Public Members

        /// <summary>
        /// HasherSharpConfig
        /// 
        /// 
        /// </summary>
        public HasherSharpConfig Config
        {
            get
            {
                return ConfigurationManager.GetSection("hasherSharpConfig") as HasherSharpConfig ?? new HasherSharpConfig();
            }
        }

        #endregion

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
        public abstract bool ValidatePassword(string password, string hash, out bool requireHashUpdate);

        #endregion

        #region Virtual Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual byte[] GenerateSalt()
        {
            byte[] salt = new byte[_config.SaltBytesLength];

            _rngCsp.GetBytes(salt);

            return salt;
        }

        #endregion

        #region Static Methods


        /// <summary>
        /// 
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
                => $"{algorith}|{iterations}|{hashBytesLength}|{salt}|{hashBytesLength}";

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

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="config"></param>
        public Hasher()
        {
            //On va créer not RNG
            _rngCsp = new RNGCryptoServiceProvider();
        }
    }
}
