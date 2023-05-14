using System.Security.Cryptography;
using System.Security;
using System.Text;

namespace Project.UserService.Api.Features.Cryptor;

public static class CryptExtensions
{
    private const int _keysize = 256;

    private static readonly byte[] InitVectorBytes = Encoding.ASCII.GetBytes("2JT85K9LEWo92Aw3");

    public static async Task<string> Encrypt(this string plainText, string passPhrase, int authId = 0)
    {
        string text = authId.ToString().PadLeft(12, '0');
        byte[] bytes = Encoding.ASCII.GetBytes("uIy71Oh" + text);
        byte[] bytes2 = Encoding.UTF8.GetBytes(plainText);
        using PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, bytes);
        byte[] bytes3 = password.GetBytes(32);
        using RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        using ICryptoTransform encryptor = symmetricKey.CreateEncryptor(bytes3, InitVectorBytes);
        using MemoryStream memoryStram = new MemoryStream();
        await using CryptoStream cryptoStream = new CryptoStream(memoryStram, encryptor, CryptoStreamMode.Write);
        await cryptoStream.WriteAsync(bytes2, 0, bytes2.Length);
        await cryptoStream.FlushFinalBlockAsync();
        byte[] bytes4 = memoryStram.ToArray();
        return bytes4.ConvertToHex();
    }

    public static string Decrypt(this string cipherText, string passPhrase, int authId = 0)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            throw new SecurityException(cipherText);
        }

        string text = authId.ToString().PadLeft(12, '0');
        using Aes aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        if (text == null)
        {
            throw new SecurityException(text);
        }

        Encoding.ASCII.GetBytes("uIy71Oh" + text);
        using PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, Encoding.ASCII.GetBytes("uIy71Oh" + text));
        if (passwordDeriveBytes == null)
        {
            throw new SecurityException("secretKey");
        }

        aes.Key = passwordDeriveBytes.GetBytes(32);
        byte[] array = aes.DecryptCbc(cipherText.ConvertToBytes().ToArray(), InitVectorBytes);
        if (array == null)
        {
            throw new SecurityException("plainText");
        }

        return Encoding.UTF8.GetString(array, 0, array.Length);
    }

    public static byte[] ConvertToBytes(this string source)
    {
        string source2 = source;
        return (from x in Enumerable.Range(0, source2.Length)
                where x % 2 == 0
                select Convert.ToByte(source2.Substring(x, 2), 16)).ToArray();
    }

    public static string ConvertToHex(this byte[] bytes)
    {
        StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2 + 2);
        foreach (byte value in bytes)
        {
            StringBuilder stringBuilder2 = stringBuilder;
            StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(0, 1, stringBuilder2);
            handler.AppendFormatted(value, "X2");
            stringBuilder2.Append(ref handler);
        }

        return stringBuilder.ToString();
    }
}
