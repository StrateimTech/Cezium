using System;

namespace Client.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteCentered(string value)
        {
            int length = (Console.WindowWidth - value.Length);
            if (length < 2)
            {
                Console.WriteLine(value);
                return;
            }
            Console.SetCursorPosition(length / 2, Console.CursorTop);
            Console.WriteLine(value);
        }
        
        public static void WriteCentered(string value, string name)
        {
            int length = (Console.WindowWidth - value.Length);
            Console.Write($"[{name}]");
            if (length < 2)
            {
                Console.WriteLine(value);
                return;
            }
            Console.SetCursorPosition(length / 2, Console.CursorTop);
            Console.WriteLine(value);
        }
        
        public static void WriteCentered(string value, params object[] args)
        {
            var formattedString = String.Format(value, args);
            int length = (Console.WindowWidth - formattedString.Length);
            if (length < 2)
            {
                Console.WriteLine(value);
                return;
            }
            Console.SetCursorPosition((Console.WindowWidth - formattedString.Length) / 2, Console.CursorTop);
            Console.WriteLine(formattedString);
        }
        
        public static void WriteCentered(string value, string name, params object[] args)
        {
            var formattedString = String.Format(value, args);
            int length = (Console.WindowWidth - formattedString.Length);
            Console.Write($"[{name}]");
            if (length < 2)
            {
                Console.WriteLine(value);
                return;
            }
            Console.SetCursorPosition((Console.WindowWidth - formattedString.Length) / 2, Console.CursorTop);
            Console.WriteLine(formattedString);
        }
    }
}