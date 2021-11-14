using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Loader
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Arguments cannot be zero!");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File doesn't exist!");
                return;
            }

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new Exception($"Platform not supported ({Environment.OSVersion})");
            }

            byte[] fileBytes = File.ReadAllBytes(args[0]);
            Console.WriteLine($"File Size: {fileBytes.Length}");
            
            LoadAssembly(Assembly.Load(fileBytes));
            Array.Clear(fileBytes, 0, fileBytes.Length);
        }

        private static void LoadAssembly(object assembly)
        {
            Type t = ((Assembly)assembly).GetType("Client.Program");
            if (t == null)
            {
                throw new Exception("[TypeLoader] No such type exists.");
            }

            var methodInfo = t.GetMethod("Main");
            if (methodInfo == null)
            {
                throw new Exception("[MethodLoader] No such method exists.");
            }

            var o = Activator.CreateInstance(t);
            methodInfo.Invoke(o, null);
        }
    }
}