namespace Main_Practice.AccessControl;

using System.Security.Cryptography;
using System.Text;
public static partial class Security
{
    // Функція хешування
    public static string Hashing(string value)
    {
        using var sha256Hash = SHA256.Create();
        
        // Перевожу наданий рядок у байти
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
        
        StringBuilder cryptoString = new StringBuilder();
        
        // Перевожу масив байтів у строкове представлення
        for (int i = 0; i < bytes.Length; i++)
            cryptoString.Append(bytes[i].ToString("x2"));
        
        // Повертаю хешований рядок
        return cryptoString.ToString();
    }
}
