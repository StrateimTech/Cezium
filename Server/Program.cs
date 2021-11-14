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
        }

        public void ProcessRequest()
        {
            // RsaUtils.RsaDecrypt(bytes, RsaCryptoService.ExportParameters(true), false)
            // Handle RSA Handshake (EG: Give (Server Public key -> Client), Get (Client -> Client Public Key))
            
            //Check if account data exists blah blah blah...

            byte[] fileAssemblyBytes = File.ReadAllBytes("a");

            var obfuscatedAssembly = AssemblyUtils.GetObfuscatedAssembly(fileAssemblyBytes);

            // Use Client's RSA Public Key & Encrypt to send over network.
            // var encryptedAssembly = RsaUtils.RsaEncrypt(obfuscatedAssembly, rsaCryptoService.ExportParameters(false), false);
        }
    }
}