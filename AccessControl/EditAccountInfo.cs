using Main_Practice.Menus;

namespace Main_Practice.AccessControl;

using Tools;
using DATABASE;
using Configuration;

public static partial class Security
{
    // Змінити дані про акаунт
    public static (Account, bool) EditAccountInfo(Account account)
    {
        using var db = new DbController();

        // Тимчасові змінні для обробки введення команди
        string? value;
        string? command;

        switch (account.AccountType)
        {
            case AccountType.User:
            {
                TableGen.DrawFrame(Config.FormWidth, Config.FormHeight - 1);

                // Малювання назви форми
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
                Console.Write(Text.AlignCenter($"[ {Text.Bold("Які дані ви бажаєте змінити ?")} ]",
                    Config.FormWidth + 6));

                // Перше поле
                Console.SetCursorPosition(Config.PosX + 1, Console.CursorTop + 2);
                Console.Write(Text.AlignCenter("Ім'я", Config.FormWidth - 2));

                // Друге поле
                Console.SetCursorPosition(Config.PosX + 1, Console.CursorTop + 2);
                Console.Write(Text.AlignCenter("Пароль", Config.FormWidth - 2));

                // Третє поле
                Console.SetCursorPosition(Config.PosX + 1, Console.CursorTop + 2);
                Console.Write(Text.AlignCenter("Ім'я та пароль", Config.FormWidth - 2));

                // Малювання рамки на початковій позиції
                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 3, false);

                // Встановлення курсора на початкову позицію
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 4);

                // Початкове виділення поля
                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 4);
                Console.Write(Text.AlignCenter(Text.Colored("Ім'я", Color.Green),
                    Config.FormWidth - 6 + Text.ColoredDiff("Ім'я", Color.Green)));

                Console.CursorVisible = false;

                // Переміщення рамки по варіантах
                while (true)
                {
                    var keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                        {
                            // Якщо поточний елемент - середній --> перемістити вказівник до першого елемента
                            if (Console.CursorTop == Config.PosY + 6)
                            {
                                // Стирання проміжних ліній, незалежно від того, де рамка (вище чи нище)
                                for (int posY = 3; posY <= 9; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Малювання рамки на елементі вище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 3, false);

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter("Пароль", Config.FormWidth - 6));

                                // Виділення тексту зеленим
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 4);
                                Console.Write(Text.AlignCenter(Text.Colored("Ім'я", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Ім'я", Color.Green)));

                                // Переміщення курсора на позицію верхнього від нього елемента
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 4);
                            }

                            // Якщо поточний елемент - третій --> Перемістити вказівник до середнього елемента
                            else if (Console.CursorTop == Config.PosY + 8)
                            {
                                // Стирання ліній навколо поточного елемента
                                for (int posY = 7; posY <= 9; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 8);
                                Console.Write(Text.AlignCenter("Ім'я та пароль", Config.FormWidth - 6));

                                // Виділення елемента вище
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter(Text.Colored("Пароль", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Пароль", Color.Green)));

                                // Малювання рамки на елементі вище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 5, false);

                                // Переміщення курсора на позицію верхнього від нього елемента
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                            }

                            break;
                        }

                        case ConsoleKey.DownArrow:
                        {
                            // Якщо поточний елемент - середній --> перемістити вказівник до останнього елемента
                            if (Console.CursorTop == Config.PosY + 6)
                            {
                                // Стирання проміжних ліній, незалежно від того, де рамка (вище чи нище)
                                for (int posY = 3; posY <= 9; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter("Пароль", Config.FormWidth - 6));

                                // Виділення елемента нище
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 8);
                                Console.Write(Text.AlignCenter(Text.Colored("Ім'я та пароль", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Ім'я та пароль", Color.Green)));

                                // Малювання рамки на елементі нище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 7, false);

                                // Переміщення курсора на позицію нижнього від нього елемента
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 8);
                            }

                            // Якщо поточний елемент - перший --> Перемістити вказівник до середнього елемента
                            if (Console.CursorTop == Config.PosY + 4)
                            {
                                // Стирання ліній навколо поточного елемента
                                for (int posY = 3; posY <= 5; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 4);
                                Console.Write(Text.AlignCenter("Ім'я", Config.FormWidth - 6));

                                // Виділення елемента нище
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter(Text.Colored("Пароль", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Пароль", Color.Green)));

                                // Малювання рамки на елементі нище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 5, false);

                                // Переміщення вказівник до елемента нище
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                            }

                            break;
                        }

                        // Якщо натиснута клавіша "Enter" - Обрати поточний елемент
                        case ConsoleKey.Enter:
                        {
                            // Якщо поточний елемент - Ім'я
                            if (Console.CursorTop == Config.PosY + 4)
                            {
                                // Оприділяємо ширину форми
                                var width = "Ім'я: ".Length + Config.MaxLength + 4;

                                // Ширина рамки = Довжина поля з пробілом в началі + Максимальна довжина вводимого значення
                                TableGen.DrawFrame(width + 2, 4);

                                // Малювання заголовка форми + Центрування в середині формі
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                                Console.Write(Text.AlignCenter(Text.Colored("Зміна імені", Color.Green),
                                    width - 2 + Text.ColoredDiff("Зміна імені", Color.Green)));

                                // Зміщаємо курсор нище і відмальовуємо саме поле
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Ім'я: ");

                                // Малюємо лінію нище від поля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write(TableGen.Line(width - 2));

                                Console.CursorVisible = true;

                                // Повертаємо курсор до поля вводу і даємо запит на ввід імені
                                Console.SetCursorPosition(Config.PosX + 8, Console.CursorTop - 1);
                                (value, command) = Input.ReadStringValue();
                                if (command == "Exit") return (account, false);
                                account.Username = value ?? "None";

                                Console.CursorVisible = false;

                                // Стираємо лінію після вводу імені
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write(new string(' ', width - 2));

                                // Переміщуємо курсор на позицію нижче форми й виводимо повідомлення про успішність зміни імені
                                Console.SetCursorPosition(Config.PosX, Config.PosY + 8);
                                Console.Write(Text.AlignCenter(
                                    Text.Colored("Ім'я акаунта успішно змінено !", Color.Green),
                                    width + 2 + Text.ColoredDiff("Ім'я акаунта успішно змінено !", Color.Green)));

                                Console.SetCursorPosition(Config.PosX, Config.PosY + 9);
                                Console.Write("Натисність любу клавішу. . .");
                                Console.ReadKey(true);

                                return (account, true);
                            }

                            // Якщо поточний елемент - Пароль
                            if (Console.CursorTop == Config.PosY + 6)
                            {
                                // Оприділяємо ширину форми
                                var width = "Старий пароль: ".Length + Config.MaxLength + 4;

                                // Ширина рамки = Довжина поля з пробілом в началі + Максимальна довжина вводимого значення
                                TableGen.DrawFrame(width + 2, 6);

                                // Малювання заголовка форми + Центрування в середині формі
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                                Console.Write(Text.AlignCenter(Text.Colored("Зміна пароля", Color.Green),
                                    width - 2 + Text.ColoredDiff("Зміна пароля", Color.Green)));

                                // Зміщаємо курсор нище і відмальовуємо поле для вводу старого паролю
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Старий пароль: ");

                                // Зміщаємо курсор нище і відмальовуємо поле для вводу нового пароля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Новий пароль: ");

                                // Малюємо лінію нище від поля вводу старого пароля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop - 1);
                                Console.Write(TableGen.Line(width - 2));

                                Console.CursorVisible = true;

                                // Зациклюємо ввід старого пароля
                                while (true)
                                {
                                    // Повертаємо курсор до поля вводу і даємо запит на ввід старого пароля
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 3);
                                    (value, command) = Input.ReadPassword();
                                    if (command == "Exit") return (account, false);
                                    var oldPassword = value ?? "None";

                                    // Якщо введений пароль правильний
                                    if (account.ValidatePassword(oldPassword))
                                    {
                                        // Стираємо лінію під старим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4);
                                        Console.Write(new string(' ', width - 2));

                                        // Малюємо лінію під новим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                        Console.Write(TableGen.Line(width - 2));

                                        // Переміщуємо курсор до поля введення нового пароля
                                        Console.SetCursorPosition(Config.PosX + 16, Console.CursorTop - 1);
                                        break;
                                    }

                                    // Якщо введений пароль неправильний
                                    // Стираємо введений пароль
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 3);
                                    Console.Write(new string(' ', Config.MaxLength));

                                    // Перекрашуємо лінію у червоний колір
                                    Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                    Console.Write(Text.Colored(TableGen.Line(width - 2), Color.Red));
                                }

                                // Вводимо новий пароль (з перевіркою на команду)
                                (value, command) = Input.ReadPassword();
                                if (command == "Exit") return (account, false);
                                account.Password = value ?? "None";

                                Console.CursorVisible = false;

                                // Стираємо лінію після вводу пароля
                                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 6);
                                Console.Write(new string(' ', width - 2));

                                // Переміщуємо курсор на позицію нижче форми й виводимо повідомлення про успішність зміни пароля
                                Console.SetCursorPosition(Config.PosX, Config.PosY + 8);
                                Console.Write(Text.AlignCenter(
                                    Text.Colored("Пароль акаунта успішно змінено !", Color.Green),
                                    width + 2 + Text.ColoredDiff("Пароль акаунта успішно змінено !", Color.Green)));

                                Console.SetCursorPosition(Config.PosX, Config.PosY + 9);
                                Console.Write("Натисність любу клавішу. . .");
                                Console.ReadKey(true);

                                return (account, true);
                            }

                            // Якщо поточний елемент - Ім'я та пароль
                            if (Console.CursorTop == Config.PosY + 8)
                            {
                                // Оприділяємо ширину форми - Слово "Старий пароль" довше, тому відштовхуємось від нього
                                var width = "Старий пароль: ".Length + Config.MaxLength + 4;

                                // Малюємо рамку
                                TableGen.DrawFrame(width + 2, 8);

                                // Виводим заголовок
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                                Console.Write(Text.AlignCenter(Text.Colored(account.Info, Color.Green),
                                    width - 2 + Text.ColoredDiff(account.Info, Color.Green)));

                                // Виводимо необхідні поля й малюємо лінію на початковій позиції
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Ім'я: ");
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write(TableGen.Line(width - 2));
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write("Старий пароль: ");
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Новий пароль: ");

                                Console.CursorVisible = true;

                                // Вводимо нове ім'я (з перевіркою команди)
                                Console.SetCursorPosition(Config.PosX + 8, Console.CursorTop - 4);
                                (value, command) = Input.ReadStringValue();
                                if (command == "Exit") return (account, false);
                                account.Username = value ?? "None";

                                // Стираємо лінію під полем вводу імені
                                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4);
                                Console.Write(new string(' ', width - 2));

                                // Малюємо лінію під полем вводу старого пароля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write(TableGen.Line(width - 2));

                                // Зациклюємо введення старого паролю
                                while (true)
                                {
                                    // Повертаємо курсор до поля вводу і даємо запит на ввід старого пароля
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 5);
                                    (value, command) = Input.ReadPassword();
                                    if (command == "Exit") return (account, false);
                                    var oldPassword = value ?? "None";

                                    // Якщо введений пароль правильний
                                    if (account.ValidatePassword(oldPassword))
                                    {
                                        // Стираємо лінію під старим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 6);
                                        Console.Write(new string(' ', width - 2));

                                        // Малюємо лінію під новим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                        Console.Write(TableGen.Line(width - 2));

                                        // Переміщуємо курсор до поля введення нового пароля
                                        Console.SetCursorPosition(Config.PosX + 16, Console.CursorTop - 1);
                                        break;
                                    }

                                    // Якщо введений пароль неправильний
                                    // Стираємо введений пароль
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 5);
                                    Console.Write(new string(' ', Config.MaxLength));

                                    // Перекрашуємо лінію у червоний колір
                                    Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                    Console.Write(Text.Colored(TableGen.Line(width - 2), Color.Red));
                                }

                                // Вводимо новий пароль (з перевіркою на команду)
                                (value, command) = Input.ReadPassword();
                                if (command == "Exit") return (account, false);
                                account.Password = value ?? "None";

                                Console.CursorVisible = false;

                                // Стираємо лінію після вводу пароля
                                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 8);
                                Console.Write(new string(' ', width - 2));

                                // Переміщуємо курсор на позицію нижче форми й виводимо повідомлення про успішність зміни пароля
                                Console.SetCursorPosition(Config.PosX, Config.PosY + 12);
                                Console.Write(Text.AlignCenter(
                                    Text.Colored("Пароль акаунта успішно змінено !", Color.Green),
                                    width + 2 + Text.ColoredDiff("Пароль акаунта успішно змінено !", Color.Green)));

                                Console.SetCursorPosition(Config.PosX, Config.PosY + 13);
                                Console.Write("Натисність любу клавішу. . .");
                                Console.ReadKey(true);

                                return (account, true);
                            }

                            break;
                        }
                    }
                }
            }

            case AccountType.Admin:
            {
                // Визначаємо необхідну ширину і висоту рамки - щоб вмістити у неї всі елементи
                var width = DbController.GetMaxLength(db.Accounts.ToList()) + 4;
                var height = Config.MaxElToForm * 2 + 3;

                // Малюємо рамку
                TableGen.DrawFrame(width, height);

                // Виводим заголовок
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                Console.Write(Text.AlignCenter("<< Наявні  акаунти >>", width - 2));

                // Виводимо всередині список акаунтів
                AnimalYard.PrintItemList(db.Accounts.ToList(), 2, 4, 1, width - 4);

                // Викликаємо метод для вибірки елемента, при цьому зберігаючи індекс вибраного елемента
                var index = TableGen.NavigateFrame(width - 2, 1, db.Accounts.ToList(), 1, 3) - 1;

                // Для оптимізації нашої програми зразу витягнемо обраний акаунт із бази даних
                var accountMod = db.Accounts.ElementAt(index); // Account Modified - Змінений акаунт

                // Далі прописуємо те ж саме, що прописували для "User"

                TableGen.DrawFrame(Config.FormWidth, Config.FormHeight - 1);

                // Малювання назви форми
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
                Console.Write(Text.AlignCenter($"[ {Text.Bold("Які дані ви бажаєте змінити ?")} ]",
                    Config.FormWidth + 6));

                // Перше поле
                Console.SetCursorPosition(Config.PosX + 1, Console.CursorTop + 2);
                Console.Write(Text.AlignCenter("Ім'я", Config.FormWidth - 2));

                // Друге поле
                Console.SetCursorPosition(Config.PosX + 1, Console.CursorTop + 2);
                Console.Write(Text.AlignCenter("Пароль", Config.FormWidth - 2));

                // Третє поле
                Console.SetCursorPosition(Config.PosX + 1, Console.CursorTop + 2);
                Console.Write(Text.AlignCenter("Ім'я та пароль", Config.FormWidth - 2));

                // Малювання рамки на початковій позиції
                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 3, false);

                // Встановлення курсора на початкову позицію
                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 4);

                // Початкове виділення поля
                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 4);
                Console.Write(Text.AlignCenter(Text.Colored("Ім'я", Color.Green),
                    Config.FormWidth - 6 + Text.ColoredDiff("Ім'я", Color.Green)));

                Console.CursorVisible = false;

                // Переміщення рамки по варіантах
                while (true)
                {
                    var keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                        {
                            // Якщо поточний елемент - середній --> перемістити вказівник до першого елемента
                            if (Console.CursorTop == Config.PosY + 6)
                            {
                                // Стирання проміжних ліній, незалежно від того, де рамка (вище чи нище)
                                for (int posY = 3; posY <= 9; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Малювання рамки на елементі вище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 3, false);

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter("Пароль", Config.FormWidth - 6));

                                // Виділення тексту зеленим
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 4);
                                Console.Write(Text.AlignCenter(Text.Colored("Ім'я", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Ім'я", Color.Green)));

                                // Переміщення курсора на позицію верхнього від нього елемента
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 4);
                            }

                            // Якщо поточний елемент - третій --> Перемістити вказівник до середнього елемента
                            else if (Console.CursorTop == Config.PosY + 8)
                            {
                                // Стирання ліній навколо поточного елемента
                                for (int posY = 7; posY <= 9; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 8);
                                Console.Write(Text.AlignCenter("Ім'я та пароль", Config.FormWidth - 6));

                                // Виділення елемента вище
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter(Text.Colored("Пароль", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Пароль", Color.Green)));

                                // Малювання рамки на елементі вище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 5, false);

                                // Переміщення курсора на позицію верхнього від нього елемента
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                            }

                            break;
                        }

                        case ConsoleKey.DownArrow:
                        {
                            // Якщо поточний елемент - середній --> перемістити вказівник до останнього елемента
                            if (Console.CursorTop == Config.PosY + 6)
                            {
                                // Стирання проміжних ліній, незалежно від того, де рамка (вище чи нище)
                                for (int posY = 3; posY <= 9; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter("Пароль", Config.FormWidth - 6));

                                // Виділення елемента нище
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 8);
                                Console.Write(Text.AlignCenter(Text.Colored("Ім'я та пароль", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Ім'я та пароль", Color.Green)));

                                // Малювання рамки на елементі нище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 7, false);

                                // Переміщення курсора на позицію нижнього від нього елемента
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 8);
                            }

                            // Якщо поточний елемент - перший --> Перемістити вказівник до середнього елемента
                            if (Console.CursorTop == Config.PosY + 4)
                            {
                                // Стирання ліній навколо поточного елемента
                                for (int posY = 3; posY <= 5; posY += 2)
                                {
                                    Console.SetCursorPosition(Config.PosX + 1, Config.PosY + posY);
                                    Console.Write(new string(' ', Config.FormWidth - 2));
                                }

                                // Зняття виділення з поточного елемента
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 4);
                                Console.Write(Text.AlignCenter("Ім'я", Config.FormWidth - 6));

                                // Виділення елемента нище
                                Console.SetCursorPosition(Config.PosX + 3, Config.PosY + 6);
                                Console.Write(Text.AlignCenter(Text.Colored("Пароль", Color.Green),
                                    Config.FormWidth - 6 + Text.ColoredDiff("Пароль", Color.Green)));

                                // Малювання рамки на елементі нище
                                TableGen.DrawFrame(Config.FormWidth - 4, 1, 2, 5, false);

                                // Переміщення вказівник до елемента нище
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 6);
                            }

                            break;
                        }

                        // Якщо натиснута клавіша "Enter" - Обрати поточний елемент
                        case ConsoleKey.Enter:
                        {
                            // Якщо поточний елемент - Ім'я
                            if (Console.CursorTop == Config.PosY + 4)
                            {
                                // Оприділяємо ширину форми
                                width = "Нове ім'я: ".Length + Config.MaxLength + 4;

                                // Ширина рамки = Довжина поля з пробілом в началі + Максимальна довжина вводимого значення
                                TableGen.DrawFrame(width + 2, 4);

                                // Малювання заголовка форми + Центрування в середині формі
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                                Console.Write(Text.AlignCenter(Text.Colored(accountMod.Info, Color.Green),
                                    width - 2 + Text.ColoredDiff(accountMod.Info, Color.Green)));

                                // Зміщаємо курсор нище і відмальовуємо саме поле
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Нове ім'я: ");

                                // Малюємо лінію нище від поля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write(TableGen.Line(width - 2));

                                Console.CursorVisible = true;

                                // Повертаємо курсор до поля вводу і даємо запит на ввід імені
                                Console.SetCursorPosition(Config.PosX + 13, Console.CursorTop - 1);
                                (value, command) = Input.ReadStringValue();
                                if (command == "Exit") return (account, false);
                                accountMod.Username = value ?? "None";

                                Console.CursorVisible = false;

                                // Стираємо лінію після вводу імені
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write(new string(' ', width - 2));

                                // Переміщуємо курсор на позицію нижче форми й виводимо повідомлення про успішність зміни імені
                                Console.SetCursorPosition(Config.PosX, Config.PosY + 8);
                                Console.Write(Text.AlignCenter(
                                    Text.Colored("Ім'я акаунта успішно змінено !", Color.Green),
                                    width + 2 + Text.ColoredDiff("Ім'я акаунта успішно змінено !", Color.Green)));

                                Console.SetCursorPosition(Config.PosX, Config.PosY + 9);
                                Console.Write("Натисність любу клавішу. . .");
                                Console.ReadKey(true);

                                // Збереження змін у базі даних
                                db.SaveChanges();

                                return (account, true);
                            }

                            // Якщо поточний елемент - Пароль
                            if (Console.CursorTop == Config.PosY + 6)
                            {
                                // Оприділяємо ширину форми
                                width = "Старий пароль: ".Length + Config.MaxLength + 4;

                                // Ширина рамки = Довжина поля з пробілом в началі + Максимальна довжина вводимого значення
                                TableGen.DrawFrame(width + 2, 6);

                                // Малювання заголовка форми + Центрування в середині формі
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                                Console.Write(Text.AlignCenter(Text.Colored(accountMod.Info, Color.Green),
                                    width - 2 + Text.ColoredDiff(accountMod.Info, Color.Green)));

                                // Зміщаємо курсор нище і відмальовуємо поле для вводу старого паролю
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Старий пароль: ");

                                // Зміщаємо курсор нище і відмальовуємо поле для вводу нового пароля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Новий пароль: ");

                                // Малюємо лінію нище від поля вводу старого пароля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop - 1);
                                Console.Write(TableGen.Line(width - 2));

                                Console.CursorVisible = true;

                                // Зациклюємо ввід старого пароля
                                while (true)
                                {
                                    // Повертаємо курсор до поля вводу і даємо запит на ввід старого пароля
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 3);
                                    (value, command) = Input.ReadPassword();
                                    if (command == "Exit") return (account, false);
                                    var oldPassword = value ?? "None";

                                    // Якщо введений пароль правильний
                                    if (accountMod.ValidatePassword(oldPassword))
                                    {
                                        // Стираємо лінію під старим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4);
                                        Console.Write(new string(' ', width - 2));

                                        // Малюємо лінію під новим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                        Console.Write(TableGen.Line(width - 2));

                                        // Переміщуємо курсор до поля введення нового пароля
                                        Console.SetCursorPosition(Config.PosX + 16, Console.CursorTop - 1);
                                        break;
                                    }

                                    // Якщо введений пароль неправильний
                                    // Стираємо введений пароль
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 3);
                                    Console.Write(new string(' ', Config.MaxLength));

                                    // Перекрашуємо лінію у червоний колір
                                    Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                    Console.Write(Text.Colored(TableGen.Line(width - 2), Color.Red));
                                }

                                // Вводимо новий пароль (з перевіркою на команду)
                                (value, command) = Input.ReadPassword();
                                if (command == "Exit") return (account, false);
                                accountMod.Password = value ?? "None";

                                Console.CursorVisible = false;

                                // Стираємо лінію після вводу пароля
                                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 6);
                                Console.Write(new string(' ', width - 2));

                                // Переміщуємо курсор на позицію нижче форми й виводимо повідомлення про успішність зміни пароля
                                Console.SetCursorPosition(Config.PosX, Config.PosY + 8);
                                Console.Write(Text.AlignCenter(
                                    Text.Colored("Пароль акаунта успішно змінено !", Color.Green),
                                    width + 2 + Text.ColoredDiff("Пароль акаунта успішно змінено !", Color.Green)));

                                Console.SetCursorPosition(Config.PosX, Config.PosY + 9);
                                Console.Write("Натисність любу клавішу. . .");
                                Console.ReadKey(true);

                                // Збереження змін у базі даних
                                db.SaveChanges();

                                return (account, true);
                            }

                            // Якщо поточний елемент - Ім'я та пароль
                            if (Console.CursorTop == Config.PosY + 8)
                            {
                                // Оприділяємо ширину форми - Слово "Старий пароль" довше, тому відштовхуємось від нього
                                width = "Старий пароль: ".Length + Config.MaxLength + 4;

                                // Малюємо рамку
                                TableGen.DrawFrame(width + 2, 8);

                                // Виводим заголовок
                                Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
                                Console.Write(Text.AlignCenter(Text.Colored(accountMod.Info, Color.Green),
                                    width - 2 + Text.ColoredDiff(accountMod.Info, Color.Green)));

                                // Виводимо необхідні поля й малюємо лінію на початковій позиції
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Нове ім'я: ");
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write(TableGen.Line(width - 2));
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                Console.Write("Старий пароль: ");
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write("Новий пароль: ");

                                Console.CursorVisible = true;

                                // Вводимо нове ім'я (з перевіркою команди)
                                Console.SetCursorPosition(Config.PosX + 8, Console.CursorTop - 4);
                                (value, command) = Input.ReadStringValue();
                                if (command == "Exit") return (account, false);
                                accountMod.Username = value ?? "None";

                                // Стираємо лінію під полем вводу імені
                                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 4);
                                Console.Write(new string(' ', width - 2));

                                // Малюємо лінію під полем вводу старого пароля
                                Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                Console.Write(TableGen.Line(width - 2));

                                // Зациклюємо введення старого паролю
                                while (true)
                                {
                                    // Повертаємо курсор до поля вводу і даємо запит на ввід старого пароля
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 5);
                                    (value, command) = Input.ReadPassword();
                                    if (command == "Exit") return (account, false);
                                    var oldPassword = value ?? "None";

                                    // Якщо введений пароль правильний
                                    if (accountMod.ValidatePassword(oldPassword))
                                    {
                                        // Стираємо лінію під старим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 6);
                                        Console.Write(new string(' ', width - 2));

                                        // Малюємо лінію під новим паролем
                                        Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 2);
                                        Console.Write(TableGen.Line(width - 2));

                                        // Переміщуємо курсор до поля введення нового пароля
                                        Console.SetCursorPosition(Config.PosX + 16, Console.CursorTop - 1);
                                        break;
                                    }

                                    // Якщо введений пароль неправильний
                                    // Стираємо введений пароль
                                    Console.SetCursorPosition(Config.PosX + 17, Config.PosY + 5);
                                    Console.Write(new string(' ', Config.MaxLength));

                                    // Перекрашуємо лінію у червоний колір
                                    Console.SetCursorPosition(Config.PosX + 2, Console.CursorTop + 1);
                                    Console.Write(Text.Colored(TableGen.Line(width - 2), Color.Red));
                                }

                                // Вводимо новий пароль (з перевіркою на команду)
                                (value, command) = Input.ReadPassword();
                                if (command == "Exit") return (account, false);
                                accountMod.Password = value ?? "None";

                                Console.CursorVisible = false;

                                // Стираємо лінію після вводу пароля
                                Console.SetCursorPosition(Config.PosX + 2, Config.PosY + 8);
                                Console.Write(new string(' ', width - 2));

                                // Переміщуємо курсор на позицію нижче форми й виводимо повідомлення про успішність зміни пароля
                                Console.SetCursorPosition(Config.PosX, Config.PosY + 12);
                                Console.Write(Text.AlignCenter(
                                    Text.Colored("Пароль акаунта успішно змінено !", Color.Green),
                                    width + 2 + Text.ColoredDiff("Пароль акаунта успішно змінено !", Color.Green)));

                                Console.SetCursorPosition(Config.PosX, Config.PosY + 13);
                                Console.Write("Натисність любу клавішу. . .");
                                Console.ReadKey(true);

                                // Збереження змін у базі даних
                                db.SaveChanges();

                                return (account, true);
                            }

                            break;
                        }
                    }
                }
            }
        }

        return (account, true);
    }
}
