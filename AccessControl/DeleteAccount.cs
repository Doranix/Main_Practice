using Main_Practice.Menus;

namespace Main_Practice.AccessControl;

using Tools;
using DATABASE;
using Configuration;

public static partial class Security
{
    public static Account DeleteAccount(Account account)
    {
        using var db = new DbController();

        Console.CursorVisible = false;
        
        switch (account.AccountType)
        {
            case AccountType.User:
            {
                TableGen.DrawFrame(Config.FormWidth, 4);
                
                // Вивід заголовка
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                Console.Write(Text.AlignCenter("Бажаєте видалити цей акаунт ?", Config.FormWidth - 2));
                
                // Вивожу основні поля
                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 3);
                Console.Write(Text.AlignCenter("Так", 23));
                
                Console.SetCursorPosition(Config.PosX + 26, Config.PosY + 3);
                Console.Write(Text.AlignCenter("Ні", 23));

                var currentElement = 1;

                // Обробляємо ввід
                while (true)
                {
                    if (currentElement == 1) // Якщо поточний елемент -> перший
                    {
                        // Виділяємо поточний елемент
                        TableGen.DrawFrame(Config.FormWidth / 2, 1, 1, 2, false);
                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 3);
                        Console.Write(Text.AlignCenter(Text.Colored("Так", Color.Green), 23 + Text.ColoredDiff("Так", Color.Green)));
                    }
                    
                    if (currentElement == 2) // Якщо поточний елемент -> другий
                    {
                        // Виділяємо поточний елемент
                        TableGen.DrawFrame(Config.FormWidth / 2 - 1, 1, Config.FormWidth / 2, 2, false);
                        Console.SetCursorPosition(Config.PosX + 26, Config.PosY + 3);
                        Console.Write(Text.AlignCenter(Text.Colored("Ні", Color.Green), 23 + Text.ColoredDiff("Ні", Color.Green)));
                    }
                    
                    var keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (currentElement == 1) currentElement = 2;
                            break;
                        
                        case ConsoleKey.LeftArrow:
                            if (currentElement == 2) currentElement = 1;
                            break;

                        case ConsoleKey.Enter:
                        {
                            if (currentElement == 1)
                            {
                                // Вивід про успішність видалення акаунта
                                Console.SetCursorPosition(Config.PosX + 1, Config.FormHeight + 2);
                                Console.Write("Ваш акаунт успішно видалено !");
                                Console.SetCursorPosition(Config.PosX + 1, Config.FormHeight + 3);
                                Console.Write("Натисніть любу клавішу . . .");
                                Console.ReadKey(true);
                                
                                // Видалення поточного акаунта з бази даних
                                db.Accounts.Remove(account);
                                
                                return db.Accounts.First();
                            }
                            
                            if (currentElement == 2)
                            {
                                Console.SetCursorPosition(Config.PosX + 1, Config.FormHeight + 2);
                                Console.Write("Натисніть любу клавішу . . .");
                                Console.ReadKey(true);
                                return account;
                            }

                            break;
                        }
                    }
                    
                    if (currentElement == 1) // Якщо поточний елемент -> перший
                    {
                        // Стираємо виділення з поточного елемента
                        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
                        Console.Write(new string(' ', Config.FormWidth - 2));
                        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 4);
                        Console.Write(new string(' ', Config.FormWidth - 2));
                    }
                    
                    if (currentElement == 2) // Якщо поточний елемент -> другий
                    {
                        // Стираємо виділення з поточного елемента
                        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
                        Console.Write(new string(' ', Config.FormWidth - 2));
                        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 4);
                        Console.Write(new string(' ', Config.FormWidth - 2));
                    }
                    
                    Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 3);
                    Console.Write(Text.AlignCenter("Так", 23));
                
                    Console.SetCursorPosition(Config.PosX + 26, Config.PosY + 3);
                    Console.Write(Text.AlignCenter("Ні", 23));
                }

                break;
            }

            case AccountType.Admin:
            {
                // Ставлю контрольну точку
                DelAccount:
                
                TableGen.DrawFrame(Config.FormWidth, db.Accounts.Count() > Config.MaxElToForm + 2 ? Config.MaxElToForm * 2 + 3 : db.Accounts.Count() * 2 + 3);
                
                // Вивід полів і вибірка елемента
                AnimalYard.PrintItemList(db.Accounts.ToList(), 2, 4, 1, Config.FormWidth - 4);
                var index = TableGen.NavigateFrame(Config.FormWidth - 2, 1, db.Accounts.ToList(), 1, 3) - 1;

                // Якщо вибраний акаунт --> акаунт адміністратора ---> вивести помилку
                if (db.Accounts.ElementAt(index).AccountType == AccountType.Admin)
                {
                    Console.SetCursorPosition(Config.PosX, Config.PosY + Config.MaxElToForm * 2 +  6);
                    Console.Write(Text.Colored("Помилка ! Ви не можете видалити акаунт адміністратора !", Color.Red));
                    
                    Console.SetCursorPosition(Config.PosX, Config.PosY + Config.MaxElToForm * 2 +  7);
                    Console.Write("Натисни любу клавішу . . .");
                    Console.ReadKey(true);
                    
                    goto DelAccount; // Вертаємось до контрольної точки
                }

                db.Accounts.Remove(db.Accounts.ElementAt(index));
                db.SaveChanges();
                
                // Вивід про успішність видалення акаунта
                Console.SetCursorPosition(Config.PosX, Config.PosY + Config.MaxElToForm * 2 +  6);
                Console.Write(Text.Colored("Ви успішно видалили цей акаунт !", Color.Green));
                
                Console.SetCursorPosition(Config.PosX, Config.PosY + Config.MaxElToForm * 2 +  7);
                Console.Write("Натисни любу клавішу . . .");
                Console.ReadKey(true);

                break;
            }
        }

        return account;
    }
}