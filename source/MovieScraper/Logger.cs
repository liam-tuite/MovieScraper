using System;

namespace MovieScraper
{
    public static class Logger
    {
        /// <summary>
        /// Test a condition and if it evaluates to false, print the given message.
        /// </summary>
        /// <param name="condition">The condition to test</param>
        /// <param name="message">The error message</param>
        public static void Test(bool condition, string message)
        {
            if (!condition)
                throw new Exception(message);
        }

        /// <summary>
        /// Print a message as information.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"INFO:\n{message}");
        }

        /// <summary>
        /// Print a message as a warning.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"WARNING:\n{message}");

            Console.ForegroundColor = originalColor;
        }
    }
}
