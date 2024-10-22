namespace Main_Practice.Tools;

public static class TableGen
{
    // Побудова лінії довільної довжини
    public static string Line(int length, string symbol = "\u2500")
    {
        var line = "";
        for (int i = 0; i < length; i++)
            line += symbol;

        return line;
    }
    
    // Побудова форми для реєстрації або логіну
    public static void DrawFrame(int formWidth, int formHeight)
    {
        Console.Clear();
        
        // Верхня границя форми
        Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y);
        Console.WriteLine("\u250c" + Line(formWidth - 2) + "\u2510");
        
        // Бокові границі
        for (int pos = (int) Cursor.Y; pos < formHeight + (int) Cursor.Y; pos++)
        {
            // Ліва границя
            Console.SetCursorPosition((int) Cursor.X, pos + 1);
            Console.Write("\u2502");
            
            // Права границя
            Console.SetCursorPosition((int) Cursor.X + formWidth - 1, pos + 1);
            Console.Write("\u2502");
        }
        
        // Нижня границя
        Console.SetCursorPosition((int) Cursor.X, (int) Cursor.Y + formHeight + 1);
        Console.WriteLine("\u2514" + Line(formWidth - 2) + "\u2518");
    }
}