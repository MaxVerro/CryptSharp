using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HasherSharp.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public class HasherSharpConfig : ConfigurationSection
    {
        /// <summary>
        /// Algorithm
        /// .NET implementation only support HMACSHA1 (for now). We will use it as default value.
        /// </summary>
        [ConfigurationProperty("algorithm", DefaultValue = "HMACSHA1", IsRequired = true)]
        public string Algorithm
        {
            get
            {
                return (string)this["algorithm"];
            }
            set
            {
                this["algorithm"] = value;
            }
        }

        /// <summary>
        /// HashBytesLength
        /// .NET implementation only support HMACSHA1 (for now). We will use 20 bytes as default value.
        /// 
        /// Note: Output more bits doesn't make the hash more secure, but it costs the defender a lot more
        /// time while not costing the attacker. An attacker will just compare the first hash function sized
        /// output saving them the time to generate the reset of the PBKDF2 output. To mkae the hash more 
        /// secure increase the number of iterations instead.
        /// 
        /// </summary>
        [IntegerValidator(ExcludeRange = false, MinValue = 20)]
        [ConfigurationProperty("hashBytesLength", DefaultValue = 20, IsRequired = true)]
        public int HashBytesLength
        {
            get
            {
                return (int)this["hashBytesLength"];
            }
            set
            {
                this["hashBytesLength"] = value;
            }
        }

        /// <summary>
        /// SaltBytesLength
        /// 
        /// As storage permits, use a 32 byte or 64 byte salt actual size dependent on protection function.
        /// 
        /// Note: Salts serve two purposes: 
        ///     1) Prevent the protected form from revealing two identical credentials.
        ///     2) Augment entropy fed to protecting function without relying on credential complexity.
        ///        The second aims to make pre-computed lookup attacks [*2] on an individual credential 
        ///        and time-based attacks on a population intractable.
        /// </summary>
        [IntegerValidator(ExcludeRange = false, MinValue = 32)]
        [ConfigurationProperty("saltBytesLength", DefaultValue = 32, IsRequired = true)]
        public int SaltBytesLength
        {
            get
            {
                return (int)this["saltBytesLength"];
            }
            set
            {
                this["saltBytesLength"] = value;
            }
        }

        /// <summary>
        /// Iteration
        /// 
        /// While there is a minimum number of iterations recommended to ensure data safety, this value changes every year as technology improves.
        /// However, it is critical to understand that a single work factor does not fit all designs. Experimentation is important.
        /// 
        /// Note: The number of iterations should be reviewed every year.
        /// 
        /// </summary>
        [IntegerValidator(ExcludeRange = false, MinValue = 1)]
        [ConfigurationProperty("iterations", DefaultValue = 20000, IsRequired = true)]
        public int Iterations
        {
            get
            {
                return (int)this["iterations"];
            }
            set
            {
                this["iterations"] = value;
            }
        }
    }
}
