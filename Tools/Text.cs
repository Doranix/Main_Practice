namespace Main_Practice.Tools;

public static class Text
{
    // Вирівнювання тексту по лівому краю
    public static string AlignLeft(string str, int width)
    {
        return str.PadLeft(width);
    }
    
    // Вирівнювання тексту по правому краю
    public static string AlignRight(string str, int width)
    {
        return str.PadRight(width);
    }
    
    // Центрування тексту в заданій довжині рядка
    public static string AlignCenter(string str, int width)
    {
        return str.PadLeft((width - str.Length) / 2 + str.Length).PadRight(width);
    }
    
    // Кольоровий текст
    public static string Colored(string str, Color color)
    {
        switch (color)
        {
            case Color.Green:
                return $"\u001b[32m{str}\u001b[0m";
            case Color.Red:
                return $"\u001b[31m{str}\u001b[0m";
            case Color.Black:
                return $"\u001b[30m{str}\u001b[0m";
            case Color.Blue:
                return $"\u001b[34m{str}\u001b[0m";
            case Color.Gray:
                return $"\u001b[90m{str}\u001b[0m";
            case Color.Pink:
                return $"\u001b[35m{str}\u001b[0m";
            case Color.White:
                return $"\u001b[37m{str}\u001b[0m";
            case Color.Yellow:
                return $"\u001b[33m{str}\u001b[0m";
        }

        return str;
    }
    
    // Різниця довжини між звичайним текстом та кольоровим
    public static int ColoredDiff(string str, Color color)
    {
        return Colored(str, color).Length - str.Length;
    }
    
    // Жирний текст
    public static string Bold(string str)
    {
        return $"\u001b[1m{str}\u001b[0m";
    }
}