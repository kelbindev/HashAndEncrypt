using EncryptionManager.Encrypt;
using EncryptionManager.Hash;
using System.Text;

var ValueToBeProcessed = "MYCONTENTTOHASH";

Console.WriteLine("ORIGINAL VALUE: " + ValueToBeProcessed);

#region hash
Console.WriteLine("HASH START===============================================");
Console.WriteLine("==============================================================================================");
Console.WriteLine("MD5 Hash: " + await MD5Hasher.HashValue(ValueToBeProcessed));

Console.WriteLine("SHA256 Hash: " + await SHA256Hasher.HashValue(ValueToBeProcessed));
#endregion

#region hmac
const string hmacSecretKey = "MyUltimateSecretKey12345";
Console.WriteLine("HMAC Hash: " + await HMACHasher.HashValue(ValueToBeProcessed, hmacSecretKey));
Console.WriteLine("==============================================================================================");
Console.WriteLine("HASH DONE===============================================");
#endregion

Console.WriteLine();

#region aes
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
#endregion

Console.WriteLine();

#region rsa
Console.WriteLine("RSA START===============================================");
Console.WriteLine("==============================================================================================");
int keyLength = 2048;

RSAEncryptor.GenerateKeys(keyLength, out var publicKey, out var privateKey);

var base64PublicKey = "4br9jir1d2UPUyN9RMe0uPcwDJFoqEo6eEa4fW4Pp/bb9s5Lz61CZOZzQ7sbhDeVCpGIsJWhmbXnTh9NNIkGUXLc6ZEzOY2/zQcqLGy5vpcF5SpAFmrJgh4qL+KSwZkIOk/d4T7AVYgK8/kAosz4aFydUaUORiio3dEVxyk5aoc8S/egVwkjfzbdbCXlrRkiKjvvfn1HbjCSbT8oZNejcYoJu+O44+A2bEBloX/S10XZQ//PzMuCiObcNQEyvZx+IbldtGFeLM+/Hb0/pR5ysswFIBpKvkFSMABLLzFPar2u9/WCIWzf3Z0Xsu69mUrg9P4mztj0Ion7A5JznBO+5Q==\r\nAQAB\r\n";
var base64PrivateKey = "4br9jir1d2UPUyN9RMe0uPcwDJFoqEo6eEa4fW4Pp/bb9s5Lz61CZOZzQ7sbhDeVCpGIsJWhmbXnTh9NNIkGUXLc6ZEzOY2/zQcqLGy5vpcF5SpAFmrJgh4qL+KSwZkIOk/d4T7AVYgK8/kAosz4aFydUaUORiio3dEVxyk5aoc8S/egVwkjfzbdbCXlrRkiKjvvfn1HbjCSbT8oZNejcYoJu+O44+A2bEBloX/S10XZQ//PzMuCiObcNQEyvZx+IbldtGFeLM+/Hb0/pR5ysswFIBpKvkFSMABLLzFPar2u9/WCIWzf3Z0Xsu69mUrg9P4mztj0Ion7A5JznBO+5Q==\r\nAQAB\r\nJETJLKiWXCKuvar8G6sCzJ1a6QoMCM40atLiHbUras9tNFCdObca7d9hMcbEew/7MIGFtumz12C004aP/xrtGvUbraOY2Wx/HtBB5HVrusXFOrb5KSDrU67JFHyRG+4ctlHS0rgkaxm6uMyPX5kTaKFI3a9phbhOuaU4nJFxGj5nBjhKgYLA7Q745eKj7iRNqzVf0BjRdqoPuzF01PjX2riVgNzvuNKUKwpxCZRGOgPjfSKyS7PD3BEjcAJBcdw12KviTeCFOBtsHY1BsvgxUKHdCN1cT+we6wsKEdKkRJaJbl5LPBzp+oZXgVL8SfRPKmaMyO09lVhx1GbxobTuGQ==\r\n/Ps5HClmYCJhCViQHeaW2hlZC7Yb6HFSiwENXzNWFJsof9KLVXM+WyRVRtiPEZZji9QPg9T7Do7OKLFIBM9Uhaawe8maabNVol/FvUymP0fqOPVCAT43uVEWaNWcrO6I13Z3LHYbDq4AAokcyARh2fgm/cRdr44tZAQX/gocgvs=\r\n5GyGSl7PxwOQ4dDitNeR5OMO3mYALzQ2OLIiUrzFLCSztMMFCNAF8nu8V9G1ld/tTafNTVHc0F1cQCcnFKTeatIHG7xSee3p16x6Urz50qNBi1XRZxSR9tSnutjbuCQhcNc49ZSPeAZsEenq6YyIVmucTDVE5R5N7P3ObinmH58=\r\nOorwdmHmPQz3o7AI+cJo2mPPRXSJFMtUrLI7o/I/U1lsj9frQoT+wxyvV0u6H/nLY4pZZMqeponJj4UnA2XvS1Z9tMe8ogVMJd0/50SbnD1KKJApDByXzJzFMNFuBl7U7/eMMI/MRyNhATU1odvRwR05+LO+cmfWfTStUK6KVo0=\r\nXevtj7jKUDc8JE2iWd9lYqql23YvTzl1rfw4trJdlFgG+CVT9ZqrR1fvSmC0/EoVpd+AP/GQDLDGlK5EmG1t8pgnShU2CGJ1yzNlHI4NXrm34pXHw02m8snfqakvNtjMUzx7dCbAkwZ4+yimaZ9aE8atfnxMtrSuL+rwLUXXFFs=\r\nozD9IRRDXwUBLeH78Yx+CF4b9ic2bfRNgyo+LtZVHu+6libpN4qWNRktsp9JA/0OxV5GPMnoKmf4jcAJwPAnfFtw6SMeo5pTSxlinjRIld81w6LtDjWMFCLN4PAh9MGsjSxAMmhc6w5aYG0v9BxqSVM3NPG5yZDQHt2o2mxZJrc=\r\n";

var newPublicKey = RSAEncryptor.Base64StringToKey(base64PublicKey);
var newPrivateKey = RSAEncryptor.Base64StringToKey(base64PrivateKey);

var rsaContent = RSAEncryptor.EncryptValue(ValueToBeProcessed, newPublicKey);
Console.WriteLine("RSA ENCRYPTED CONTENT: " + Convert.ToHexString(rsaContent));
var rsaDecryptedContent = RSAEncryptor.DecryptValue(rsaContent, newPrivateKey);
Console.WriteLine("RSA DECRYPTED CONTENT: " + Encoding.UTF8.GetString(rsaDecryptedContent));
Console.WriteLine("==============================================================================================");
Console.WriteLine("RSA DONE===============================================");
#endregion;