using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Server.Utils;

namespace Server.Assembly
{
    public class AssemblyLoader
    {
        public readonly ManualResetEvent ResetEvent = new(false);
        
        public static byte[] FindLatestAssembly(string folderPath)
        {
            var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".dll"));
            string bestFile = null; 
            foreach (var file in files)
            {
                if (bestFile == null)
                {
                    bestFile = file;
                    continue;
                }
                var bestFileTime = File.GetLastWriteTime(bestFile);
                var fileTime = File.GetLastWriteTime(file);
                if  (DateTime.Compare(fileTime, bestFileTime) > 0)
                {
                    bestFile = file;
                }
            }
            if (bestFile != null)
            {
                return File.ReadAllBytes(bestFile);
            }
            return null;
        }
        
        public void StartWatcher(string folderPath)
        {
            using var watcher = new FileSystemWatcher(folderPath, "*.dll");

            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
            
            watcher.Changed += (sender, args) =>
            {
                ConsoleUtils.WriteLine("Assembly folder change detected", "AssemblyWatcher");
                ConsoleUtils.WriteLine("Looking for best assembly to load", "AssemblyWatcher");
                var assembly = FindLatestAssembly(folderPath);
                if (assembly == null)
                {
                    ConsoleUtils.WriteLine("Couldn't find new assembly using last loaded assembly", "AssemblyWatcher");
                    return;
                }
                ConsoleUtils.WriteLine("Loaded new assembly", "AssemblyWatcher");
                Program.ClientAssembly = assembly;
            };
            
            watcher.EnableRaisingEvents = true;
            ResetEvent.WaitOne();
        }
    }
}