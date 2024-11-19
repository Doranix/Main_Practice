namespace Main_Practice.Animals;

using Menus;
using DATABASE;
using Tools;
using Configuration;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public partial class Worker : IInitable
{
    // Метод для ініціалізації полів об'єкта
    public int Init(int x = 0, int y = 0)
    {
        using var db = new DbController();
        DrawForm(Config.FormWidth, 19, x, y);

        Name = InputStringField("Ім'я: ", x, 4);
        SurName = InputStringField("Прізвище: ", x, 6);
        MiddleName = InputStringField("По батькові: ", x, 8);
        
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + 11);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        SelectBirthDay("Дата народження: ", x + Config.FormWidth / 3, 10);
        DrawFormElements(x, y);
        
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + 11);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + 13);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        SelectGender("Стать: ", x + Config.FormWidth / 3, 12);
        DrawFormElements(x, y);
        
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + 13);
        Console.Write(new string(' ', Config.FormWidth - 2));


        Salary = InputNumberField("Заробітня плата: ", x, 14);
        WorkExperience = (uint)InputNumberField("Стаж роботи: ", x, 16);
        
        
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + 19);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        JobTitleId = SelectJobTitleId(db.JobTitles, x + Config.FormWidth + 1, y) ?? new JobTitle().Init(x, 21);
        DrawFormElements(x, y);
        
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + 19);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        db.Workers.Add(this);
        db.SaveChanges();
        
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 21);
        Console.Write("Об'єкт успішно ініціалізований !");

        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y + 22);
        Console.Write("Натисність любу клавішу. . .");

        Console.ReadKey(true);
        
        // Стирання форми після заповнення всіх даних
        TableGen.Clear(Config.FormWidth, 24, x, y);
        
        return Id;
    }
    
    // Малювання форми
    private void DrawForm(int width, int height, int posX, int posY)
    {
        TableGen.DrawFrame(width, height, posX, posY, false);

        Console.SetCursorPosition(Config.PosX + posX + 1, Config.PosY + posY + 2);
        Console.Write(Text.AlignCenter("Робітник", width - 2));
        DrawFormElements(posX, posY);
    }
    
    // Відображення елементів форми
    private void DrawFormElements(int posX, int posY)
    {
        var formElements = new[]
        {
            "Ім'я: " + Name,
            "Прізвище: " + SurName,
            "По-батькові: " + MiddleName,
            "Дата народження: " + BirthDay.ToShortDateString(),
            "Стать: " + (Gender.HasValue ? (Gender == Tools.Gender.Male ? "Чоловіча" : "Жіноча") : ""),
            "Заробітна плата: " + (Salary >= 0 ? Salary : ""),
            "Стаж роботи: " + WorkExperience,
            "Код посади: " + (JobTitleId.HasValue ? JobTitleId : "")
        };

        for (int i = 0; i < formElements.Length; i++)
        {
            Console.SetCursorPosition(Config.PosX + posX + Config.FormWidth / 3, Config.PosY + posY + 4 + i * 2);
            Console.Write(formElements[i]);
        }
    }
    
    // Визначення коду посади
    private int? SelectJobTitleId(DbSet<JobTitle> jobTitles, int x = 0, int y = 0)
    {
        return jobTitles.Any() ? DisplaySelection(jobTitles.ToList(), x, y) : null;
    }
    
    // Вибірка елемента
    private int? DisplaySelection<T>(List<T> items, int x, int y) where T : class, IAnimalClass, IInitable, new()
    {
        var width = DbController.GetMaxLength(items) + 4;
        var height = Math.Min(Config.MaxElToForm * 2 + 3, items.Count * 2 + 3);
        var rightArrow = "\u2500\u2500\u27f6";

        TableGen.DrawFrame(width, height, x, y, false);
        AnimalYard.PrintItemList(items, (short)(x + 2), (short)(y + 4), 1, width - 4);

        Console.SetCursorPosition(Config.PosX + x, Config.PosY - 1);
        Console.Write(Text.AlignCenter(rightArrow, width));

        var index = TableGen.NavigateFrame(width - 2, 1, items, (short)(x + 1), y + 3, navRight: true) - 1;
        
        // Стирання форми вибірки й повернення ідентифікатор обраного елемента
        TableGen.Clear(width, height + 3, x, y - 1);
        if (index >= 0) return items[index].Id;

        if (index == -3)
        {
            TableGen.Clear(width, height + 3, x, y - 1);
            return new T().Init(x, y);
        }

        return null;
    }
    
    // Введення дати
    private void SelectBirthDay(string label, int x = 0, int y = 0)
    {
        byte dateElement = 1;
        bool isDateEditing = true;

        while (isDateEditing)
        {
            Console.SetCursorPosition(Config.PosX + x, Config.PosY + y);
            Console.Write(label +
                          dateElement switch
                          {
                              // Виділення кількості днів
                              1 => Text.Bold(Text.Colored(BirthDay.Day.ToString("00"), Color.Green)) + '.' +
                                   BirthDay.Month.ToString("00") + '.' + 
                                   BirthDay.Year,
                              
                              // Виділення кількості місяців
                              2 => BirthDay.Day.ToString("00") + '.' +
                                   Text.Bold(Text.Colored(BirthDay.Month.ToString("00"), Color.Green)) + '.' +
                                   BirthDay.Year,
                              
                              // Виділення кількості років
                              3 => BirthDay.Day.ToString("00") + '.' +
                                   BirthDay.Month.ToString("00") + '.' +
                                   Text.Bold(Text.Colored(BirthDay.Year.ToString(), Color.Green)),
                              
                              _ => ""
                          });

            // Обробка натискання клавіш
            switch (Console.ReadKey(true).Key)
            {
                // Перехід до наступного елемента дати
                case ConsoleKey.RightArrow:
                    if (dateElement < 3) dateElement++;
                    break;
                
                // Перехід до попереднього елемента дати
                case ConsoleKey.LeftArrow:
                    if (dateElement > 1) dateElement--;
                    break;
                
                // Збільшення значення поточного елемента дати
                case ConsoleKey.UpArrow:
                    switch (dateElement)
                    {
                        case 1:
                            if (BirthDay <= DateTime.Now)
                                BirthDay = BirthDay.AddDays(1);
                            break;
                        
                        case 2:
                            if (BirthDay <= DateTime.Now)
                                BirthDay = BirthDay.AddMonths(1);
                            break;
                        
                        case 3:
                            if (BirthDay <= DateTime.Now)
                                BirthDay = BirthDay.AddYears(1);
                            break;
                    }

                    break;
                
                // Зменшення значення поточного елемента дати
                case ConsoleKey.DownArrow:
                    switch (dateElement)
                    {
                        case 1:
                            if (DateTime.Now.AddYears(-120) < BirthDay)
                                BirthDay = BirthDay.AddDays(-1);
                            break;
                        
                        case 2:
                            if (DateTime.Now.AddYears(-120) < BirthDay)
                                BirthDay = BirthDay.AddMonths(-1);
                            break;
                        
                        case 3:
                            if (DateTime.Now.AddYears(-120) < BirthDay)
                                BirthDay = BirthDay.AddYears(-1);
                            break;
                    }

                    break;
                
                case ConsoleKey.Enter:
                    isDateEditing = false;
                    break;
            }
        }
    }
    
    // Визначення статі
    private void SelectGender(string label, int x = 0, int y = 0)
    {
        bool isGenderEditing = true;
        Gender = Tools.Gender.Male;

        while (isGenderEditing)
        {
            Console.SetCursorPosition(Config.PosX + x, Config.PosY + y);
            Console.Write(label +
                          Gender switch
                          {
                              Tools.Gender.Male => "[ " + Text.Bold(Text.Colored("Чоловіча", Color.Green)) + " | " + "Жіноча ]",
                              Tools.Gender.Female => "[ Чоловіча | " + Text.Bold(Text.Colored("Жіноча", Color.Green)) + " ]",
                              _ => ""
                          });
            
            // Обробка натискання клавіш
            switch (Console.ReadKey(true).Key)
            {
                // Перехід до наступного елемента дати
                case ConsoleKey.RightArrow:
                    Gender = Tools.Gender.Female;
                    break;
                
                // Перехід до попереднього елемента дати
                case ConsoleKey.LeftArrow:
                    Gender = Tools.Gender.Male;
                    break;
                
                case ConsoleKey.Enter:
                    isGenderEditing = false;
                    
                    // Стирання контенту вибірки
                    Console.SetCursorPosition(Config.PosX + x, Config.PosY + y);
                    Console.Write(label + new string(' ', Config.MaxLength + 5));
                    
                    break;
            }
        }
    }

    // Метод для обробки вводу від користувача
    private string InputStringField(string label, int x = 0, int y = 0)
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
    
    // Метод для обробки вводу від користувача
    private int InputNumberField(string label, int x = 0, int y = 0)
    {
        // Малюємо лінію під полем вводу
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 1);
        Console.Write(TableGen.Line(Config.FormWidth - 2));
        
        // Малюємо саме поле вводу
        Console.SetCursorPosition(Config.PosX + x + Config.FormWidth / 3, Config.PosY + y);
        Console.Write(label);

        // Приймаємо ввід
        var input = Input.ReadNumber().value ?? 0;
        
        // Стираємо лінію під полем вводу
        Console.SetCursorPosition(Config.PosX + x + 1, Config.PosY + y + 1);
        Console.Write(new string(' ', Config.FormWidth - 2));
        
        // Повертаємо отримане значення
        return input;
    }
}