namespace Main_Practice.AccessControl;

using DATABASE;
using System.IO;

public static class Session
{
    private static Account? _account = null;
    private static readonly string SessionPath = "session.dat";

    public static Account? Current
    {
        get
        {
            using var db = new DbController();
            
            // Якщо файла з сесією не існує - повертаємо null
            if (!File.Exists(SessionPath))
                return null;

            if (_account == null)
            {
                // Конвертуємо байти у початкове значення - ідентифікатор акаунта
                var accountId = BitConverter.ToInt32(File.ReadAllBytes(SessionPath), 0);
                _account = db.Accounts.Find(accountId);
            }
            
            // Повертаємо акаунт поточної сесії
            return _account;
        }

        set
        {
            using var file = File.Open(SessionPath, FileMode.OpenOrCreate);
            
            if (value != null)
            {
                file.SetLength(0);
                file.Write(BitConverter.GetBytes(value.Id));
                file.Flush();
                _account = value;
            }
        }
    }
}
