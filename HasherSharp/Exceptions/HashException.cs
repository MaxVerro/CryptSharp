using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HasherSharp.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    class HashException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Section { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HashException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public HashException(string message) : this(message, String.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="section"></param>
        public HashException(string message, string section) : this(message, section, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public HashException(string message, Exception innerException) : this(message, String.Empty, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="section"></param>
        /// <param name="innerException"></param>
        public HashException(string message, string section, Exception innerException) : base(message, innerException)
        {
            Section = section;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected HashException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
