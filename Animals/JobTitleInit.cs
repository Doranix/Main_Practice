namespace Main_Practice.Animals;

using DATABASE;
using Tools;
using Configuration;

public partial class JobTitle : IInitable
{
    // Метод для ініціалізації полів об'єкта
    public int Init(int x = 0, int y = 0)
    {
        using var db = new DbController();

        // Малювання рамки форми
        DrawFrame(x, y);

        // Зчитування вводу користувача
        ReadUserInput(x, y);

        // Збереження даних у базі
        SaveToDatabase(db);

        // Відображення повідомлення про успіх
        DisplaySuccessMessage(x, y);

        // Очищення форми
        ClearForm(x, y);

        // Повідомлення про успіх
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 5);
        Console.Write("Об'єкт успішно ініціалізований!");
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 6);
        Console.Write("Натисніть будь-яку клавішу...");
    
        Console.ReadKey(true);
    
        // Стирання форми після заповнення даних у формі
        TableGen.Clear(32, 6, x, y);
        
        return Id;
    }

    private void DrawFrame(int x, int y)
    {
        TableGen.DrawFrame(32, 1, x, y, clear: false);
    }

    private void ReadUserInput(int x, int y)
    {
        Console.SetCursorPosition(Config.PosX + x + 2, Config.PosY + y + 1);
        Console.Write("Посада: ");
        Title = Input.ReadStringValue().value ?? "Unknown";
    }

    private void SaveToDatabase(DbController db)
    {
        db.JobTitles.Add(this);
        db.SaveChanges();
    }

    private void DisplaySuccessMessage(int x, int y)
    {
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 4);
        Console.Write("Об'єкт успішно ініціалізований!");
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 5);
        Console.Write("Натисніть будь-яку клавішу...");
        Console.ReadKey(true);
    }

    private void ClearForm(int x, int y)
    {
        TableGen.Clear(32, 6, x, y);
    }
}