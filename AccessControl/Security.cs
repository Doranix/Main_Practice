namespace Main_Practice.AccessControl;

using System.Text;
using System.Security.Cryptography;
using Tools;

public static class Security
{
    private const short FormWidth = 50;
    private const short FormHeight = 10;
    private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгґдеєжзийіїклмнопрстуфхцчшщьюяАБВГҐДЕЄЖЗИЙІЇКЛМНОПРСТУФХЦЧШЩЬЮЯ0123456789!@#$%&*_-=+<>?/\\|";
    private const byte MaxLength = 20;
    private static int _genId = 1;
    
    private static List<Account> _accounts = new List<Account>();

    public static int GenId
    {
        get => _genId++;
    }
    
    // Функція хешування
    public static string Hashing(string value)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
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
    
    
    // Метод для вводу пароля, із заміною значень ні зірочки
    private static string ReadPassword()
    {
        StringBuilder password = new StringBuilder();

        ConsoleKeyInfo keyInfo;

        // Заміна вводимих символів на зірочки
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            else if (keyInfo.Key != ConsoleKey.Backspace && password.Length <= MaxLength && AllowedCharacters.Contains(keyInfo.KeyChar))
            {
                password.Append(keyInfo.KeyChar);
                Console.Write('*');
            }
        }
        
        // Повернення введеного пароля
        return password.ToString();
    }
    
    // Метод для вводу імені
    private static string ReadName()
    {
        StringBuilder name = new StringBuilder();

        ConsoleKeyInfo keyInfo;

        // Ввід імені з урахунком максимальної довжини
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (keyInfo.Key == ConsoleKey.Backspace && name.Length > 0)
            {
                name.Remove(name.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            else if (keyInfo.Key != ConsoleKey.Backspace && name.Length <= MaxLength && AllowedCharacters.Contains(keyInfo.KeyChar))
            {
                name.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }
        }
        
        // Повернення введеного пароля
        return name.ToString();
    }
    
    // Реєстрація
    public static void Register(Account acc)
    {
        Account newAccount = new Account();
        
        switch (acc.AccountType)
        {
            case AccountType.Quest:
            case AccountType.User:
                // Відмальовка рамки для форми реєстрації
                Console.Clear();
                TableGen.DrawFrame(FormWidth, FormHeight);
        
                // Назва реєстраційної форми
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 2);
                Console.Write(Text.AlignCenter("[ Реєстрація нового акаунта ]", FormWidth - 2));
        
                // Поля реєстраційної форми
                Console.SetCursorPosition((int) Cursor.X + 1 + (FormWidth / 3), (int) Cursor.Y + 5);
                Console.Write("Ім'я:    ");

                Console.SetCursorPosition((int) Cursor.X + 1 + (FormWidth / 3), (int) Cursor.Y + 8);
                Console.Write("Пароль:  ");
        
                Console.SetCursorPosition((int) Cursor.X + 10 + (FormWidth / 3), (int) Cursor.Y + 5);
                newAccount.Username = ReadName();
        
                Console.SetCursorPosition((int) Cursor.X + 10 + (FormWidth / 3), (int) Cursor.Y + 8);
                newAccount.Password = ReadPassword();
        
                _accounts.Add(newAccount);
        
                Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y + 1 + FormHeight + 3);
                Console.WriteLine("========== Реєстрація успішна ! ==========");
        
                Console.SetCursorPosition((int) Cursor.X, Console.CursorTop);
                Console.WriteLine("Натисність любу клавішу. . .");
                Console.ReadKey(true);
        
                Console.Clear();

                break;
            
            case AccountType.Admin:
                // Відмальовка рамки для форми реєстрації
                Console.Clear();
                TableGen.DrawFrame(FormWidth, FormHeight + 5);
        
                // Назва реєстраційної форми
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 2);
                Console.Write(Text.AlignCenter($"[ {Text.Bold("Реєстрація")} ]", FormWidth + 6));
        
                // Поля реєстраційної форми
                Console.SetCursorPosition((int) Cursor.X + 1 + (FormWidth / 3), (int) Cursor.Y + 5);
                Console.Write("Ім'я:    ");

                Console.SetCursorPosition((int) Cursor.X + 1 + (FormWidth / 3), (int) Cursor.Y + 8);
                Console.Write("Пароль:  ");
        
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 6);
                Console.Write(TableGen.Line(FormWidth - 2));
                
                Console.SetCursorPosition((int) Cursor.X + 10 + (FormWidth / 3), (int) Cursor.Y + 5);
                newAccount.Username = ReadName();
        
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 6);
                Console.Write(new string(' ', FormWidth - 2));
                
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 9);
                Console.Write(TableGen.Line(FormWidth - 2));
                
                Console.SetCursorPosition((int) Cursor.X + 10 + (FormWidth / 3), (int) Cursor.Y + 8);
                newAccount.Password = ReadPassword();
                
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 9);
                Console.Write(new string(' ', FormWidth - 2));
                
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 10);
                Console.Write(Text.AlignCenter("Тип акаунту", FormWidth - 2));
                
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 11);
                Console.Write(Text.AlignCenter($"\u256d{TableGen.Line(18)}\u256e    \u256d{TableGen.Line(18)}\u256e", FormWidth - 2));
                
                Console.SetCursorPosition((int) Cursor.X + 3, (int) Cursor.Y + 12);
                Console.Write("\u2502");
                Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 13);
                Console.Write(Text.AlignCenter($"\u2570{TableGen.Line(18)}\u256f    \u2570{TableGen.Line(18)}\u256f", FormWidth - 2));
                
                // Малювання центральних перегородок
                Console.SetCursorPosition((int) Cursor.X + 22, (int) Cursor.Y + 12);
                Console.Write("\u2502");
                Console.SetCursorPosition(Console.CursorLeft + 4, (int) Cursor.Y + 12);
                Console.Write("\u2502");
                Console.SetCursorPosition(Console.CursorLeft + 18, (int) Cursor.Y + 12);
                Console.Write("\u2502");
                
                Console.SetCursorPosition((int) Cursor.X + 4, (int) Cursor.Y + 12);
                Console.Write(Text.AlignCenter($">> {Text.Colored("Адміністратор", Color.Green)}", 18));
                
                Console.SetCursorPosition((int) Cursor.X + 28, (int) Cursor.Y + 12);
                Console.Write(Text.AlignCenter("Користувач", 18));
                
                // Малювання лінії під лівою рамкою
                Console.SetCursorPosition((int) Cursor.X + 3, (int) Cursor.Y + 14);
                Console.Write(TableGen.Line(20));
                
                Console.SetCursorPosition((int) Cursor.X + 23, (int) Cursor.Y + 12);
                
                // Пустий нескінченний цикл, який не закінчиться, поки користувач не натисне один із запропонованих варіантів
                while (true)
                {
                    Console.CursorVisible = false;
                    
                    var keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.RightArrow && Console.GetCursorPosition().Left != (int) Cursor.X + 47)
                    {
                        // Зняття вибірки з лівої рамки
                        Console.SetCursorPosition((int) Cursor.X + 4, (int) Cursor.Y + 12);
                        Console.Write(Text.AlignCenter("Адміністратор", 18));
                        
                        // Вибірка правої рамки
                        Console.SetCursorPosition((int) Cursor.X + 30, (int) Cursor.Y + 12);
                        Console.Write($" >> {Text.Colored("Користувач", Color.Green)}");
                        
                        // Прибирання лінії з лівої рамки
                        Console.SetCursorPosition((int) Cursor.X + 3, (int) Cursor.Y + 14);
                        Console.Write(new string(' ', 20));
                        
                        // Малювання лінії під правою рамкою
                        Console.SetCursorPosition((int) Cursor.X + 27, (int) Cursor.Y + 14);
                        Console.Write(TableGen.Line(20));
                        
                        Console.SetCursorPosition((int) Cursor.X + 47, (int) Cursor.Y + 12);
                    }
                    
                    else if (keyInfo.Key == ConsoleKey.LeftArrow && Console.GetCursorPosition().Left != (int) Cursor.X + 23)
                    {
                        // Зняття вибірки з правої рамки
                        Console.SetCursorPosition((int) Cursor.X + 28, (int) Cursor.Y + 12);
                        Console.Write(Text.AlignCenter("Користувач", 18));
                        
                        // Вибірка лівої рамки
                        Console.SetCursorPosition((int) Cursor.X + 4, (int) Cursor.Y + 12);
                        Console.Write(Text.AlignCenter($">> {Text.Colored("Адміністратор", Color.Green)}", 18));
                        
                        // Прибирання лінії з правої рамки
                        Console.SetCursorPosition((int) Cursor.X + 27, (int) Cursor.Y + 14);
                        Console.Write(new string(' ', 20));
                        
                        // Малювання лінії під лівою рамкою
                        Console.SetCursorPosition((int) Cursor.X + 3, (int) Cursor.Y + 14);
                        Console.Write(TableGen.Line(20));
                        
                        Console.SetCursorPosition((int) Cursor.X + 23, (int) Cursor.Y + 12);
                    }
                    
                    else if (keyInfo.Key == ConsoleKey.Enter && Console.GetCursorPosition().Left == (int) Cursor.X + 23)
                    {
                        newAccount.AccountType = AccountType.Admin;
                        
                        Console.SetCursorPosition((int) Cursor.X + 4, (int) Cursor.Y + 12);
                        Console.Write(Text.AlignCenter($"{Text.Colored("\u2714\ufe0f", Color.Green)} Адміністратор", 25));
                        Console.SetCursorPosition((int) Cursor.X + 24, (int) Cursor.Y + 12);
                        
                        // Стирання правої рамки
                        for (int i = 11; i <= 13; i++)
                        {
                            Console.SetCursorPosition((int) Cursor.X + 27, (int) Cursor.Y + i);
                            Console.Write(new string(' ', 20));
                        }
                        
                        Console.CursorVisible = true;
                        
                        break;
                    }
                    
                    else if (keyInfo.Key == ConsoleKey.Enter && Console.GetCursorPosition().Left == (int) Cursor.X + 47)
                    {
                        newAccount.AccountType = AccountType.User;
                        
                        Console.SetCursorPosition((int) Cursor.X + 28, (int) Cursor.Y + 12);
                        Console.Write(Text.AlignCenter($"{Text.Colored("\u2714\ufe0f", Color.Green)} Користувач", 25));
                        Console.SetCursorPosition((int) Cursor.X + 44, (int) Cursor.Y + 12);
                        
                        // Стирання лівої рамки
                        // Стирання правої рамки
                        for (int i = 11; i <= 13; i++)
                        {
                            Console.SetCursorPosition((int) Cursor.X + 3, (int) Cursor.Y + i);
                            Console.Write(new string(' ', 20));
                        }
                        
                        Console.CursorVisible = true;
                        
                        break;
                    }
                }
                
                _accounts.Add(newAccount);
        
                // Вивід про успішність реєстрації аккаунта, із додатковим форматуванням
                Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y + 1 + FormHeight + 7);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    Text.AlignCenter($"{(newAccount.AccountType == AccountType.Admin ? "Адміністратор" : "Користувач")} успішно зареєстрований !", FormWidth));
                Console.ResetColor();
                
                Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y + FormHeight + 9);
                Console.WriteLine(Text.AlignCenter(TableGen.Line((newAccount.AccountType == AccountType.Admin ? 38 : 35)), FormWidth));
        
                Console.SetCursorPosition((int) Cursor.X, Console.CursorTop);
                Console.WriteLine("Натисність любу клавішу. . .");
                Console.ReadKey(true);
        
                Console.Clear();

                break;
        }
    }
    
    // Вхід в акаунт
    public static Account Login()
    {
        // Відмальовка рамки для форми входу в аккаунт
        Console.Clear();
        TableGen.DrawFrame(FormWidth, FormHeight - 3);
        
        Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 1);
        Console.Write(Text.AlignCenter($"[ {Text.Bold("Вхід в аккаунт")} ]", FormWidth + 6));
        
        // Малювання поля для імені
        Console.SetCursorPosition((int) Cursor.X + 1 , (int) Cursor.Y + 1 + (FormHeight / 3) + 1);
        Console.Write(TableGen.Line(FormWidth - 2));
        
        Console.SetCursorPosition((int) Cursor.X + 1 + (FormWidth / 3), (int) Cursor.Y + 1 + (FormHeight / 3));
        Console.Write("Ім'я: ");
        
        // Ввід імені користувача
        var name = ReadName();
        
        // Стирання лінії під полем вводу імені
        Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 1 + FormHeight / 3 + 1);
        Console.Write(new string(' ', FormWidth - 2));
        
        // Малювання поля для вводу пароля
        Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 1 + (FormHeight / 2) + 1);
        Console.Write(TableGen.Line(FormWidth - 2));
            
        Console.SetCursorPosition((int) Cursor.X + 1 + (FormWidth / 3), (int) Cursor.Y + 1 + (FormHeight / 2));
        Console.Write("Пароль: ");

        var password = ReadPassword();
        
        // Стирання лінії під полем вводу пароля
        Console.SetCursorPosition((int) Cursor.X + 1, (int) Cursor.Y + 1 + (FormHeight / 2) + 1);
        Console.Write(new string(' ', FormWidth - 2));

        var user = _accounts.Find(acc => acc.Username == name);
        
        if (user != null)
            if (user.ValidatePassword(password))
            {
                // Вивід про успішність входу в акаунт
                Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y + 2 + FormHeight);
                Console.WriteLine(Text.AlignCenter(Text.Colored("Успішний вхід в аккаунт !", Color.Green) , FormWidth + 9));
                        
                Console.SetCursorPosition((int) Cursor.X + 5, Console.CursorTop);
                Console.WriteLine("Натисність любу клавішу. . .");
                Console.ReadKey(true);
                        
                return user;
            }
            
        // Вивід про неправильність введення даних
        Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y + 2 + FormHeight);
        Console.WriteLine(Text.AlignCenter(Text.Colored("Неправильні введені дані !", Color.Red) , FormWidth + 9));
        
        Console.SetCursorPosition((int) Cursor.X + 5, Console.CursorTop);
        Console.WriteLine("Натисність любу клавішу. . .");
        Console.ReadKey(true);
        
        return new Account("Quest", "quest", AccountType.Quest);
    }
}