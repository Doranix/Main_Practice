namespace Main_Practice.Tools;

using System.Text;
using Configuration;

public static class Input
{
    // Метод для вводу пароля, із заміною значень ні зірочки
    public static (string? value, string? command) ReadPassword()
    {
        Console.CursorVisible = true;
        
        var password = new StringBuilder();
        
        ConsoleKeyInfo keyInfo;

        // Заміна вводимих символів на зірочки
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            // Якщо натиснута клавіша - "Backspace (Клавіша для стирання)" - Видалити останній символ з пароля і стерти символ з консолі
            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            
            // Якщо вводимий символ дозволений - він додається до пароля і виводиться на екран у вигляді зірочки
            else if (keyInfo.Key != ConsoleKey.Backspace && password.Length <= Config.MaxLength && Config.AllowedCharacters.Contains(keyInfo.KeyChar))
                password.Append(keyInfo.KeyChar);
            
            // Якщо натиснута клавіша "Esc" - дати команду на вихід
            else if (keyInfo.Key == ConsoleKey.Escape)
                return (null, "Exit");
        }
        
        Console.CursorVisible = false;
        
        // Повернення введеного пароля
        return (password.ToString(), null);
    }
    
    
    // Метод для вводу імені
    public static (string? value, string? command) ReadStringValue()
    {
        Console.CursorVisible = true;
        
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
        
        Console.CursorVisible = false;
        
        // Повернення введеного імені
        return (name.ToString(), null);
    }
    
    // Метод для вводу числового значення
    public static (int? value, string? command) ReadNumber()
    {
        Console.CursorVisible = true;
        
        var numberInput = new StringBuilder();
        ConsoleKeyInfo keyInfo;
    
        // Ввід числового значення з урахунком максимальної довжини
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            // Якщо натиснута клавіша "Backspace" - Видалити останній символ і стерти його з консолі
            if (keyInfo.Key == ConsoleKey.Backspace && numberInput.Length > 0)
            {
                numberInput.Remove(numberInput.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        
            // Якщо натиснута цифра та її довжина не перевищує дозволену - додати до вводу
            else if (char.IsDigit(keyInfo.KeyChar) && numberInput.Length < Config.MaxLength)
            {
                numberInput.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }

            // Якщо натиснута клавіша "Esc" - вийти
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return (null, "Exit");
            }
        }
    
        Console.CursorVisible = false;
        
        // Перетворення введеного значення у число і повернення результату
        return int.TryParse(numberInput.ToString(), out int result) ? (result, null) : (null, "InvalidInput");
    }
    
    // Метод для вводу дійсного числа (Double)
    public static (double? value, string? command) ReadDouble()
    {
        Console.CursorVisible = true;

        var numberInput = new StringBuilder();
        bool hasDecimalPoint = false;
        ConsoleKeyInfo keyInfo;

        // Ввід числового значення
        while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            // Якщо натиснута клавіша "Backspace" - Видалити останній символ і стерти його з консолі
            if (keyInfo.Key == ConsoleKey.Backspace && numberInput.Length > 0)
            {
                // Оновлення стану наявності десяткового роздільника
                if (numberInput[^1] == '.' || numberInput[^1] == ',')
                    hasDecimalPoint = false;

                numberInput.Remove(numberInput.Length - 1, 1);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            // Якщо натиснута цифра і довжина не перевищує дозволену - додати до вводу
            else if (char.IsDigit(keyInfo.KeyChar) && numberInput.Length < Config.MaxLength)
            {
                numberInput.Append(keyInfo.KeyChar);
                Console.Write(keyInfo.KeyChar);
            }
            // Якщо натиснута крапка або кома для розділення десяткових значень
            else if ((keyInfo.KeyChar == '.' || keyInfo.KeyChar == ',') && !hasDecimalPoint && numberInput.Length < Config.MaxLength)
            {
                numberInput.Append('.');
                Console.Write('.');
                hasDecimalPoint = true;
            }
            // Якщо натиснута клавіша "Esc" - вийти
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.CursorVisible = false;
                return (null, "Exit");
            }
        }

        Console.CursorVisible = false;

        // Перетворення введеного значення у число і повернення результату
        return double.TryParse(numberInput.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double result)
            ? (result, null)
            : (null, "InvalidInput");
    }
}
