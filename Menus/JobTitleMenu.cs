namespace Main_Practice.Menus;

using AccessControl;
using Tools;
using Configuration;

public static partial class AnimalYard
{
    private static MenuConst JobTitleMenu(Account account)
    {
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = account.AccountType switch
        {
            AccountType.Admin => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Додати посаду", "\u2795\ud83c\udff7\ufe0f" },
                new[] { "Видалити посаду", "\ud83d\uddd1\ud83c\udff7\ufe0f" },
                new[] { "Переглянути список посад", "\ud83d\udcdc\ud83c\udff7\ufe0f" },
                new[] { "Змінити інформацію про посаду", "\u270f\ufe0f\ud83c\udff7\ufe0f" }
            },

            AccountType.User => new []
            {
                new [] { "Вийти з програми", "\u23fb"},
                new [] { "Переглянути список посад", "\ud83d\udcdc\ud83c\udff7\ufe0f"}
            },
            
            _ => Array.Empty<string[]>()
        };

        TableGen.DrawFrame(Config.FormWidth, menuElement.Length * 2 + 3);
        
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
        Console.Write(Text.AlignCenter("[ ПОСАДИ ]", Config.FormWidth - 2));
        
        // Виводимо пункти меню
        for (int i = 0; i < menuElement.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + Config.FormWidth / 5, Config.PosY + 4 + i * 2);
            Console.Write(menuElement[i][0] + " " + menuElement[i][1]);
        }
        
        // Індекс поточного елемента
        var currentElToMenu = 0;
        
        while (true)
        {
            // Виділення поточного елемента
            TableGen.DrawFrame(Config.FormWidth - 2, 1, 1, 3 + currentElToMenu * 2, false);
            Console.SetCursorPosition(Config.PosX + Config.FormWidth / 5, Config.PosY + 4 + currentElToMenu * 2);
            Console.Write(Text.Colored(menuElement[currentElToMenu][0], Color.Green) + " " + menuElement[currentElToMenu][1]);

            switch (Console.ReadKey(true).Key)
            {
                // Якщо натиснута стрілочка вверх --> стерти виділення та зменшити позицію курсора
                case ConsoleKey.UpArrow:
                    if (currentElToMenu > 0)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 5, Config.PosY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu--;
                    }

                    break;
                
                // Якщо натиснута стрілочка вниз --> стерти виділення та збільшити позицію курсора
                case ConsoleKey.DownArrow:
                    if (currentElToMenu < menuElement.Length - 1)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 5, Config.PosY + 4 + currentElToMenu * 2);
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
                            1 => MenuConst.AddJobTitle,
                            2 => MenuConst.DeleteJobTitle,
                            3 => MenuConst.GetJobTitleList,
                            4 => MenuConst.EditJobTitle,
                            _ => MenuConst.Exit
                        },
                        
                        AccountType.User => currentElToMenu switch
                        {
                            0 => MenuConst.Exit,
                            1 => MenuConst.GetJobTitleList,
                            _ => MenuConst.Exit
                        },
                        
                        _ => MenuConst.Exit
                    };
            }
        }
    }
}
