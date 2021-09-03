using System;

namespace Main.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteCentered(string value)
        {
            int length = (Console.WindowWidth - value.Length);
            if (length < 2)
            {
                throw new Exception("Length cannot be shorter than two.");
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
                throw new Exception("Length cannot be shorter than two.");
            }
            Console.SetCursorPosition((Console.WindowWidth - formattedString.Length) / 2, Console.CursorTop);
            Console.WriteLine(formattedString);
        }
    }
}