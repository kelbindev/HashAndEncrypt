using EncryptionManager.Encrypt;
using EncryptionManager.Hash;
using System.Text;

var ValueToBeProcessed = "KELBIN-PC";

Console.WriteLine("ORIGINAL VALUE: " + ValueToBeProcessed);

//#region hash
//Console.WriteLine("HASH START===============================================");
//Console.WriteLine("==============================================================================================");
//Console.WriteLine("MD5 Hash: " + await MD5Hasher.HashValue(ValueToBeProcessed));

//Console.WriteLine("SHA256 Hash: " + await SHA256Hasher.HashValue(ValueToBeProcessed));
//#endregion
//#region hmac
//const string hmacSecretKey = "MyUltimateSecretKey12345";
//Console.WriteLine("HMAC Hash: " + await HMACHasher.HashValue(ValueToBeProcessed, hmacSecretKey));
//Console.WriteLine("==============================================================================================");
//Console.WriteLine("HASH DONE===============================================");
//#endregion
//Console.WriteLine();
//#region aes
//Console.WriteLine("AES START===============================================");
//Console.WriteLine("==============================================================================================");
//var aesKey = AESEncrpytor.generateNewAESKey();
//var aesContent = AESEncrpytor.EncryptValue(ValueToBeProcessed, aesKey);

//var aesResult = aesContent.Item1;
//var iv = aesContent.Item2;

//Console.WriteLine("AES ENCRYPTED CONTENT: " + Convert.ToHexString(aesResult));
//var aesDecryptedResult = AESEncrpytor.DecryptValue(aesResult, aesKey, iv);
//Console.WriteLine("AES DECRYPTED CONTENT: " + Encoding.UTF8.GetString(aesDecryptedResult));
//Console.WriteLine("==============================================================================================");
//Console.WriteLine("AES DONE===============================================");
//#endregion
//Console.WriteLine();
#region rsa
Console.WriteLine("RSA START===============================================");
Console.WriteLine("==============================================================================================");
int keyLength = 2048;

RSAEncryptor.GenerateKeys(keyLength, out var publicKey, out var privateKey);

Console.WriteLine("RSA Public Key Base64: " + RSAEncryptor.KeyToBase64String(publicKey));
Console.WriteLine("RSA Private Key Base64: " + RSAEncryptor.KeyToBase64String(privateKey));

Console.WriteLine("RSA Public Key Base64 Replace New Line: " + RSAEncryptor.KeyToBase64String(publicKey).Replace(Environment.NewLine, "\\r\\n"));
Console.WriteLine("RSA Private Key Base64 Replace New Line: " + RSAEncryptor.KeyToBase64String(privateKey).Replace(Environment.NewLine, "\\r\\n"));

Console.WriteLine();

var rsaContent = RSAEncryptor.EncryptValue(ValueToBeProcessed, publicKey);
Console.WriteLine("RSA ENCRYPTED CONTENT: " + Convert.ToHexString(rsaContent));
var rsaDecryptedContent = RSAEncryptor.DecryptValue(rsaContent, privateKey);
Console.WriteLine("RSA DECRYPTED CONTENT: " + Encoding.UTF8.GetString(rsaDecryptedContent));
Console.WriteLine("==============================================================================================");
Console.WriteLine("RSA DONE===============================================");
#endregion;