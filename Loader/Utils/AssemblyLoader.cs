using System;
using System.Reflection;

namespace Loader.Utils
{
    public static class AssemblyLoader
    {
        public static void LoadAssembly(object assembly, string path, string main)
        {
            Type t = ((Assembly)assembly).GetType(path);
            if (t == null)
            {
                throw new Exception("[TypeLoader] Failed to find type.");
            }
            
            var methodInfo = t.GetMethod(main);
            if (methodInfo == null)
            {
                throw new Exception("[MethodLoader] Failed to find method.");
            }
            
            var o = Activator.CreateInstance(t);
            try
            {
                methodInfo.Invoke(o, null);
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteLine($"{ex}", "Client");
            }
        }
    }
}