using System;

namespace Main.Utils
{
    public class ConsoleUtils
    {
        public static void WriteCentered(string value)
        {
            Console.SetCursorPosition((Console.WindowWidth - value.Length) / 2, Console.CursorTop);
            Console.WriteLine(value);
        }
        
        public static void WriteCentered(string value, params object[] args)
        {
            var formattedString = String.Format(value, args);
            Console.SetCursorPosition((Console.WindowWidth - formattedString.Length) / 2, Console.CursorTop);
            Console.WriteLine(formattedString);
        }
    }
}