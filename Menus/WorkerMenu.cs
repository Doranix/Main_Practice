namespace Main_Practice.Menus;

using AccessControl;
using Tools;
using Configuration;

public static partial class AnimalYard
{
    private static MenuConst WorkerMenu(Account account)
    {
        // Ставимо контрольну точку
        WorkerMenu:
        
        Console.CursorVisible = false;
        
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = account.AccountType switch
        {
            AccountType.Admin => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Додати робітника", "\u2795\ud83d\udc77" },
                new[] { "Видалити робітника", "\ud83d\uddd1\ud83d\udc77" },
                new[] { "Переглянути список робітників", "\ud83d\udcdc\ud83d\udc77" },
                new[] { "Змінити інформацію про робітника", "\u270f\ufe0f\ud83d\udc77" },
                new[] { "Отримати список робітників з певним стажем", "\ud83d\udcc5\ud83d\udc77" },
                new[] { "Посади", "\ud83c\udff7\ufe0f" }
            },

            AccountType.User => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Переглянути список робітників", "\ud83d\udcdc\ud83d\udc77" },
                new[] { "Отримати список робітників з певним стажем", "\ud83d\udcc5\ud83d\udc77" },
                new[] { "Посади", "\ud83c\udff7\ufe0f" }
            },

            _ => Array.Empty<string[]>()
        };
        
        TableGen.DrawFrame(Config.FormWidth, menuElement.Length * 2 + 3);
        
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
        Console.Write(Text.AlignCenter("[ РОБІТНИКИ ]", Config.FormWidth - 2));
        
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
                
                // Якщо натиснута стрілочка вниз --> стерти виділення та збільшити позицію курсора
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
                    switch (account.AccountType)
                    {
                        case AccountType.Admin:
                            switch (currentElToMenu)
                            {
                                case 0: return MenuConst.Exit;
                                case 1: return MenuConst.AddWorker;
                                case 2: return MenuConst.DeleteWorker;
                                case 3: return MenuConst.GetWorkerList;
                                case 4: return MenuConst.EditWorker;
                                case 5: return MenuConst.GetWorkersByExperience;
                                case 6:
                                {
                                    var command = JobTitleMenu(account);
                                    if (command == MenuConst.Exit) goto WorkerMenu;
                                    return command;
                                }
                            }

                            break;
                    
                        case AccountType.User:
                            switch (currentElToMenu)
                            {
                                case 0: return MenuConst.Exit;
                                case 1: return MenuConst.GetWorkerList;
                                case 2: return MenuConst.GetWorkersByExperience;
                                case 3:
                                {
                                    var command = JobTitleMenu(account);
                                    if (command == MenuConst.Exit) goto WorkerMenu;
                                    return command;
                                }
                            }

                            break;
                    }

                    break;
            }
        }
    }
}
