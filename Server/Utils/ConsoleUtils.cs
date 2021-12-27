using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Server.Utils
{
    public static class ConsoleUtils
    {
        private static readonly TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "America/Los_Angeles" : "Pacific Standard Time");
        
        public static void WriteLine(string message, string displayName = null, bool displayTime = true, bool logOutput = true)
        {
            var dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo);
            var formattedTime = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
            var output = displayName != null
                ? $" [{(displayTime ? formattedTime + " | " : "")}{displayName}] | {message}"
                : $" {(displayTime ? "["+ formattedTime + "] | " : "")}{message}";
            Console.WriteLine(output);
            if (logOutput)
            {
                var logStringOutput = $" [{formattedTime} | {displayName}] | {message}";
                var logDirectoryLocation = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Logs";
                var logFileLocation = $"{logDirectoryLocation}{Path.DirectorySeparatorChar}{DateTime.Now:yyyy-dd-M}.txt";
                if(!Directory.Exists(logDirectoryLocation)) {
                    Directory.CreateDirectory(logDirectoryLocation);
                }
                File.AppendAllText(logFileLocation, logStringOutput + Environment.NewLine);
            }
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