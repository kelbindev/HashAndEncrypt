using EncryptionManager.Encrypt;
using EncryptionManager.Hash;
using System.Text;

var ValueToBeProcessed = "MYCONTENTTOHASH";

Console.WriteLine("ORIGINAL VALUE: " + ValueToBeProcessed);

Console.WriteLine("HASH START===============================================");
Console.WriteLine("==============================================================================================");
Console.WriteLine("MD5 Hash: " + await MD5Hasher.HashValue(ValueToBeProcessed));

Console.WriteLine("SHA256 Hash: " + await SHA256Hasher.HashValue(ValueToBeProcessed));

const string hmacSecretKey = "MyUltimateSecretKey12345";
Console.WriteLine("HMAC Hash: " + await HMACHasher.HashValue(ValueToBeProcessed, hmacSecretKey));
Console.WriteLine("==============================================================================================");
Console.WriteLine("HASH DONE===============================================");
Console.WriteLine();
//AES
Console.WriteLine("AES START===============================================");
Console.WriteLine("==============================================================================================");
var aesKey = AESEncrpytor.generateNewAESKey();
var aesContent = AESEncrpytor.EncryptValue(ValueToBeProcessed, aesKey);

var aesResult = aesContent.Item1;
var iv = aesContent.Item2;

Console.WriteLine("AES ENCRYPTED CONTENT: " + Convert.ToHexString(aesResult));
var aesDecryptedResult = AESEncrpytor.DecryptValue(aesResult, aesKey, iv);
Console.WriteLine("AES DECRYPTED CONTENT: " + Encoding.UTF8.GetString(aesDecryptedResult));
Console.WriteLine("==============================================================================================");
Console.WriteLine("AES DONE===============================================");

Console.WriteLine();

Console.WriteLine("RSA START===============================================");
Console.WriteLine("==============================================================================================");
int keyLength = 2048;

RSAEncryptor.GenerateKeys(keyLength, out var publicKey, out var privateKey);

var rsaContent = RSAEncryptor.EncryptValue(ValueToBeProcessed, publicKey);
Console.WriteLine("RSA ENCRYPTED CONTENT: " + Convert.ToHexString(rsaContent));
var rsaDecryptedContent = RSAEncryptor.DecryptValue(rsaContent, privateKey);
Console.WriteLine("RSA DECRYPTED CONTENT: " + Encoding.UTF8.GetString(rsaDecryptedContent));
Console.WriteLine("==============================================================================================");
Console.WriteLine("RSA DONE===============================================");