using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CSN.Persistence.Extensions
{
    public class RSAManagerExtension
    {
        private const string path = "../CSN.Persistence/RSA_Keys";

        public void WriteToFiles()
        {
            Directory.CreateDirectory(path);

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                var privateKey = rsa.ExportParameters(true);
                var publicKey = rsa.ExportParameters(false);

                File.WriteAllText(Path.Combine(path, "privateKey.txt"), Convert.ToBase64String(rsa.ExportCspBlob(true)));
                File.WriteAllText(Path.Combine(path, "publicKey.txt"), Convert.ToBase64String(rsa.ExportCspBlob(false)));
            }
        }

        public (RSAParameters privateKey, RSAParameters publicKey) ReadFromFiles()
        {
            var privateKeyBytes = Convert.FromBase64String(File.ReadAllText(Path.Combine(path, "privateKey.txt")));
            var publicKeyBytes = Convert.FromBase64String(File.ReadAllText(Path.Combine(path, "publicKey.txt")));

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportCspBlob(privateKeyBytes);
                var privateKey = rsa.ExportParameters(true);

                rsa.ImportCspBlob(publicKeyBytes);
                var publicKey = rsa.ExportParameters(false);

                return (privateKey, publicKey);
            }
        }
    }
}