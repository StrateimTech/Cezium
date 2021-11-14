using System;
using System.IO;
using System.Reflection;

namespace Loader
{
    class Program
    {
        // private const string DllPath = @"C:\Users\Etho\Desktop\ConsoleLibrary.dll";
        // private const string DllPath = @"E:\Programming\C#\ConsoleLibrary\publish\ConsoleLibrary.dll";

        static void Main(string[] args)
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

            byte[] fileBytes = File.ReadAllBytes(args[0]);
            Console.WriteLine($"File Size: {fileBytes.Length}");

            var assembly = Assembly.Load(fileBytes);

            Array.Clear(fileBytes, 0, fileBytes.Length);

            Type t = assembly.GetType("Client.Program");
            if (t == null)
            {
                foreach (var type in assembly.GetTypes())
                {
                    Console.WriteLine($"TName: {type.Name}");
                }
                throw new Exception("No such type exists.");
            }

            var methodInfo = t.GetMethod("Main");
            if (methodInfo == null)
            {
                foreach (var methods in t.GetMethods())
                {
                    Console.WriteLine($"MName: {methods.Name}");
                }
                throw new Exception("No such method exists.");
            }

            var o = Activator.CreateInstance(t);
            var r = methodInfo.Invoke(o, null);
        }
    }
}