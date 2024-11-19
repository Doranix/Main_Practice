namespace Main_Practice.Menus;

using AccessControl;
using Tools;
using Configuration;

public static partial class AnimalYard
{
    private static MenuConst AccountMenu(Account account)
    {
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = account.AccountType switch
        {
            AccountType.Quest => new[]
            {
                new [] { "Вийти із програми", "\u23fb" },
                new [] { "Увійти", "\u27a1\ufe0f\ud83d\udc64" },
                new [] { "Зареєструватись", "\u2795\ud83d\udc64" }
            },
            
            AccountType.User => new[]
            {
                new [] { "Повернутись назад", "\ud83d\udd19" },
                new [] { "Увійти в інший аккаунт", "\ud83d\udd04\ud83d\udc64" },
                new [] { "Зареєструвати новий акаунт", "\u2795\ud83d\udc64" },
                new [] { "Отримати інформацію про акаунт", "\ud83d\udcc4\ud83d\udc64" },
                new [] { "Змінити дані акаунта", "\u270f\ufe0f\ud83d\udc64" },
                new [] { "Видалити акаунт", "\ud83d\uddd1\ud83d\udc64" }
            },
            
            AccountType.Admin => new[]
            {
                new [] { "Повернутись назад", "\ud83d\udd19" },
                new [] { "Увійти в інший аккаунт", "\ud83d\udd04\ud83d\udc64" },
                new [] { "Зареєструвати новий акаунт", "\u2795\ud83d\udc64" },
                new [] { "Отримати інформацію про акаунт", "\ud83d\udcc4\ud83d\udc64" },
                new [] { "Змінити дані акаунта", "\u270f\ufe0f\ud83d\udc64" },
                new [] { "Отримати список акаунтів у базі", "\ud83d\udcdc\ud83d\udc64" },
                new [] { "Видалити акаунт", "\ud83d\uddd1\ud83d\udc64" }
            },
            
            _ => Array.Empty<string[]>()
        };
        
        TableGen.DrawFrame(Config.FormWidth, menuElement.Length * 2 + 3);
        
        // Виводимо заголовок
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
        Console.Write(Text.AlignCenter(account.Info, Config.FormWidth - 2));
        
        // Виводимо пункти меню
        for (int i = 0; i < menuElement.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4 + i * 2);
            Console.Write(menuElement[i][0] + " " + menuElement[i][1]);
        }
        
        // Індекс поточного елемента
        var currentElToMenu = 0;
        
        while (true)
        {
            // Виділення поточного елемента
            TableGen.DrawFrame(Config.FormWidth - 2, 1, 1, 3 + currentElToMenu * 2, false);
            Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4 + currentElToMenu * 2);
            Console.Write(Text.Colored(menuElement[currentElToMenu][0], Color.Green) + " " + menuElement[currentElToMenu][1]);

            switch (Console.ReadKey(true).Key)
            {
                // Якщо натиснута стрілочка вверх --> стерти виділення та зменшити позицію курсора
                case ConsoleKey.UpArrow:
                    if (currentElToMenu > 0)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu--;
                    }

                    break;
                
                // Якщо натиснута стрілочка вниз --> стерти виділення та зменшити позицію курсора
                case ConsoleKey.DownArrow:
                    if (currentElToMenu < menuElement.Length - 1)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu++;
                    }

                    break;
                
                // Якщо натиснута клавіша "Enter" --> Повернути вибране значне відповідно до типу акаунта
                case ConsoleKey.Enter:
                    return account.AccountType switch
                    {
                        AccountType.Admin => currentElToMenu switch
                        {
                            0 => MenuConst.Exit,
                            1 => MenuConst.Login,
                            2 => MenuConst.Register,
                            3 => MenuConst.GetAccountInfo,
                            4 => MenuConst.EditAccountInfo,
                            5 => MenuConst.GetAccountList,
                            6 => MenuConst.DeleteAccount,
                            _ => MenuConst.Exit
                        },

                        AccountType.User => currentElToMenu switch
                        {
                            0 => MenuConst.Exit,
                            1 => MenuConst.Login,
                            2 => MenuConst.Register,
                            3 => MenuConst.GetAccountInfo,
                            4 => MenuConst.EditAccountInfo,
                            5 => MenuConst.DeleteAccount,
                            _ => MenuConst.Exit
                        },

                        AccountType.Quest => currentElToMenu switch
                        {
                            0 => MenuConst.Exit,
                            1 => MenuConst.Login,
                            2 => MenuConst.Register,
                            _ => MenuConst.Exit
                        },
                        
                        _ => MenuConst.Exit
                    };
            }
        }
    }
}
