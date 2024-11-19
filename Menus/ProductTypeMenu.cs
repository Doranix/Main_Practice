namespace Main_Practice.Menus;

using AccessControl;
using Tools;
using Configuration;

public static partial class AnimalYard
{
    private static MenuConst ProductTypeMenu(Account account)
    {
        TableGen.DrawFrame(Config.FormWidth, 15);
        
        Console.CursorVisible = false;
        
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = account.AccountType switch
        {
            AccountType.Admin => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Додати тип продукції", "\u2795\ud83d\udce6" },
                new[] { "Видалити тип продукції", "\ud83d\uddd1\ud83d\udce6" },
                new[] { "Переглянути список типів продукції", "\ud83d\udcdc\ud83d\udce6" },
                new[] { "Змінити інформацію про тип продукції", "\u270f\ufe0f\ud83d\udce6" }
            },
            
            AccountType.User => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Переглянути список типів продукції", "\ud83d\udcdc\ud83d\udce6" }
            },
            
            _ => Array.Empty<string[]>()
        };
        
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
        Console.Write(Text.AlignCenter("[ ТИП ПРОДУКЦІЇ ]", Config.FormWidth - 2));
        
        // Виводимо пункти меню
        for (int i = 0; i < menuElement.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + Config.FormWidth / 8, Config.PosY + 4 + i * 2);
            Console.Write(menuElement[i][0] + " " + menuElement[i][1]);
        }
        
        // Індекс поточного елемента
        var currentElToMenu = 0;

        while (true)
        {
            // Виділення поточного елемента
            TableGen.DrawFrame(Config.FormWidth - 2, 1, 1, 3 + currentElToMenu * 2, false);
            Console.SetCursorPosition(Config.PosX + Config.FormWidth / 8, Config.PosY + 4 + currentElToMenu * 2);
            Console.Write(Text.Colored(menuElement[currentElToMenu][0], Color.Green) + " " + menuElement[currentElToMenu][1]);

            switch (Console.ReadKey(true).Key)
            {
                // Якщо натиснута стрілочка вверх --> стерти виділення та зменшити позицію курсора
                case ConsoleKey.UpArrow:
                    if (currentElToMenu > 0)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 8, Config.PosY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu--;
                    }

                    break;
                
                // Якщо натиснута стрілочка вниз --> стерти виділення та збільшити позицію курсора
                case ConsoleKey.DownArrow:
                    if (currentElToMenu < menuElement.Length - 1)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 8, Config.PosY + 4 + currentElToMenu * 2);
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
                            1 => MenuConst.AddProductType,
                            2 => MenuConst.DeleteProductType,
                            3 => MenuConst.GetProductTypeList,
                            4 => MenuConst.EditProductType,
                            _ => MenuConst.Exit
                        },
                        
                        AccountType.User => currentElToMenu switch
                        {
                            0 => MenuConst.Exit,
                            1 => MenuConst.GetProductTypeList,
                            _ => MenuConst.Exit
                        },
                        
                        _ => MenuConst.Exit
                    };
            }
        }
    }
}
