using System;
using System.Reflection;

namespace Loader.Utils
{
    public static class AssemblyLoader
    {
        public static void LoadAssembly(object assembly, string path, string main)
        {
            foreach (var VARIABLE in ((Assembly)assembly).GetTypes())
            {
                Console.WriteLine($"{VARIABLE.Name} {VARIABLE.Namespace}");
            }
            
            Type t = ((Assembly)assembly).GetType(path);
            if (t == null)
            {
                throw new Exception("[TypeLoader] No such type exists.");
            }
            
            var methodInfo = t.GetMethod(main);
            if (methodInfo == null)
            {
                throw new Exception("[MethodLoader] No such method exists.");
            }
            
            var o = Activator.CreateInstance(t);
            methodInfo.Invoke(o, null);
        }
    }
}