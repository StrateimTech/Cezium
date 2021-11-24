using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using dnlib.DotNet;
using Server.Obfuscation.Impl.Renaming;

namespace Server.Obfuscation
{
    public class ObfuscationHandler
    {
        private readonly byte[] _assembly;

        private readonly List<IObfuscation> _obfuscationHandlers = new()
        {
            new ModuleRenamer()
        };

        public ObfuscationHandler(byte[] assembly)
        {
            _assembly = assembly;
        }

        public (byte[], string, string) Run()
        {
            ModuleDefMD moduleDefMd = ModuleDefMD.Load(_assembly);
            if (moduleDefMd == null)
            {
                throw new Exception("Couldn't load assembly.");
            }

            Console.WriteLine(moduleDefMd.Assembly.Name);

            string path = "";
            string main = "";
            foreach (var obfuscationHandler in _obfuscationHandlers)
            {
                obfuscationHandler.Handle(moduleDefMd);
                if (obfuscationHandler is ModuleRenamer moduleRenamer)
                {
                    main = moduleRenamer.main;
                    path = moduleRenamer.path;
                }
            }
            
            using var stream = new MemoryStream();
            moduleDefMd.Write(stream);
            return (stream.ToArray(), path, main);
        }
    }
}