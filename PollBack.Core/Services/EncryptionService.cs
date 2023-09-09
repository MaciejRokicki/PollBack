using Microsoft.Extensions.Options;
using PollBack.Core.AppSettings;
using PollBack.Core.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace PollBack.Core.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly SecuritySettings securitySettings;

        public EncryptionService(IOptions<SecuritySettings> securitySettings)
        {
            this.securitySettings = securitySettings.Value;
        }

        public string EncryptBySHA256(string value)
        {
            if (value == string.Empty)
                return String.Empty; ;

            value = new StringBuilder(value)
                .Append(securitySettings.Salt)
                .ToString();

            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            sha256Hash.Dispose();

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public bool VerifySHA256(string valueString, string hash)
        {
            if (valueString == string.Empty)
                return false;

            if (hash == string.Empty)
                return false;

            string encryptedString = EncryptBySHA256(valueString);

            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            if (stringComparer.Compare(encryptedString, hash) == 0)
                return true;

            return false;
        }
    }
}
