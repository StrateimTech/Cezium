using System;
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
                    foreach (var nestedType in typeDef.NestedTypes)
                    {
                        TypeNestedRecursion(nestedType);
                    }
                    HandleType(typeDef);
                }
            }
        }

        private void TypeNestedRecursion(TypeDef typeDef)
        {
            HandleType(typeDef);
            foreach (var nestedType in typeDef.NestedTypes)
            {
                HandleType(typeDef);
                TypeNestedRecursion(nestedType);
            }
        }

        private void HandleType(TypeDef typeDef)
        {
            var randomTypeName = RandomizationUtils.RandomString(_stringLength);
            var randomTypeNamespace = RandomizationUtils.RandomString(_stringLength);

            if (typeDef.Name == "Program")
            {
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
                foreach (var parameter in methodDef.Parameters)
                {
                    parameter.Name = RandomizationUtils.RandomString(_stringLength);
                }
                
                if (methodDef.IsConstructor || methodDef.IsGetter || methodDef.IsSetter || methodDef.IsVirtual)
                    continue;

                var randomString = RandomizationUtils.RandomString(64);
                if (methodDef.Name == "Main")
                {
                    main = randomString;
                }

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