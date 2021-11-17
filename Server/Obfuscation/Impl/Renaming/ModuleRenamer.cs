using System;
using System.Collections.Generic;
using dnlib.DotNet;
using Server.Utils;

namespace Server.Obfuscation.Impl.Renaming
{
    public class ModuleRenamer : IObfuscation
    {
        private readonly int _stringLength = 8;

        public void Handle(ModuleDefMD md)
        {
            foreach (var moduleDef in md.Assembly.Modules)
            {
                Console.WriteLine($"MDName: {moduleDef.Name}");
                foreach (var typeDef in moduleDef.Types)
                {
                    Console.WriteLine($"TDName: {typeDef.Name}");
                    if (typeDef.Name != "Program")
                    {
                        typeDef.Name = RandomizationUtils.RandomString(_stringLength);
                        typeDef.Namespace = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var fieldDef in typeDef.Fields)    
                    {
                        Console.WriteLine($"FDName: {fieldDef.Name}");
                        fieldDef.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var methodDef in typeDef.Methods)
                    {
                        Console.WriteLine($"MDName: {methodDef.Name}");
                        
                        foreach (var parameter in methodDef.Parameters)
                        {
                            Console.WriteLine($"MP: {parameter.Name}");
                            parameter.Name = RandomizationUtils.RandomString(_stringLength);
                        }
                        
                        // if(methodDef.Name == "Main")
                            // continue;
                        // Console.WriteLine($"MDName: {methodDef.Name}");
                        // methodDef.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var propertyDef in typeDef.Properties)
                    {
                        Console.WriteLine($"PD: {propertyDef.Name}");
                        propertyDef.Name = RandomizationUtils.RandomString(_stringLength);
                    
                        // foreach (var methodDef in propertyDef.GetMethods)
                        // {
                        //     Console.WriteLine($"PD MD: {methodDef.Name}");
                        //     methodDef.Name = RandomizationUtils.RandomString(_stringLength);
                        // }
                    }
                    
                    foreach (var genericParameter in typeDef.GenericParameters)
                    {
                        Console.WriteLine($"GP: {genericParameter.Name}");
                        genericParameter.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                }
            }
        }
    }
}