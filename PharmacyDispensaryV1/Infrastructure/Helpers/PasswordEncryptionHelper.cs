using System.Security.Cryptography;
using System.Text;

public static class PasswordEncryptionHelper
{
    // Clave y vector de inicialización (IV).
    // Nota: En un entorno de producción, NO DEBES guardar la clave y el IV
    // de esta manera. Deberías obtenerlos de un lugar seguro (por ejemplo,
    // un almacén de claves como Azure Key Vault o AWS Secrets Manager).
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("estaEsUnaClaveSecretaDe32Bytes!"); // 32 bytes para AES-256
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("esteEsUnIVde16!"); // 16 bytes para AES

    public static string Encrypt(string plainText)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new();
        using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msDecrypt = new(cipherBytes);
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);

        return srDecrypt.ReadToEnd();
    }
}