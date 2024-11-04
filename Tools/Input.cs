namespace Main_Practice.Tools;

using System.Text;
using Configuration;

public static class Input
{
    // Метод для вводу пароля, із заміною значень ні зірочки
    public static (string? value, string? command) ReadPassword()
    {
        var password = new StringBuilder();
        
        ConsoleKeyInfo keyInfo;

        // Заміна вводимих символів на зірочки
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            // Якщо натиснута клавіша - "Backspace (Клавіша для стирання)" - Видалити останній символ з пароля і стерти символ з консолі
            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                //Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                //Console.Write(' ');
                //Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            
            // Якщо вводимий символ дозволений - він додається до пароля і виводиться на екран у вигляді зірочки
            else if (keyInfo.Key != ConsoleKey.Backspace && password.Length <= Config.MaxLength && Config.AllowedCharacters.Contains(keyInfo.KeyChar))
                password.Append(keyInfo.KeyChar);
            
            // Якщо натиснута клавіша "Esc" - дати команду на вихід
            else if (keyInfo.Key == ConsoleKey.Escape)
                return (null, "Exit");
        }
        
        // Повернення введеного пароля
        return (password.ToString(), null);
    }
    
    
    // Метод для вводу імені
    public static (string? value, string? command) ReadName()
    {
        var name = new StringBuilder();
        
        ConsoleKeyInfo keyInfo;
        
        // Ввід імені з урахунком максимальної довжини
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            // Якщо натиснута клавіша - "Backspace (Клавіша для стирання)" - Видалити останній символ з пароля і стерти символ з консолі
            if (keyInfo.Key == ConsoleKey.Backspace && name.Length > 0)
            {
                name.Remove(name.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            
            // Якщо вводимий символ дозволений - він додається до пароля і виводиться на екран
            else if (keyInfo.Key != ConsoleKey.Backspace && name.Length <= Config.MaxLength && Config.AllowedCharacters.Contains(keyInfo.KeyChar))
            {
                name.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }
            
            // Якщо натиснута клавіша "Esc" - дати команду на вихід
            else if (keyInfo.Key == ConsoleKey.Escape)
                return (null, "Exit");
        }
        
        // Повернення введеного імені
        return (name.ToString(), null);
    }
}