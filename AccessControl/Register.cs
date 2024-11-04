namespace Main_Practice.AccessControl;

using Tools;
using DATABASE;
using Configuration;

public static partial class Security
{
    // Реєстрація
    public static void Register(Account acc)
    {
        using var db = new DbController();
        Account newAccount = new Account();
        
        
        // Створення тимчасової змінної для зберігання значень і перевірки на команду
        string? value;
        string? command;
        
        switch (acc.AccountType)
        {
            case AccountType.Quest:
            case AccountType.User:
                // Відмальовка рамки для форми реєстрації
                Console.Clear();
                TableGen.DrawFrame(Config.FormWidth, Config.FormHeight);
        
                // Назва реєстраційної форми
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
                Console.Write(Text.AlignCenter("[ Реєстрація нового акаунта ]", Config.FormWidth - 2));
        
                // Поля реєстраційної форми
                Console.SetCursorPosition(Config.PosX + 1 + (Config.FormWidth / 3), Config.PosY + 5);
                Console.Write("Ім'я:    ");

                Console.SetCursorPosition(Config.PosX + 1 + (Config.FormWidth / 3), Config.PosY + 8);
                Console.Write("Пароль:  ");
                
                // Малювання лінії під полем вводу імені
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                Console.Write(TableGen.Line(Config.FormWidth - 2));
        
                Console.SetCursorPosition(Config.PosX + 10 + (Config.FormWidth / 3), Config.PosY + 5);
                (value, command) = Input.ReadName();
                if (command == "Exit") return;
                newAccount.Username = value ?? "None";
                
                // Стирання лінії під полем вводу імені
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                Console.Write(new string(' ', Config.FormWidth - 2));
                
                // Малювання лінії під полем вводу пароля
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 9);
                Console.Write(TableGen.Line(Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 10 + (Config.FormWidth / 3), Config.PosY + 8);
                (value, command) = Input.ReadPassword();
                if (command == "Exit") return;
                newAccount.Password = value ?? "None";
                
                // Стирання лінії під полем вводу пароля
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 9);
                Console.Write(new string(' ', Config.FormWidth - 2));

                // Реєстрація нового акаунт до бази даних
                db.Accounts.Add(newAccount);
                db.SaveChanges();
        
                // Вивід про успішність входу в акаунт
                Console.SetCursorPosition(Config.PosX, Config.PosY + 2 + Config.FormHeight + 3);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    Text.AlignCenter("Користувач успішно зареєстрований !", Config.FormWidth));
                Console.ResetColor();
        
                Console.SetCursorPosition(Config.PosX + 5, Console.CursorTop);
                Console.WriteLine("Натисність любу клавішу. . .");
                Console.ReadKey(true);
        
                Console.Clear();

                break;
            
            case AccountType.Admin:
                // Відмальовка рамки для форми реєстрації
                Console.Clear();
                TableGen.DrawFrame(Config.FormWidth, Config.FormHeight + 5);
        
                // Назва реєстраційної форми
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
                Console.Write(Text.AlignCenter($"[ {Text.Bold("Реєстрація")} ]", Config.FormWidth + 6));
        
                // Поля реєстраційної форми
                Console.SetCursorPosition(Config.PosX + 1 + (Config.FormWidth / 3), Config.PosY + 5);
                Console.Write("Ім'я:    ");

                Console.SetCursorPosition(Config.PosX + 1 + (Config.FormWidth / 3), Config.PosY + 8);
                Console.Write("Пароль:  ");
        
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                Console.Write(TableGen.Line(Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 10 + (Config.FormWidth / 3), Config.PosY + 5);
                (value, command) = Input.ReadName();
                if (command == "Exit") return;
                newAccount.Username = value ?? "None";
        
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                Console.Write(new string(' ', Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 9);
                Console.Write(TableGen.Line(Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 10 + (Config.FormWidth / 3), Config.PosY + 8);
                (value, command) = Input.ReadPassword();
                if (command == "Exit") return;
                newAccount.Password = value ?? "None";
                
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 9);
                Console.Write(new string(' ', Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 10);
                Console.Write(Text.AlignCenter("Тип акаунту", Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 11);
                Console.Write(Text.AlignCenter($"\u256d{TableGen.Line(18)}\u256e    \u256d{TableGen.Line(18)}\u256e", Config.FormWidth - 2));
                
                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 12);
                Console.Write("\u2502");
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 13);
                Console.Write(Text.AlignCenter($"\u2570{TableGen.Line(18)}\u256f    \u2570{TableGen.Line(18)}\u256f", Config.FormWidth - 2));
                
                // Малювання центральних перегородок
                Console.SetCursorPosition(Config.PosX + 22, Config.PosY + 12);
                Console.Write("\u2502");
                Console.SetCursorPosition(Console.CursorLeft + 4, Config.PosY + 12);
                Console.Write("\u2502");
                Console.SetCursorPosition(Console.CursorLeft + 18, Config.PosY + 12);
                Console.Write("\u2502");
                
                Console.SetCursorPosition(Config.PosX + 4, Config.PosY + 12);
                Console.Write(Text.AlignCenter($">> {Text.Colored("Адміністратор", Color.Green)}", 18));
                
                Console.SetCursorPosition(Config.PosX + 28, Config.PosY + 12);
                Console.Write(Text.AlignCenter("Користувач", 18));
                
                // Малювання лінії під лівою рамкою
                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 14);
                Console.Write(TableGen.Line(20));
                
                Console.SetCursorPosition(Config.PosX + 23, Config.PosY + 12);
                
                // Пустий нескінченний цикл, який не закінчиться, поки користувач не натисне один із запропонованих варіантів
                while (true)
                {
                    Console.CursorVisible = false;
                    
                    var keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.RightArrow && Console.GetCursorPosition().Left != Config.PosX + 47)
                    {
                        // Зняття вибірки з лівої рамки
                        Console.SetCursorPosition(Config.PosX + 4, Config.PosY + 12);
                        Console.Write(Text.AlignCenter("Адміністратор", 18));
                        
                        // Вибірка правої рамки
                        Console.SetCursorPosition(Config.PosX + 30, Config.PosY + 12);
                        Console.Write($" >> {Text.Colored("Користувач", Color.Green)}");
                        
                        // Прибирання лінії з лівої рамки
                        Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 14);
                        Console.Write(new string(' ', 20));
                        
                        // Малювання лінії під правою рамкою
                        Console.SetCursorPosition(Config.PosX + 27, Config.PosY + 14);
                        Console.Write(TableGen.Line(20));
                        
                        Console.SetCursorPosition(Config.PosX + 47, Config.PosY + 12);
                    }
                    
                    else if (keyInfo.Key == ConsoleKey.LeftArrow && Console.GetCursorPosition().Left != Config.PosX + 23)
                    {
                        // Зняття вибірки з правої рамки
                        Console.SetCursorPosition(Config.PosX + 28, Config.PosY + 12);
                        Console.Write(Text.AlignCenter("Користувач", 18));
                        
                        // Вибірка лівої рамки
                        Console.SetCursorPosition(Config.PosX + 4, Config.PosY + 12);
                        Console.Write(Text.AlignCenter($">> {Text.Colored("Адміністратор", Color.Green)}", 18));
                        
                        // Прибирання лінії з правої рамки
                        Console.SetCursorPosition(Config.PosX + 27, Config.PosY + 14);
                        Console.Write(new string(' ', 20));
                        
                        // Малювання лінії під лівою рамкою
                        Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 14);
                        Console.Write(TableGen.Line(20));
                        
                        Console.SetCursorPosition(Config.PosX + 23, Config.PosY + 12);
                    }
                    
                    else if (keyInfo.Key == ConsoleKey.Enter && Console.GetCursorPosition().Left == Config.PosX + 23)
                    {
                        newAccount.AccountType = AccountType.Admin;
                        
                        Console.SetCursorPosition(Config.PosX + 4, Config.PosY + 12);
                        Console.Write(Text.AlignCenter($"{Text.Colored("\u2714\ufe0f", Color.Green)} Адміністратор", 25));
                        Console.SetCursorPosition(Config.PosX + 24, Config.PosY + 12);
                        
                        // Стирання правої рамки
                        for (int i = 11; i <= 13; i++)
                        {
                            Console.SetCursorPosition(Config.PosX + 27, Config.PosY + i);
                            Console.Write(new string(' ', 20));
                        }
                        
                        Console.CursorVisible = true;
                        
                        break;
                    }
                    
                    else if (keyInfo.Key == ConsoleKey.Enter && Console.GetCursorPosition().Left == Config.PosX + 47)
                    {
                        newAccount.AccountType = AccountType.User;
                        
                        Console.SetCursorPosition(Config.PosX + 28, Config.PosY + 12);
                        Console.Write(Text.AlignCenter($"{Text.Colored("\u2714\ufe0f", Color.Green)} Користувач", 25));
                        Console.SetCursorPosition(Config.PosX + 44, Config.PosY + 12);
                        
                        // Стирання лівої рамки
                        // Стирання правої рамки
                        for (int i = 11; i <= 13; i++)
                        {
                            Console.SetCursorPosition(Config.PosX + 3, Config.PosY + i);
                            Console.Write(new string(' ', 20));
                        }
                        
                        Console.CursorVisible = true;
                        
                        break;
                    }
                }

                // Реєстрація нового акаунта до бази даних
                db.Accounts.Add(newAccount);
                db.SaveChanges();
        
                // Вивід про успішність реєстрації аккаунта, із додатковим форматуванням
                Console.SetCursorPosition(Config.PosX, Config.PosY + 1 + Config.FormHeight + 7);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    Text.AlignCenter($"{(newAccount.AccountType == AccountType.Admin ? "Адміністратор" : "Користувач")} успішно зареєстрований !", Config.FormWidth));
                Console.ResetColor();
                
                Console.SetCursorPosition(Config.PosX, Config.PosY + Config.FormHeight + 9);
                Console.WriteLine(Text.AlignCenter(TableGen.Line((newAccount.AccountType == AccountType.Admin ? 38 : 35)), Config.FormWidth));
        
                Console.SetCursorPosition(Config.PosX, Console.CursorTop);
                Console.WriteLine("Натисність любу клавішу. . .");
                Console.ReadKey(true);
        
                Console.Clear();

                break;
        }
    }
}