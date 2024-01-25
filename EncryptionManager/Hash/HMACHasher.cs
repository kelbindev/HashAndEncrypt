using System.Security.Cryptography;
using System.Text;

namespace EncryptionManager.Hash;
public static class HMACHasher
{
    private static readonly string secretKey = "mySuperSecretKeyy";
    public static async Task<string> HashValue(string valueToHash)
    {
        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(valueToHash));
        byte[] hashOne;
        byte[] key = Encoding.UTF8.GetBytes(secretKey);
        using (var hmac = new HMACSHA256(key))
        {
            hashOne = await hmac.ComputeHashAsync(strStreamOne);
        }
        
        return Convert.ToHexString(hashOne);
    }

}
