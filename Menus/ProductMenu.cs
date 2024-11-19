namespace Main_Practice.Menus;

using Tools;
using AccessControl;
using Configuration;

public static partial class AnimalYard
{
    private static MenuConst ProductMenu(Account account)
    {
        // Ставимо контрольну точку
        ProductMenu:
        
        TableGen.DrawFrame(Config.FormWidth, 17);
        
        Console.CursorVisible = false;
        
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = account.AccountType switch
        {
            AccountType.Admin => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Додати продукцію", "\u2795\ud83e\udd5b" },
                new[] { "Видалити продукцію", "\ud83d\uddd1\ud83e\udd5b" },
                new[] { "Переглянути список продукції", "\ud83d\udcdc\ud83e\udd5b" },
                new[] { "Змінити інформацію про продукцію", "\u270f\ufe0f\ud83e\udd5b" },
                new[] { "Типи продукції", "\ud83d\udce6" }
            },
            
            AccountType.User => new []
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Переглянути список продукції", "\ud83d\udcdc\ud83e\udd5b" },
                new[] { "Типи продукції", "\ud83d\udce6" }
            },
            
            _ => Array.Empty<string[]>()
        };
        
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
        Console.Write(Text.AlignCenter("[ ПРОДУКЦІЯ ]", Config.FormWidth - 2));
        
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
                    switch (account.AccountType)
                    {
                        case AccountType.Admin:
                            switch (currentElToMenu)
                            {
                                case 0: return MenuConst.Exit;
                                case 1: return MenuConst.AddProduct;
                                case 2: return MenuConst.DeleteProduct;
                                case 3: return MenuConst.GetProductList;
                                case 4: return MenuConst.EditProduct;
                                case 5:
                                {
                                    var command = ProductTypeMenu(account);
                                    if (command == MenuConst.Exit) goto ProductMenu;
                                    return command;
                                }
                            }

                            break;
                    
                        case AccountType.User:
                            switch (currentElToMenu)
                            {
                                case 0: return MenuConst.Exit;
                                case 1: return MenuConst.GetProductList;
                                case 2:
                                {
                                    var command = ProductTypeMenu(account);
                                    if (command == MenuConst.Exit) goto ProductMenu;
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
