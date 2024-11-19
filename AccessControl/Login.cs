namespace Main_Practice.AccessControl;

using Tools;
using DATABASE;
using Configuration;
public static partial class Security
{
    // Вхід в акаунт
    public static Account Login()
    {
        using var db = new DbController();
        
        // Створення тимчасової змінної для зберігання значень і перевірки на команду
        string? value;
        string? command;
        
        // Відмальовка рамки для форми входу в аккаунт
        Console.Clear();
        TableGen.DrawFrame(Config.FormWidth, Config.FormHeight - 3);
        
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
        Console.Write(Text.AlignCenter($"[ {Text.Bold("Вхід в аккаунт")} ]", Config.FormWidth + 6));
        
        // Малювання поля для імені
        Console.SetCursorPosition(Config.PosX + 1 , Config.PosY + 1 + (Config.FormHeight / 3) + 1);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        Console.SetCursorPosition(Config.PosX + 1 + (Config.FormWidth / 3), Config.PosY + 1 + (Config.FormHeight / 3));
        Console.Write("Ім'я: ");
        
        // Ввід імені користувача
        (value, command) = Input.ReadStringValue();
        if (command == "Exit") return new Account("Quest", "quest", AccountType.Quest);
        var name = value ?? "None";
        
        // Стирання лінії під полем вводу імені
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1 + Config.FormHeight / 3 + 1);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Малювання поля для вводу пароля
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1 + (Config.FormHeight / 2) + 1);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
            
        Console.SetCursorPosition(Config.PosX + 1 + (Config.FormWidth / 3), Config.PosY + 1 + (Config.FormHeight / 2));
        Console.Write("Пароль: ");

        (value, command) = Input.ReadPassword();
        if (command == "Exit") return new Account("Quest", "quest", AccountType.Quest);
        var password = value ?? "None";
        
        // Стирання лінії під полем вводу пароля
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1 + (Config.FormHeight / 2) + 1);
        Console.Write(new string(' ', Config.FormWidth - 2));

        var user = db.Accounts.FirstOrDefault(el => el.Username == name);
    
        if (user != null)
            if (user.ValidatePassword(password))
            {
                // Вивід про успішність входу в акаунт
                Console.SetCursorPosition(Config.PosX, Config.PosY + 2 + Config.FormHeight);
                Console.WriteLine(Text.AlignCenter(Text.Colored("Успішний вхід в аккаунт !", Color.Green) , Config.FormWidth + 9));
                    
                Console.SetCursorPosition(Config.PosX + 5, Console.CursorTop);
                Console.WriteLine("Натисність любу клавішу. . .");
                Console.ReadKey(true);
                    
                return user;
            }
        
        // Вивід про неправильність введення даних
        Console.SetCursorPosition(Config.PosX, Config.PosY + 2 + Config.FormHeight);
        Console.WriteLine(Text.AlignCenter(Text.Colored("Неправильні введені дані !", Color.Red) , Config.FormWidth + 9));
    
        Console.SetCursorPosition(Config.PosX + 5, Console.CursorTop);
        Console.WriteLine("Натисність любу клавішу. . .");
        Console.ReadKey(true);
    
        return new Account("Quest", "quest", AccountType.Quest);
    }
}
