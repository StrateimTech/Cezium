using System.IO;
using System.Security.Cryptography;
using System.Text;
using Server.Obfuscation;

namespace Server.Utils
{
    public static class AssemblyUtils
    {
        public static byte[] GetObfuscatedAssembly(byte[] assembly)
        {
            var obfuscationHandler = new ObfuscationHandler(assembly);
            return obfuscationHandler.Run();
        }
    }
}