using Project.UserService.Common.Extensions;
using Project.UserService.Infrastructure.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Project.UserService.Infrastructure.Utils;

/// <summary>
/// Предоставляет криптографические методы для шифровки и расшифровки
/// </summary>
public static class Cryptor
{
	private const int _keysize = 256;
	private static readonly byte[] InitVectorBytes = Encoding.ASCII.GetBytes("2JT85K9LEWo92Aw3");

	/// <summary>
	/// Шифровка
	/// </summary>
	public static string Encrypt(this string plainText, string passPhrase, int authId = 0)
	{
		var userIdPad = authId.ToString().PadLeft(12, '0');
		var saltInfo = Encoding.ASCII.GetBytes("uIy71Oh" + userIdPad);
		var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
		using var password = new PasswordDeriveBytes(passPhrase, saltInfo);
		#pragma warning disable CS0618
		var keyBytes = password.GetBytes(_keysize / 8);
		#pragma warning restore CS0618
		#pragma warning disable SYSLIB0022
		using var symmetricKey = new RijndaelManaged();
		#pragma warning restore SYSLIB0022
		symmetricKey.Mode = CipherMode.CBC;
		using var encryptor = symmetricKey.CreateEncryptor(keyBytes, InitVectorBytes);
		using var memoryStream = new MemoryStream();
		using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
		cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
		cryptoStream.FlushFinalBlock();
		var cipherTextBytes = memoryStream.ToArray();
		var resval = Convert.ToBase64String(cipherTextBytes);

		return cipherTextBytes.ConvertToHex();
	}

	/// <summary>
	/// Расшифровка
	/// </summary>
	/// <param name="cipherText"></param>
	/// <param name="passPhrase"></param>
	/// <param name="authId"></param>
	/// <returns></returns>
	/// <exception cref="DecryptTextIsNullException"></exception>
	/// <exception cref="DecryptException"></exception>
	public static string Decrypt(this string cipherText, string passPhrase, int authId = 0)
	{
		if (string.IsNullOrEmpty(cipherText))
			throw new DecryptTextIsNullException();

		var userIdPad = authId.ToString().PadLeft(12, '0');
		using var aes = Aes.Create();
		aes.Mode = CipherMode.CBC;

		if (userIdPad is null)
			throw new DecryptException(cipherText, passPhrase);

		Encoding.ASCII.GetBytes("uIy71Oh" + userIdPad);
		using var secretKey = new PasswordDeriveBytes(passPhrase, Encoding.ASCII.GetBytes("uIy71Oh" + userIdPad));

		if (secretKey is null)
			throw new DecryptException(cipherText, passPhrase);
		#pragma warning disable CS0618
		aes.Key = secretKey.GetBytes(_keysize / 8);
		#pragma warning restore CS0618
		var plainText = aes.DecryptCbc(cipherText.ConvertToBytes()
				.ToArray(),
			InitVectorBytes);

		if (plainText is null)
			throw new DecryptException(cipherText, passPhrase);

		var decryptedData = Encoding.UTF8.GetString(plainText, 0, plainText.Length);

		return decryptedData;
	}
}