using System;

namespace Server.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteLine(string line, string displayName = null)
        {
            if (displayName != null)
            {
                Console.WriteLine($" [{displayName}] | {line}");
            }
            else
            {
                Console.WriteLine($" {line}");
            }
        }
        
        public static void WriteLine(string line, params object[] args)
        {
            Console.WriteLine($" {line}", args);
        }
        
        public static void WriteLine(string line, string displayName = null, params object[] args)
        {
            if (displayName != null)
            {
                Console.WriteLine($" [{displayName}] | {line}", args);
            }
            else
            {
                Console.WriteLine($" {line}", args);
            }
        }
    }
}