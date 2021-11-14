using System;
using System.Security.Cryptography;

namespace Server.Utils
{
    public static class RsaUtils
    {
        public static byte[] RsaDecrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo, bool doOaepPadding)
        {
            try
            {
                using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
                
                rsaCryptoService.ImportParameters(rsaKeyInfo);

                return rsaCryptoService.Decrypt(dataToDecrypt, doOaepPadding);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        
        public static byte[] RsaEncrypt(byte[] dataToEncrypt, RSAParameters rsaKeyInfo, bool doOaepPadding)
        {
            try
            {
                using RSACryptoServiceProvider rsaCryptoService = new RSACryptoServiceProvider();
                
                rsaCryptoService.ImportParameters(rsaKeyInfo);

                return rsaCryptoService.Encrypt(dataToEncrypt, doOaepPadding);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}