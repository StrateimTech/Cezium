using System;

namespace Client.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteLine(string line)
        {
            Console.WriteLine($" > {line}");
        }

        public static void WriteCentered(string value)
        {
            int length = Console.WindowWidth - value.Length;
            if (length < 2)
            {
                Console.WriteLine(value);
                return;
            }

            Console.SetCursorPosition(length / 2, Console.CursorTop);
            Console.WriteLine(value);
        }
    }
}