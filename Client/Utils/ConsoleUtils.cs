using System;

namespace Client.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteLine(string line, string displayName = "Client")
        {
            Console.WriteLine(displayName != null
                ? $" [{(displayName != "Client" ? $"Client | {displayName}" : displayName)}] | {line}" : $" {line}");
        }
    }
}