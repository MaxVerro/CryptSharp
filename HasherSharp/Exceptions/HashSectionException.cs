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
    class HashSectionException : HashException
    {
        /// <summary>
        /// 
        /// </summary>
        public HashSectionException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public HashSectionException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public HashSectionException(string message, Exception innerException)  : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="section"></param>
        public HashSectionException(string message, string section) : base(message, section)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="section"></param>
        /// <param name="innerException"></param>
        public HashSectionException(string message, string section, Exception innerException) : base(message, section, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected HashSectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
