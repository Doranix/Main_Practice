namespace Main_Practice.Tools;

using Menus;
using System.Text;
using Configuration;
using Animals;

public static class TableGen
{
    private static readonly Dictionary<(int, string), string> LineCache = new();
    
    // Побудова лінії довільної довжини
    public static string Line(int length, string symbol = "\u2500")
    {
        var key = (length, symbol);

        if (!LineCache.TryGetValue(key, out var line))
        {
            var builder = new StringBuilder();
            for (int i = 0; i < length; i++)
                builder.Append(symbol);

            line = builder.ToString();
            LineCache[key] = line;
        }

        return line;
    }
    
    // Побудова рамки
    public static void DrawFrame(int formWidth, int formHeight, int x = 0, int y = 0, bool clear = true)
    {
        if (clear)
            Console.Clear();
        
        // Верхня границя форми
        Console.SetCursorPosition(Config.PosX +  x, Config.PosY + y);
        Console.WriteLine("\u250c" + Line(formWidth - 2) + "\u2510");
        
        // Бокові границі
        for (int pos = Config.PosY + y; pos < formHeight + Config.PosY; pos++)
        {
            // Ліва границя
            Console.SetCursorPosition(Config.PosX + x, pos + 1);
            Console.Write("\u2502");
            
            // Права границя
            Console.SetCursorPosition(Config.PosX + x + formWidth - 1, pos + 1);
            Console.Write("\u2502");
        }
        
        // Нижня границя
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + formHeight + 1);
        Console.WriteLine("\u2514" + Line(formWidth - 2) + "\u2518");
    }
    
    // Нативна функція для переміщення рамки (клавішами клавіатури) - яка повертає номер обраного елемента
    public static int NavigateFrame<T>(int width, int height, List<T> items, int x = 0, int y = 0, bool navLeft = false, bool navRight = false) where T : class, IAnimalClass
    {
        // Поточний елемент
        var frame = 0;
        
        // Поточний елемент у межах форми
        var currentPosInFrame = 1;
        
        // Поточна позиція прокручування
        var scrollPosition = 0;
        
        // Заради оптимізації - зразу визначимо кількість наданих елементів
        var elCount = items.Count;

        // Якщо елементів - 0 --> повернути 0
        if (elCount == 0) return 1;
        
        Console.CursorVisible = false;
        
        // Цикл для переміщення
        while (true)
        {
            // Малювання рамки на поточному елементі
            DrawFrame(width, height, x, y + (currentPosInFrame - 1) * 2, false);
            
            // Вивід нумерації поточного елемента
            ElNumberInfo(width - 2, x - 1, 1, $"{frame + 1}", $"{elCount}");
            
            // Виділення поточного елементу зеленим
            Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + (currentPosInFrame - 1) * 2 + 1);
            Console.Write(Text.AlignCenter(Text.Colored(items[frame].Info, Color.Green), width - 2 + Text.ColoredDiff(items[frame].Info, Color.Green)));
            
            var keyInfo = Console.ReadKey(true);

            // Обробка кнопок - Вправо, Вліво
            if (navLeft)
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                    return -1;
            if (navRight)
                if (keyInfo.Key == ConsoleKey.RightArrow)
                    return -2;
            
            switch (keyInfo.Key)
            {
                // Якщо натиснута клавіша Esc --> Вийти із циклу
                case ConsoleKey.Escape:
                    return 0;

                // Якщо натиснута стрілочка вверх --> перемістити рамку до верхнього елемента
                case ConsoleKey.UpArrow:
                {
                    if (currentPosInFrame > 1)
                    {
                        // Стирання рамки з попереднього елемента
                        Clear(width, height + 2, x, y + (currentPosInFrame - 1) * 2);
                        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + (currentPosInFrame - 1) * 2 + 1);
                        Console.Write(Text.AlignCenter(items[frame].Info, width - 2));
                        
                        // Понижуємо значення frame та позицію в рамці - як знак того, що ми перемістились на елемент вище
                        frame--;
                        currentPosInFrame--;
                    }
                    
                    // Якщо вперлись вверх форми й вище ще є елементи - пролистуємо список
                    if (currentPosInFrame == 1 && frame > 0)
                    {
                        // Стирання попереднього контенту
                        Clear(width, Config.MaxElToForm * 2 + 1, 1, 3);
                        
                        AnimalYard.PrintItemList(items, 2, 4, 1, width - 2, --scrollPosition);
                        
                        // Збільшуємо значення frame - як знак того, що ми перемістились на елемент нище
                        frame--;
                    }
                    
                    break;
                }
                
                // Якщо натиснута стрілочка вниз --> перемістити рамку до нижнього елемента
                case ConsoleKey.DownArrow:
                {
                    // Якщо поточний елемент вибірки в межах форми, стерти вибірку попереднього елемента і збільшити значення frame
                    if (currentPosInFrame < Math.Min(Config.MaxElToForm, items.Count))
                    {
                        // Стирання рамки з попереднього елемента
                        Clear(width, height, x, y + (currentPosInFrame - 1) * 2);
                        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + (currentPosInFrame - 1) * 2 + 1);
                        Console.Write(Text.AlignCenter(items[frame].Info, width - 2));
                        
                        // Збільшуємо значення frame та позицію в рамці - як знак того, що ми перемістились на елемент нище
                        frame++;
                        currentPosInFrame++;
                    }
                    
                    // Якщо вперлись в низ форми й нище ще є елементи -> пролистуємо список
                    else if (currentPosInFrame == Config.MaxElToForm && frame < elCount - 1)
                    {
                        // Стирання попереднього контенту
                        Clear(width, Config.MaxElToForm * 2 + 1, 1, 3);

                        AnimalYard.PrintItemList(items, 2, 4, 1, width - 2, ++scrollPosition);
                        
                        // Збільшуємо значення frame - як знак того, що ми перемістились на елемент нище
                        frame++;
                    }
                    
                    break;
                }

                // Якщо натиснута клавіша "Enter" --> повернути порядковий номер обраного елемента
                case ConsoleKey.Enter:
                    return frame + 1;
            }
        }
    }
    
    
    // Метод для виводу показника поточного елемента
    private static void ElNumberInfo(int width, int formPosX, int y, string elNumber, string elCount)
    {
        // Загальна ширина рядка (включно з дужками, слешем та пробілами всередині)
        int contentWidth = 3 + elNumber.Length + elCount.Length; // '[' + elNumber + '/' + elCount + ']'
    
        // Обчислення позиції X для центрування відносно "width"
        int centerX = (width - contentWidth) / 2;
        int globalPosX = Config.PosX + formPosX + centerX;

        // Розміщення курсора для початку рядка
        Console.SetCursorPosition(globalPosX, Config.PosY + y);
    
        Console.Write("[ ");

        // Вивід відцентрованого номера елемента
        Console.Write(Text.Colored(elNumber, elNumber == elCount ? Color.Yellow : Color.Green));

        Console.Write(" / ");

        // Вивід відцентрованого числа всіх елементів
        Console.Write(Text.Colored(elCount, Color.Yellow));

        Console.Write(" ]");
    }
    
    
    // Стирання рамки
    public static void Clear(int frameWidth, int frameHeight, int posX, int posY)
    {
        for (int i = 0; i < frameHeight; i++)
        {
            Console.SetCursorPosition(Config.PosX + posX, Config.PosY + posY + i);
            Console.Write(new string(' ', frameWidth));
        }
    }
}
