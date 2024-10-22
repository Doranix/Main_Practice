namespace Main_Practice.AccessControl;

public class Account
{
    public string Username { get; set; }

    public string Password
    {
        set
        {
            _hashedPassword = Security.Hashing(value);
        }
    }

    private static int _genId = 0;
    private string _hashedPassword = string.Empty;
    public readonly int Id;
    
    public AccountType AccountType { get; set; }

    // Конструктор без параметрів
    public Account()
    {
        Id = _genId++;
        Username = string.Empty;
        Password = string.Empty;
    }
    
    // Звичайний конструктор
    public Account(string username, string password, AccountType type)
    {
        Id = _genId++;
        Username = username;
        Password = Security.Hashing(password);
        AccountType = type;
    }
    
    // Метод для перевірки правильності пароля
    public bool ValidatePassword(string password)
    {
        return Security.Hashing(password) == _hashedPassword;
    }
}