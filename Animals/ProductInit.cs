using Microsoft.EntityFrameworkCore;

namespace Main_Practice.Animals;

using DATABASE;
using Tools;
using Configuration;
using Menus;

public partial class Product : IInitable
{
    // Метод для ініціалізації полів об'єкта
    public int Init(int x = 0, int y = 0)
    {
        using var db = new DbController();

        var formHeight = 9; // Висота форми
        
        // Малювання форми
        DrawForm(Config.FormWidth, formHeight, x, y);
        
        // Малювання лінії під полем коду тварини
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 5);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        AnimalId = SelectAnimalId(db.Animals, x + Config.FormWidth + 2, y) ?? new Animal().Init(x + Config.FormWidth + 2, y);
        DrawFormElements(x, y);
        
        // Стирання лінії під полем коду тварини
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 5);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Малювання лінії під полем коду типу продукції
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 7);
        Console.Write(TableGen.Line(Config.FormWidth - 2));

        ProductTypeId = SelectProductTypeId(db.ProductTypes, x + Config.FormWidth + 2, y) ?? new ProductType().Init(x + Config.FormWidth + 2, y);
        DrawFormElements(x, y);

        // Стирання лінії під полем коду типу продукції
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 7);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Малювання лінії під полем середньої продуктивності
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 9);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        Console.SetCursorPosition(Config.PosX + x + Config.FormWidth / 3 + 24, Config.PosY + y + 8);
        AveragePerfomance = Input.ReadDouble().value ?? 0.0;
        
        // Стирання лінії під полем середньої продуктивності
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 9);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Додавання в базу
        db.Products.Add(this);
        db.SaveChanges();

        // Повідомлення про успішну ініціалізацію
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + formHeight + 3);
        Console.Write("Продукт успішно ініціалізований!");

        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + formHeight + 4);
        Console.Write("Натисніть будь-яку клавішу . . .");
        Console.ReadKey(true);

        // Стирання форми після завершення
        TableGen.Clear(Config.FormWidth, formHeight + 7, x, y);

        return Id;
    }
    
    // Малювання форми
    private void DrawForm(int width, int height, int posX, int posY)
    {
        TableGen.DrawFrame(width, height, posX, posY, false);

        Console.SetCursorPosition(Config.PosX + posX + 1, Config.PosY + posY + 2);
        Console.Write(Text.AlignCenter("Продукт", width - 2));

        DrawFormElements(posX, posY);
    }

    // Відображення елементів форми
    private void DrawFormElements(int posX, int posY)
    {
        var formElements = new[]
        {
            "Код тварини: " + (AnimalId >= 0 ? AnimalId : ""),
            "Код типу продукту: " + (ProductTypeId >= 0 ? ProductTypeId : ""),
            "Середня продуктивність: " + AveragePerfomance
        };

        for (int i = 0; i < formElements.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + i * 2);
            Console.Write(formElements[i]);
        }
    }

    // Вибірка тварини
    private int? SelectAnimalId(DbSet<Animal> animals, int x = 0, int y = 0)
    {
        return animals.Any() ? DisplaySelection(animals.ToList(), x, y) : null;
    }

    // Вибірка типу продукції
    private int? SelectProductTypeId(DbSet<ProductType> productTypes, int x = 0, int y = 0)
    {
        return productTypes.Any() ? DisplaySelection(productTypes.ToList(), x, y) : null;
    }

    // Вибірка елемента
    private int? DisplaySelection<T>(List<T> items, int x, int y) where T : class, IAnimalClass, IInitable, new()
    {
        var width = DbController.GetMaxLength(items) + 4;
        var height = Math.Min(Config.MaxElToForm * 2 + 3, items.Count * 2 + 3);
        var rightArrow = "\u2500\u2500\u27f6";

        // Малювання форми та вивід елементів
        TableGen.DrawFrame(width, height, x, y, false);
        AnimalYard.PrintItemList(items, (short)(x + 2), (short)(y + 4), 1, width - 4);

        // Малювання стрілочки над формою
        Console.SetCursorPosition(Config.PosX + x, Config.PosY - 1);
        Console.Write(Text.AlignCenter(rightArrow, width));

        // Вибірка елемента та повернення його ідентифікатора
        var index = TableGen.NavigateFrame(width - 2, 1, items, (short)(x + 1), y + 3, navRight: true) - 1;
        
        // Стирання форми вибірки
        TableGen.Clear(width, height + 3, x, y - 1);
        if (index >= 0) return items[index].Id;

        if (index == -3)
        {
            TableGen.Clear(width, height + 3, x, y - 1);
            return new T().Init(x, y);
        }

        return null;
    }
    
}