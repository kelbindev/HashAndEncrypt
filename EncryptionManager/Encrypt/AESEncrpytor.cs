using System.Security.Cryptography;
using System.Text;

namespace EncryptionManager.Encrypt;
public static class AESEncrpytor
{
    public static byte[] generateNewAESKey()
    {
        return GenerateAESKey();
    }

    public static (byte[], byte[]) EncryptValue(string valueToEncrypt, byte[] key)
    {
        var data = Encoding.UTF8.GetBytes(valueToEncrypt);
        var encryptedData = Encrypt(data, key, out var iv);
        return (encryptedData, iv);
    }

    public static byte[] DecryptValue(byte[] data, byte[] key, byte[] iv)
    {
         return Decrypt(data, key, iv);
    }

    private static byte[] Encrypt(byte[] data, byte[] key, out byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Mode = CipherMode.CBC; // better security
            aes.Key = key;
            aes.GenerateIV(); // IV = Initialization Vector
            using (var encryptor = aes.CreateEncryptor())
            {
                iv = aes.IV;
                return encryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
    private static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC; // same as for encryption
            using (var decryptor = aes.CreateDecryptor())
            {
                return decryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
    private static byte[] GenerateAESKey()
    {
        var rnd = new RNGCryptoServiceProvider();
        var b = new byte[16];
        rnd.GetNonZeroBytes(b);
        return b;
    }
}
