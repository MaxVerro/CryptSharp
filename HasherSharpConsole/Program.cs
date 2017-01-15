using HasherSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HasherSharpConsole
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        private static PBKDF2Hasher _hasher = new PBKDF2Hasher();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool requireHashUpdate = false;
            Dictionary<string, string> hash = new Dictionary<string, string>()
            {
                {"Password1", _hasher.HashPassword("Password1") },
                {"Password2", _hasher.HashPassword("Password2") },
                {"Password3", _hasher.HashPassword("Password3") },
                {"Password4", _hasher.HashPassword("Password4") },
                {"Password5", _hasher.HashPassword("Password5") }
            };

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                foreach (KeyValuePair<string, string> kvp in hash)
                {
                    Console.WriteLine($"{kvp.Key} => {kvp.Value}");

                    if (_hasher.ValidatePassword(kvp.Key, kvp.Value, out requireHashUpdate))
                    {
                        Console.WriteLine("Password is valid.");
                    }
                    else
                    {
                        Console.WriteLine("Password is not valid.");
                    }

                    if (requireHashUpdate)
                    {
                        Console.WriteLine("Password config changed. Hash is out of date.");
                        requireHashUpdate = false;
                    }
                }

                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                _hasher.HashPassword("Password1");
                stopwatch.Stop();

                Console.WriteLine($"Computing hash took {stopwatch.ElapsedTicks} ms");

            }
        }
    }
}
