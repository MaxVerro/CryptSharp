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
    class HashFormatException : HashException
    {
        /// <summary>
        /// 
        /// </summary>
        public HashFormatException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public HashFormatException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public HashFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="section"></param>
        public HashFormatException(string message, string section) : base(message, section)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="section"></param>
        /// <param name="innerException"></param>
        public HashFormatException(string message, string section, Exception innerException) : base(message, section, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected HashFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
