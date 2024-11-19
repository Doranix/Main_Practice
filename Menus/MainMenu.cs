using Main_Practice.AccessControl;
using Main_Practice.Configuration;
using Main_Practice.Tools;

namespace Main_Practice.Menus;

public static partial class AnimalYard
{
    public static MenuConst MainMenu(Account account)
    {
        // Ставимо контрольну точку
        MainMenu:
        
        // Якщо тип акаунта -> "Гость" - надати йому інтерфейс входу в акаунт
        if (account.AccountType == AccountType.Quest)
            return AccountMenu(account);
        
        TableGen.DrawFrame(Config.FormWidth, 13);
        
        Console.CursorVisible = false;
        
        // Формуємо пункти меню відповідно до типу акаунту
        var menuElement = new[] 
        {
            new [] {"Вийти з програми", "\u23fb"},
            new []{"Тварини", "\ud83d\udc3e"},
            new []{"Продукція", "\ud83e\udd5b"},
            new []{"Робітники", "\ud83d\udc77"},
            new []{"Акаунт", "\ud83d\udc64"}
        };
        
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
        Console.Write(Text.AlignCenter("ГОЛОВНЕ МЕНЮ", Config.FormWidth - 2));
        
        // Виводимо пункти меню
        for (int i = 0; i < menuElement.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + Config.FormWidth / 3, Config.PosY + 4 + i * 2);
            Console.Write(menuElement[i][0] + " " + menuElement[i][1]);
        }
        
        // Індекс поточного елемента
        var currentElToMenu = 0;

        while (true)
        {
            // Виділення поточного елемента
            TableGen.DrawFrame(Config.FormWidth - 2, 1, 1, 3 + currentElToMenu * 2, false);
            Console.SetCursorPosition(Config.PosX + Config.FormWidth / 3, Config.PosY + 4 + currentElToMenu * 2);
            Console.Write(Text.Colored(menuElement[currentElToMenu][0], Color.Green) + " " + menuElement[currentElToMenu][1]);

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    if (currentElToMenu > 0)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 3, Config.PosY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu--;
                    }

                    break;
                
                // Якщо натиснута стрілочка вниз --> стерти виділення та збільшити позицію курсора
                case ConsoleKey.DownArrow:
                    if (currentElToMenu < menuElement.Length - 1)
                    {
                        TableGen.Clear(Config.FormWidth - 2, 3, 1, 3 + currentElToMenu * 2);
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 3, Config.PosY + 4 + currentElToMenu * 2);
                        Console.Write(menuElement[currentElToMenu][0] + " " + menuElement[currentElToMenu][1]);
                
                        currentElToMenu++;
                    }

                    break;
                
                // Якщо натиснута клавіша "Enter" --> Повернути вибране значне відповідно до типу акаунта
                case ConsoleKey.Enter:
                    switch (currentElToMenu)
                    {
                        case 0: return MenuConst.Exit;
                        case 1:
                        {
                            var command = AnimalMenu(account);   // Викликаємо під-меню для тварин
                            if (command == MenuConst.Exit) goto MainMenu; // Якщо вибраний пункт - "Назад" - повертаємось до контрольної точки
                            return command;
                        }
                        case 2:
                        {
                            var command = ProductMenu(account);  // Викликаємо під-меню для продукції
                            if (command == MenuConst.Exit) goto MainMenu; // Якщо вибраний пункт - "Назад" - повертаємось до контрольної точки
                            return command;
                        }
                        case 3:
                        {
                            var command = WorkerMenu(account);   // Викликаємо під-меню для робітників
                            if (command == MenuConst.Exit) goto MainMenu; // Якщо вибраний пункт - "Назад" - повертаємось до контрольної точки
                            return command;
                        }
                        case 4:
                        {
                            var command = AccountMenu(account);  // Викликаємо під-меню для акаунта
                            if (command == MenuConst.Exit) goto MainMenu; // Якщо вибраний пункт - "Назад" - повертаємось до контрольної точки
                            return command;
                        }
                    }

                    break;
            }
        }
    }
}
