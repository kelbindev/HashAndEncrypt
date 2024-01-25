using System.Security.Cryptography;
using System.Text;

namespace EncryptionManager.Hash;
public static class SHA256Hasher
{
    public async static Task<string> HashValue(string valueToHash)
    {
        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(valueToHash));

        byte[] hashOne;
        using (var sha256 = SHA256.Create())
        {
            hashOne = await sha256.ComputeHashAsync(strStreamOne);
        }

        return Convert.ToHexString(hashOne);
    }
}
