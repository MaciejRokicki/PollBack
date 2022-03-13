namespace PollBack.Core.Interfaces.Services
{
    public interface IEncryptionService
    {
        string EncryptBySHA256(string value);
        bool VerifySHA256(string valueString, string hash);
    }
}
