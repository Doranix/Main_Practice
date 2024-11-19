using Microsoft.EntityFrameworkCore;

namespace Main_Practice.Animals;

using Configuration;
using DATABASE;
using Tools;
using Menus;

public partial class Animal : IInitable
{
    // Метод для ініціалізації полів об'єкта
    public int Init(int x = 0, int y = 0)
    {
        using var db = new DbController();
        DrawForm(Config.FormWidth, 11, x, y);

        //
        // Ініціалізація полів об'єкта
        //
        
        // Малювання лінії під полем коду імені тварини
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 5);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        AnimalNameId = SelectAnimalNameId(db.AnimalNames, x + Config.FormWidth + 1) ?? new AnimalName().Init(x + Config.FormWidth + 1);
        DrawFormElements(x, y);
        
        // Стирання лінії під полем коду імені тварини
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 5);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Малювання лінії під полем вводу кількості років
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 7);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        Age = InputField("Вік: ", x + Config.FormWidth / 3, y + 6);
        
        // Стирання лінії під полем вводу кількості років
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 7);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Малювання лінії під полем вводу ваги
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 9);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        Weight = InputField("Вага: ", x + Config.FormWidth / 3, y + 8);
        
        // Стирання лінії під полем вводу ваги
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 9);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Малювання лінії під полем коду робітника
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 11);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        WorkerId = SelectWorkerId(db.Workers, x + Config.FormWidth + 1) ?? new Worker().Init(x + Config.FormWidth + 1);
        DrawFormElements(x, y);
        
        // Стирання лінії під полем коду робітника
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 11);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        db.Animals.Add(this);
        db.SaveChanges();

        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 14);
        Console.Write("Об'єкт успішно ініціалізований !");

        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 15);
        Console.Write("Натисність любу клавішу. . .");

        Console.ReadKey(true);
        
        // Стирання форми після заповнення всіх даних
        TableGen.Clear(Config.FormWidth, 16, x, y);
        return Id;
    }
    
    
    // Отримання коду імені тварини
    private int? SelectAnimalNameId(DbSet<AnimalName> animalNames, int x = 0, int y = 0)
    {
        return animalNames.Any() ? DisplaySelection(animalNames.ToList(), x, y) : null;
    }
    
    
    // Отримання коду робітника
    private int? SelectWorkerId(DbSet<Worker> workers, int x = 0, int y = 0)
    {
        return workers.Any() ? DisplaySelection(workers.ToList(), x, y) : null;
    }
    
    
    // Вибірка елемента
    private int? DisplaySelection<T>(List<T> items, int x, int y) where T : class, IAnimalClass, IInitable, new()
    {
        // Визначаємо необхідні змінні
        var width = DbController.GetMaxLength(items) + 4;
        var height = Math.Min(Config.MaxElToForm * 2 + 3, items.Count * 2 + 3);
        var rightArrow = "\u2500\u2500\u27f6";
        
        // Відмальовка форми й вивід елементів
        TableGen.DrawFrame(width, height, x, y, false);
        AnimalYard.PrintItemList(items, (short)(x + 2), (short)(y + 4), 1, width - 4);
        
        // Відмальовка стрілочки над формою
        Console.SetCursorPosition(Config.PosX + x, Config.PosY - 1);
        Console.Write(Text.AlignCenter(rightArrow, width));
        
        // Вибірка елемента
        var index = TableGen.NavigateFrame(width - 2, 1, items, (short)(x + 1), y + 3, false, true) - 1;
        
        TableGen.Clear(width, height + 3, x, y - 1);
        if (index >= 0) return items[index].Id;

        // Якщо натиснута команда для створення нового об'єкта (стрілочка вправо) --> ініціалізувати новий об'єкт
        if (index == -3)
        {
            TableGen.Clear(width, height + 1, x, y - 1);
            return new T().Init(x, y);
        }
        
        return null;
    }
    
    
    // Малювання форми
    private void DrawForm(int width, int height, int posX, int posY)
    {
        TableGen.DrawFrame(width, height, posX, posY, false);
        
        // Заголовок
        Console.SetCursorPosition(Config.PosX + posX + 1, Config.PosY + posY + 2);
        Console.Write(Text.AlignCenter("Тварина", width - 2));
        
        // Вивід елементів форми
        DrawFormElements(posX, posY);
    }
    
    
    // Метод для відмальовування полів у формі
    private void DrawFormElements(int posX, int posY)
    {
        var formElements = new[]
        {
            "Код назви тварини: " + (AnimalNameId >= 0 ? AnimalNameId : ""),
            "Вік: " + (Age > 0 ? Age : ""),
            "Вага: " + (Weight > 0 ? Weight : ""),
            "Код робітника: " + (WorkerId >= 0 ? WorkerId : "")
        };
    
        for (int i = 0; i < formElements.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + i * 2);
            Console.Write(formElements[i]);
        }
    }
    
    
    // Метод для введення значення
    private int InputField(string label, int posX, int posY)
    {
        Console.SetCursorPosition(Config.PosX + posX, Config.PosY + posY);
        Console.Write(label);
        
        return Input.ReadNumber().value ?? 0;
    }
}