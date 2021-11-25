using System;
using System.Text;

namespace Server.Utils
{
    public static class ConsoleUtils
    {
        private static readonly TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
        
        public static void WriteLine(string line, string displayName = null, bool displayTime = true, bool logOutput = true)
        {
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo);
            var formattedTime = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
            var output = displayName != null
                ? $" [{(displayTime ? formattedTime + " | " : "")}{displayName}] | {line}"
                : $" {(displayTime ? "["+ formattedTime + "] | " : "")}{line}";
            Console.WriteLine(output);
        }
    }
}