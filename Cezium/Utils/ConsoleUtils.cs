using System;

namespace Cezium.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteLine(string line, bool showTime = false)
        {            
            if (showTime)
            {
                Console.Write($" > {line}");
                var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, Program.TimeZoneInfo);
                var formattedTime = $"{dateTime:MM/dd/yy HH:mm:ss} <";
                Console.SetCursorPosition(Console.WindowWidth - formattedTime.Length, Console.CursorTop);
                Console.WriteLine(formattedTime);
                return;
            }
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