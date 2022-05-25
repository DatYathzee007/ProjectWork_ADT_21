using System;

namespace IL41ML_HFT_2021221.Client
{
    public static class HelperMethods
    {
        public static void OneItemToConsole<T>(this T input, string info)
        {
            if (input != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\tBEGIN: " + info);
                Console.ResetColor();
                Console.WriteLine(input.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine('\t' + " END.\t(Press a key)");
                Console.ResetColor();
                Console.ReadKey();
            }
            else
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
        public static void MessageNotExisting<T>(this T input, string table)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Entity is not Existing in table: {0} with {1}: {2} \n(Press a key)", table, nameof(input), input.ToString());
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
