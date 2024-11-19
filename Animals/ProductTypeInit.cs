namespace Main_Practice.Animals;

using DATABASE;
using Tools;
using Configuration;

public partial class ProductType : IInitable
{
    // Метод для ініціалізації полів об'єкта
    // Основний метод ініціалізації
    public int Init(int x = 0, int y = 0)
    {
        using var db = new DbController();

        // Малювання рамки
        DrawFrame(x, y);

        // Зчитування вводу користувача
        ReadUserInput(x, y);

        // Збереження даних у базі
        SaveToDatabase(db);

        // Повідомлення про успіх
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 5);
        Console.Write("Об'єкт успішно ініціалізований!");
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 6);
        Console.Write("Натисніть будь-яку клавішу...");
    
        Console.ReadKey(true);
    
        // Стирання форми після заповнення даних у формі
        TableGen.Clear(39, 6, x, y);
        
        return Id;
    }

    // Метод для малювання рамки
    private void DrawFrame(int x, int y)
    {
        TableGen.DrawFrame(39, 1, x, y, clear: false);
    }

    // Метод для вводу користувача
    private void ReadUserInput(int x, int y)
    {
        Console.SetCursorPosition(Config.PosX + x + 2, Config.PosY + y + 1);
        Console.Write("Вид продукції: ");
        Name = Input.ReadStringValue().value ?? "Unknown";
    }

    // Метод для збереження даних у базі
    private void SaveToDatabase(DbController db)
    {
        db.ProductTypes.Add(this);
        db.SaveChanges();
    }
}