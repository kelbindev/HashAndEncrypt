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
    public static string KeyToBase64String(RSAParameters rsaParams)
    {
        return ConvertKeyToBase64String(rsaParams);
    }
    public static RSAParameters Base64StringToKey(string base64Key)
    {
        return ConvertBase64StringToKey(base64Key);
    }
    private static string ConvertKeyToBase64String(RSAParameters rsaParams)
    {
        var sb = new StringBuilder();
        sb.AppendLine(Convert.ToBase64String(rsaParams.Modulus));
        sb.AppendLine(Convert.ToBase64String(rsaParams.Exponent));

        // If you're exporting the private key parameters, include them too
        if (rsaParams.D != null)
        {
            sb.AppendLine(Convert.ToBase64String(rsaParams.D));
            sb.AppendLine(Convert.ToBase64String(rsaParams.P));
            sb.AppendLine(Convert.ToBase64String(rsaParams.Q));
            sb.AppendLine(Convert.ToBase64String(rsaParams.DP));
            sb.AppendLine(Convert.ToBase64String(rsaParams.DQ));
            sb.AppendLine(Convert.ToBase64String(rsaParams.InverseQ));
        }

        return sb.ToString();
    }
    private static RSAParameters ConvertBase64StringToKey(string base64Key)
    {
        RSAParameters rsaParams = new RSAParameters();

        string[] keyParts = base64Key.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        rsaParams.Modulus = Convert.FromBase64String(keyParts[0]);
        rsaParams.Exponent = Convert.FromBase64String(keyParts[1]);

        if (keyParts.Length > 2) // Check if private key parameters are included
        {
            rsaParams.D = Convert.FromBase64String(keyParts[2]);
            rsaParams.P = Convert.FromBase64String(keyParts[3]);
            rsaParams.Q = Convert.FromBase64String(keyParts[4]);
            rsaParams.DP = Convert.FromBase64String(keyParts[5]);
            rsaParams.DQ = Convert.FromBase64String(keyParts[6]);
            rsaParams.InverseQ = Convert.FromBase64String(keyParts[7]);
        }

        return rsaParams;
    }
}
