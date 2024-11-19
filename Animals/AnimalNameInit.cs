namespace Main_Practice.Animals;

using DATABASE;
using Tools;
using Configuration;

public partial class AnimalName : IInitable
{
    // Метод для ініціалізації полів об'єкта
    public int Init(int x = 0, int y = 0)
    {
        using var db = new DbController();
    
        // Малювання форми
        DrawForm(Config.FormWidth, 7, x, y);
    
        Name = InputStringField("Ім'я тварини: ", x, y + 4);
        Ration = InputStringField("Раціон: ", x, y + 6);
    
        // Додавання до бази даних
        db.AnimalNames.Add(this);
        db.SaveChanges();
    
        // Повідомлення про успіх
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 9);
        Console.Write("Об'єкт успішно ініціалізований!");
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 10);
        Console.Write("Натисніть будь-яку клавішу...");
    
        Console.ReadKey(true);
    
        // Стирання форми після заповнення даних у формі
        TableGen.Clear(Config.FormWidth, 12, x, y);
    
        return Id;
    } 
    
    // Малювання форми
    private void DrawForm(int width, int height, int posX, int posY)
    {
        TableGen.DrawFrame(width, height, posX, posY, false);

        Console.SetCursorPosition(Config.PosX + posX + 1, Config.PosY + posY + 2);
        Console.Write(Text.AlignCenter("Ім'я тварини", width - 2));
        DrawFormElements(posX, posY);
    }
    
    // Відображення елементів форми
    private void DrawFormElements(int posX, int posY)
    {
        var formElements = new[]
        {
            "Ім'я тварини: " + Name,
            "Раціон: " + Ration
        };

        for (int i = 0; i < formElements.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + i * 2);
            Console.Write(formElements[i]);
        }
    }
    
    // Метод для текстового вводу
    private string InputStringField(string label, int x, int y)
    {
        // Малюємо лінію під полем вводу
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 1);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
    
        // Малюємо саме поле вводу
        Console.SetCursorPosition(Config.PosX + x + Config.FormWidth / 3, Config.PosY + y);
        Console.Write(label);

        // Приймаємо ввід
        var input = Input.ReadStringValue().value ?? "Unknown";
    
        // Стираємо лінію під полем вводу
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 1);
        Console.Write(new string(' ', Config.FormWidth - 2));
    
        // Повертаємо отримане значення
        return input;
    }
}