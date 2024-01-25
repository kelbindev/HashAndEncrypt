using System.Security.Cryptography;
using System.Text;

namespace EncryptionManager.Encrypt;
public static class RSAEncryptor
{
    public static void GenerateKeys(int keyLength, out RSAParameters publicKey, out RSAParameters privateKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.KeySize = keyLength;
            publicKey = rsa.ExportParameters(includePrivateParameters: false);
            privateKey = rsa.ExportParameters(includePrivateParameters: true);
        }
    }
    public static byte[] EncryptValue(string valueToEncrypt, RSAParameters publicKey)
    {
        var data = Encoding.UTF8.GetBytes(valueToEncrypt);
        return Encrypt(data, publicKey);
    }

    public static byte[] DecryptValue(byte[] data, RSAParameters privateKey)
    {
        return Decrypt(data, privateKey);
    }

    private static byte[] Encrypt(byte[] data, RSAParameters publicKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
    }
    private static byte[] Decrypt(byte[] data, RSAParameters privateKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);
            return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
    }
}
