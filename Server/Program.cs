using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Server.Utils;

namespace Server
{
    class Program
    {
        
        static void Main(string[] args)
        {
            byte[] fileAssemblyBytes = File.ReadAllBytes(@"E:\Programming\Projects\StrateimTech\Cezium\Client\publish\Client.dll");
            
            var obfuscatedAssembly = AssemblyUtils.GetObfuscatedAssembly(fileAssemblyBytes);
            
            Console.WriteLine($"Length: {obfuscatedAssembly.Length}");
            File.WriteAllBytes(@"E:\TestAssemblyA.dll", obfuscatedAssembly);
        }

        public void ProcessRequest()
        {
            // RsaUtils.RsaDecrypt(bytes, RsaCryptoService.ExportParameters(true), false)
            // Handle RSA Handshake (EG: Give (Server Public key -> Client), Get (Client -> Client Public Key))
            
            //Check if account data exists blah blah blah...

            byte[] fileAssemblyBytes = File.ReadAllBytes(@"E:\Programming\C#\ConsoleLibrary\publish\ConsoleLibrary.dll");

            var obfuscatedAssembly = AssemblyUtils.GetObfuscatedAssembly(fileAssemblyBytes);

            // Use Client's RSA Public Key & Encrypt to send over network.
            // var encryptedAssembly = RsaUtils.RsaEncrypt(obfuscatedAssembly, rsaCryptoService.ExportParameters(false), false);
        }
    }
}