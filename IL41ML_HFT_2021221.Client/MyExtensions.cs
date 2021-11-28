using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Client
{
    public static class MyExtensions
    {
        /// <summary>
        /// Linq compatible ToConsole method, that prints out List elements, with a uniq Header and Footer.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="input">Input List.</param>
        /// <param name="str">Input string, user defined that shows up in the Header and Footer.</param>
        public static void ToConsole<T>(this IEnumerable<T> input, string str)
        {
            if (input != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\tBEGIN: " + str);
                Console.ResetColor();

                foreach (T item in input)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine('\t' + str + " END.\t(Press a key)");
                Console.ResetColor();
                Console.ReadKey();
            }
            else
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
    }
}
