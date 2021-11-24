using System;
using System.Collections.Generic;
using dnlib.DotNet;
using Server.Utils;

namespace Server.Obfuscation.Impl.Renaming
{
    public class ModuleRenamer : IObfuscation
    {
        private readonly int _stringLength = 16;

        public string path;
        public string main;

        public void Handle(ModuleDefMD md)
        {
            foreach (var moduleDef in md.Assembly.Modules)
            {
                foreach (var typeDef in moduleDef.Types)
                {
                    var randomTypeName = RandomizationUtils.RandomString(_stringLength);
                    var randomTypeNamespace = RandomizationUtils.RandomString(_stringLength);

                    if (typeDef.Name == "Program")
                    {
                        ConsoleUtils.WriteLine($"{typeDef.Name} {typeDef.Namespace}");
                        path = $"{randomTypeNamespace}.{randomTypeName}";
                    }
                    typeDef.Name = randomTypeName;
                    typeDef.Namespace = randomTypeNamespace;
                    
                    foreach (var fieldDef in typeDef.Fields)    
                    {
                        fieldDef.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var methodDef in typeDef.Methods)
                    {
                        if(methodDef.IsConstructor || methodDef.IsGetter || methodDef.IsSetter || methodDef.IsVirtual)
                            continue;
                        
                        foreach (var parameter in methodDef.Parameters)
                        {
                            parameter.Name = RandomizationUtils.RandomString(_stringLength);
                        }

                        var randomString = RandomizationUtils.RandomString(64);
                        if (methodDef.Name == "Main")
                        {
                            main = randomString;
                        }
                        ConsoleUtils.WriteLine($"Name: {methodDef.Name}, {randomString}");
                        ConsoleUtils.WriteLine($"{methodDef.IsRuntime} | {methodDef.IsVirtual} | {methodDef.IsAbstract} | {methodDef.IsAssembly} | {methodDef.IsFamily} | {methodDef.IsFinal} | {methodDef.IsFire} | {methodDef.IsGetter} | {methodDef.IsManaged} | {methodDef.IsNative}");
                        ConsoleUtils.WriteLine($"{methodDef.IsInternalCall}");
                        
                        methodDef.Name = randomString;
                    }
                    
                    foreach (var propertyDef in typeDef.Properties)
                    {
                        propertyDef.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var genericParameter in typeDef.GenericParameters)
                    {
                        genericParameter.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                }
            }
        }
    }
}