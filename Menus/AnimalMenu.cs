namespace Main_Practice.Menus;

using Tools;
using AccessControl;
using Configuration;

public static partial class AnimalYard
{
    private static MenuConst AnimalMenu(Account account)
    {
        // Ставимо контрольну точку
        AnimalMenu:

        var width = Config.FormWidth + Config.FormWidth / 2 + 10;
        var posX = -(Config.FormWidth / 4);
        var posY = -Config.PosY + 1;
        
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = account.AccountType switch
        {
            AccountType.Admin => new[]
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Додати тварину", "\u2795\ud83d\udc3e" },
                new[] { "Видалити тварину", "\ud83d\uddd1\ud83d\udc3e" },
                new[] { "Переглянути список тварин", "\ud83d\udcdc\ud83d\udc3e" },
                new[] { "Змінити інформацію про тварину", "\u270f\ufe0f\ud83d\udc3e" },
                new[] { "Отримати тварин та їх продукцію", "\ud83d\udc3e\ud83e\udd5b" },
                new[] { "Отримати тварин за раціоном", "\ud83c\udf7d\ud83d\udc3e" },
                new[] { "Отримати тварин з більшою продуктивністю", "\ud83d\udcc8\ud83d\udc3e" },
                new[] { "Отримати тварину з максимальною вагою", "\u2696\ufe0f\ud83d\udc3e" },
                new[] { "Отримати тварин без робітників", "\ud83d\udeab\ud83d\udc77\ud83d\udc3e" },
                new[] { "Отримати тварин без продукції", "\ud83d\udeab\ud83e\udd5b" },
                new[] { "Скопіювати до новї таблиці тварин із певною продукцією", "\ud83d\udccb\u2795\ud83d\udc3e" },
                new[] { "Назви тварин", "\ud83d\udcdd\ud83d\udc3e" }
            },
            
            AccountType.User => new []
            {
                new[] { "Повернутись назад", "\ud83d\udd19" },
                new[] { "Переглянути список тварин", "\ud83d\udcdc\ud83d\udc3e" },
                new[] { "Отримати тварин та їх продукцію", "\ud83d\udc3e\ud83e\udd5b" },
                new[] { "Отримати тварин за раціоном", "\ud83c\udf7d\ud83d\udc3e" },
                new[] { "Отримати тварин з більшою продуктивністю", "\ud83d\udcc8\ud83d\udc3e" },
                new[] { "Отримати тварину з максимальною вагою", "\u2696\ufe0f\ud83d\udc3e" },
                new[] { "Назви тварин", "\ud83d\udcdd\ud83d\udc3e" }
            },
            
            _ => Array.Empty<string[]>()
        };
        
        TableGen.DrawFrame(width, menuElement.Length * 2 + 3, posX, posY);
        
        Console.SetCursorPosition(Config.PosX + posX + 1, Config.PosY + posY + 2);
        Console.Write(Text.AlignCenter("[ ТВАРИНИ ]", width - 2));
        
        // Виводимо пункти меню
        for (int i = 0; i < menuElement.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + i * 2);
            Console.Write(menuElement[i][0] + " " + menuElement[i][1]);
        }
        
        // Індекс поточного елемента
        var currentElToMenu = 0;

        while (true)
        {
            // Виділення поточного елемента
            TableGen.DrawFrame(width - 2, 1, posX + 1, posY + 3 + currentElToMenu * 2, false);
            Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + currentElToMenu * 2);
            Console.Write(Text.Colored(menuElement[currentElToMenu][0], Color.Green) + " " + menuElement[currentElToMenu][1]);

            switch (Console.ReadKey(true).Key)
            {
                // Якщо натиснута стрілочка вверх --> стерти виділення та зменшити позицію курсора
                case ConsoleKey.UpArrow:
                    if (currentElToMenu > 0)
                    {
                        TableGen.Clear(width - 2, 3, posX + 1, posY + 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu--;
                    }

                    break;
                
                // Якщо натиснута стрілочка вниз --> стерти виділення та зменшити позицію курсора
                case ConsoleKey.DownArrow:
                    if (currentElToMenu < menuElement.Length - 1)
                    {
                        TableGen.Clear(width - 2, 3, posX + 1, posY + 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu++;
                    }

                    break;
                
                // Якщо натиснута клавіша "Enter" --> Повернути вибране значне відповідно до типу акаунта
                case ConsoleKey.Enter:
                    switch (account.AccountType)
                    {
                        case AccountType.Admin:
                        {
                            switch (currentElToMenu)
                            {
                                case 0: return MenuConst.Exit;
                                case 1: return MenuConst.AddAnimal;
                                case 2: return MenuConst.DeleteAnimal;
                                case 3: return MenuConst.GetAnimalList;
                                case 4: return MenuConst.EditAnimal;
                                case 5: return MenuConst.GetAnimalsWithProducts;
                                case 6: return MenuConst.GetAnimalsWithRation;
                                case 7: return MenuConst.GetProductivityAnimals;
                                case 8: return MenuConst.GetAnimalWithMaxWeight;
                                case 9: return MenuConst.GetAnimalWithoutWorker;
                                case 10: return MenuConst.GetAnimalWithoutProduct;
                                case 11: return MenuConst.CopyAnimalData;
                                case 12:
                                {
                                    var command = AnimalNameMenu(account);
                                    if (command == MenuConst.Exit) goto AnimalMenu;
                                    return command;
                                }
                            }

                            break;
                        }

                        case AccountType.User:
                        {
                            switch (currentElToMenu)
                            {
                                case 0: return MenuConst.Exit;
                                case 1: return MenuConst.GetAnimalList;
                                case 2: return MenuConst.GetAnimalsWithProducts;
                                case 3: return MenuConst.GetAnimalsWithRation;
                                case 4: return MenuConst.GetProductivityAnimals;
                                case 5: return MenuConst.GetAnimalWithMaxWeight;
                                case 6:
                                {
                                    var command = AnimalNameMenu(account);
                                    if (command == MenuConst.Exit) goto AnimalMenu;
                                    return command;
                                }
                            }

                            break;
                        }
                    }

                    break;
            }
        }
    }
}
