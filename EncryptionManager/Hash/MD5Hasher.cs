using System.Security.Cryptography;
using System.Text;

namespace EncryptionManager.Hash;
public static class MD5Hasher
{
    public async static Task<string> HashValue(string valueToHash)
    {
        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(valueToHash));
        byte[] hashOne;
        using (var hasher = MD5.Create())
        {
            hashOne = await hasher.ComputeHashAsync(strStreamOne);
        }
        return (string)Convert.ToHexString(hashOne);
    }
}
