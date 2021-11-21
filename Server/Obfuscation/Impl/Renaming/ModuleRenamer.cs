using System;
using System.Collections.Generic;
using dnlib.DotNet;
using Server.Utils;

namespace Server.Obfuscation.Impl.Renaming
{
    public class ModuleRenamer : IObfuscation
    {
        private readonly int _stringLength = 16;

        public void Handle(ModuleDefMD md)
        {
            foreach (var moduleDef in md.Assembly.Modules)
            {
                foreach (var typeDef in moduleDef.Types)
                {
                    if (typeDef.Name != "Program")
                    {
                        typeDef.Name = RandomizationUtils.RandomString(_stringLength);
                        typeDef.Namespace = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var fieldDef in typeDef.Fields)    
                    {
                        fieldDef.Name = RandomizationUtils.RandomString(_stringLength);
                    }
                    
                    foreach (var methodDef in typeDef.Methods)
                    {
                        foreach (var parameter in methodDef.Parameters)
                        {
                            parameter.Name = RandomizationUtils.RandomString(_stringLength);
                        }
                        
                        if(methodDef.Name == "Main")
                            continue;
                        methodDef.Name = RandomizationUtils.RandomString(_stringLength);
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