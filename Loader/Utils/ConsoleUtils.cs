using System;

namespace Loader.Utils
{
    public class ConsoleUtils
    {
        public static void WriteLine(string line, string displayName = "Loader")
        {
            Console.WriteLine(displayName != null
                ? $" [{displayName}] | {line}" : $" {line}");
        }
        
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
    }
}