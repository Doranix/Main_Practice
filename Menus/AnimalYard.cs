using Main_Practice.Animals;
using Main_Practice.Configuration;
using Main_Practice.DATABASE;
using Main_Practice.Tools;

namespace Main_Practice.Menus;

public static partial class AnimalYard
{
    // Отримати список об'єктів
    public static void PrintItemList<T>(List<T> database, short x = 1, short y = 1, byte distance = 1, int width = 0, int startIndex = 0) where T : class, IAnimalClass
    {
        if (database.Count == 0) return;
        
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y);
        
        // Вивід елементів
        for (int i = startIndex; 
             i < (database.Count > Config.MaxElToForm
                 ? (startIndex + Config.MaxElToForm)
                 : database.Count); i++)
        {
            // Динамічне центрування видимих даних
            Console.Write(Text.AlignCenter(database[i].Info, width));
            
            Console.SetCursorPosition(Config.PosX + x, Console.CursorTop + 1 + distance);
        }
    }
    
    
    // Отримати інформацію про об'єкт
    public static void PrintItemInfo<T>(List<T> items) where T : class, IAnimalClass
    {
        if (items.Count > 0)
        {
            var width = DbController.GetMaxLength(items) + 4;    // Ширина рамки
            var height = Config.MaxElToForm * 2 + 3;             // Висота рамки
        
            // Малювання рамки для елементів
            TableGen.DrawFrame(width, height);
        
            // Вивід елементів усередині рамки
            PrintItemList(items, 2, 4, 1, width - 4);
        
            // Викликаємо метод для вибірки елемента, при цьому зберігаючи індекс вибраного елемента
            var index = TableGen.NavigateFrame(width - 2, 1, items, 1, 3) - 1;

            var info = items[index].Info; // Зразу зберігаємо необхідну нам інформацію
        
            TableGen.DrawFrame(info.Length + 6, 3); // Підганяєм рамку під вивід 
        
            Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
            Console.Write(Text.AlignCenter(info, info.Length + 4));
            
            // Виводимо запит на ввід будь-якої клавіші
            Console.SetCursorPosition(Config.PosX, Console.CursorTop + 4);
            Console.Write(Text.AlignCenter("Натисність будь-яку клавішу. . .", info.Length + 6));
            Console.ReadKey(true);
        }
        
        else
        {
            TableGen.DrawFrame(28, 3); // Підганяєм рамку під вивід 
        
            Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
            Console.Write(Text.AlignCenter(
                $"{Text.Colored("Список", Color.Yellow)} {Text.Colored("пустий", Color.Blue)} \u00AF\\_(\u30C4)_/\u00AF",
                25 + Text.ColoredDiff("Список", Color.Yellow) + Text.ColoredDiff("пустий", Color.Blue)));
            
            // Виводимо запит на ввід будь-якої клавіші
            Console.SetCursorPosition(Config.PosX, Console.CursorTop + 4);
            Console.Write(Text.AlignCenter("Натисність будь-яку клавішу. . .", 25));
            Console.ReadKey(true);
        }
    }
    
    
    // Отримати робітників зі вказаним стажем і більше
    public static List<Worker> GetWorkersByExperience(int workExp)
    {
        using var db = new DbController();
        return db.Workers.ToList().Where(w => w.WorkExperience >= workExp).ToList();
    }
    
    
    // Отримати тварин та їх продукцію
    public static List<ItemInfo> GetAnimalsWithProducts()
    {
        using var db = new DbController();
    
        // Виконуємо один запит для отримання тварин з їх назвами та продукцією
        var animalsWithProducts = db.Animals
            .Join(db.AnimalNames,
                animal => animal.AnimalNameId,
                animalName => animalName.Id,
                (animal, animalName) => new { animal, animalName })
            .Join(db.Products,
                animalWithName => animalWithName.animal.Id,
                product => product.AnimalId,
                (animalWithName, product) => new { animalWithName, product })
            .Join(db.ProductTypes,
                animalWithProduct => animalWithProduct.product.ProductTypeId,
                productType => productType.Id,
                (animalWithProduct, productType) => new
                {
                    AnimalName = animalWithProduct.animalWithName.animalName.Name,
                    ProductType = productType.Name
                })
            .ToList();

        // Формуємо результат у вигляді списку ItemInfo
        return animalsWithProducts.Select(ap => new ItemInfo
        {
            Info = $"{ap.AnimalName}, Продукція: {ap.ProductType}"
        }).ToList();
    }
    
    // Видалити об'єкт із бази даних
    public static void DeleteItem<T>(List<T> items) where T : class, IAnimalClass
    {
        using var db = new DbController();

        if (items.Count == 0)
        {
            TableGen.DrawFrame(28, 3); // Підганяєм рамку під вивід 
        
            Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 2);
            Console.Write(Text.AlignCenter(
                $"{Text.Colored("Список", Color.Yellow)} {Text.Colored("пустий", Color.Blue)} \u00AF\\_(\u30C4)_/\u00AF",
                25 + Text.ColoredDiff("Список", Color.Yellow) + Text.ColoredDiff("пустий", Color.Blue)));
            
            // Виводимо запит на ввід будь-якої клавіші
            Console.SetCursorPosition(Config.PosX, Console.CursorTop + 4);
            Console.Write(Text.AlignCenter("Натисність будь-яку клавішу. . .", 25));
            Console.ReadKey(true);
            return;
        }

        // Ширина рамки
        var width = DbController.GetMaxLength(items) + 4;
        
        TableGen.DrawFrame(width, items.Count > Config.MaxElToForm ? Config.MaxElToForm * 2 + 4 : items.Count * 2 + 4);
        
        // Вибірка елемента і його видалення
        PrintItemList(items, 2, 4, 1, width - 4);
        var itemToDelete = db.Set<T>().Find(items[TableGen.NavigateFrame(width - 2, 1, items, 1, 3) - 1].Id);

        if (itemToDelete != null)
        {
            db.Remove(itemToDelete);
            db.SaveChanges();
        }
    }
    
    // Отримати тварин зі вказаним раціоном
    public static List<Animal> GetAnimalsWithRation(string ration)
    {
        using var db = new DbController();
        return db.Animals
            .Join(db.AnimalNames, animal => animal.AnimalNameId, animalName => animalName.Id,
                (animal, animalName) => new { animal, animalName }).Where(el => el.animalName.Ration == ration)
            .Select(el => el.animal).ToList();
    }
    
    // Отримати тварин, продуктивність котрих більша за зазначену
    public static List<Animal> GetProductivityAnimals(double productive)
    {
        using var db = new DbController();
        return db.Animals
            .Join(db.Products, animal => animal.Id, product => product.AnimalId,
                (animal, product) => new { animal, product.AveragePerfomance })
            .Where(el => el.AveragePerfomance > productive).Select(el => el.animal).ToList();
    }
    
    // Отримати тварину з максимальною вагою
    public static Animal GetAnimalWithMaxWeight()
    {
        using var db = new DbController();
        return db.Animals.OrderByDescending(animal => animal.Weight).First();
    }
    
    // Отримати список тварин без робітників
    public static List<Animal> GetAnimalWithoutWorker()
    {
        using var db = new DbController();
        return db.Animals.Where(animal => !db.Workers.Any(worker => worker.Id == animal.WorkerId)).ToList();
    }
    
    // Отримати список тварин без продукції
    public static List<Animal> GetAnimalWithoutProduct()
    {
        using var db = new DbController();
        return db.Animals.Where(animal => !db.Products.Any(product => product.AnimalId == animal.Id)).ToList();
    }
    
    // Скопіювати до нової таблиці тварин із заданою продукцією
    public static void CopyAnimalData(string productive)
    {
        using var db = new DbController();
    
        // Отримуємо тварин, які мають вказаний тип продукції
        var selectedAnimals = db.Animals
            .Join(db.Products,
                animal => animal.Id,
                product => product.AnimalId,
                (animal, product) => new { animal, product })
            .Join(db.ProductTypes,
                animalProduct => animalProduct.product.ProductTypeId,
                productType => productType.Id,
                (animalProduct, productType) => new { animalProduct.animal, productType.Name })
            .Where(ap => ap.Name == productive)
            .Select(ap => ap.animal)
            .ToList();

        // Копіюємо відібраних тварин до додаткової таблиці
        db.AdditionalTable.AddRange(selectedAnimals);
        db.SaveChanges();
    }
}
