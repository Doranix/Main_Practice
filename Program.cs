using Main_Practice.Animals;

namespace Main_Practice;

using Configuration;
using DATABASE;
using Menus;
using Tools;
using AccessControl;

internal static class Program
{
    private static void Main()
    {
        try
        {
            // Підгружаєм конфігурацію
            Config.LoadConfig();
            using var db = new DbController();
            
            // Змінна для збереження стану програми
            bool isProgramStarted = true;

            Console.CursorVisible = false;

            if (Session.Current == null)
                Session.Current = db.Accounts.First();
            
            // Основний цикл програми
            while (isProgramStarted)
                switch (AnimalYard.MainMenu(Session.Current))
                {
                    case MenuConst.Exit:
                        isProgramStarted = false;
                        break;
                    
                    case MenuConst.Login:
                        Session.Current = Security.Login();
                        break;
                    
                    case MenuConst.Register:
                        Security.Register(Session.Current);
                        break;
                    
                    case MenuConst.EditAccountInfo:
                        Security.EditAccountInfo(Session.Current);
                        break;
                    
                    case MenuConst.DeleteAccount:
                        Security.DeleteAccount(Session.Current);
                        break;
                    
                    case MenuConst.AddAnimal:
                        Console.Clear();
                        new Animal().Init(-(Config.FormWidth / 2 + 1));
                        break;
                    
                    case MenuConst.AddProduct:
                        Console.Clear();
                        new Product().Init(-(Config.FormWidth / 2 + 1));
                        break;
                    
                    case MenuConst.AddWorker:
                        Console.Clear();
                        new Worker().Init();
                        break;
                    
                    case MenuConst.AddAnimalName:
                        Console.Clear();
                        new AnimalName().Init();
                        break;
                    
                    case MenuConst.AddJobTitle:
                        Console.Clear();
                        new JobTitle().Init();
                        break;
                    
                    case MenuConst.AddProductType:
                        Console.Clear();
                        new ProductType().Init();
                        break;
                    
                    case MenuConst.DeleteAnimal:
                        AnimalYard.DeleteItem(db.Animals.ToList());
                        break;
                    
                    case MenuConst.DeleteProduct:
                        AnimalYard.DeleteItem(db.Products.ToList());
                        break;
                    
                    case MenuConst.DeleteJobTitle:
                        AnimalYard.DeleteItem(db.JobTitles.ToList());
                        break;
                    
                    case MenuConst.DeleteWorker:
                        AnimalYard.DeleteItem(db.Workers.ToList());
                        break;
                    
                    case MenuConst.DeleteAnimalName:
                        AnimalYard.DeleteItem(db.AnimalNames.ToList());
                        break;
                    
                    case MenuConst.DeleteProductType:
                        AnimalYard.DeleteItem(db.ProductTypes.ToList());
                        break;

                    case MenuConst.GetAccountList:
                        AnimalYard.PrintItemInfo(db.Accounts.ToList());
                        break;
                    
                    case MenuConst.GetAnimalList:
                        AnimalYard.PrintItemInfo(db.Animals.ToList());
                        break;
                    
                    case MenuConst.GetProductList:
                        AnimalYard.PrintItemInfo(db.Products.ToList());
                        break;
                    
                    case MenuConst.GetWorkerList:
                        AnimalYard.PrintItemInfo(db.Workers.ToList());
                        break;
                    
                    case MenuConst.GetAnimalNameList:
                        AnimalYard.PrintItemInfo(db.AnimalNames.ToList());
                        break;
                    
                    case MenuConst.GetProductTypeList:
                        AnimalYard.PrintItemInfo(db.ProductTypes.ToList());
                        break;
                    
                    case MenuConst.GetJobTitleList:
                        AnimalYard.PrintItemInfo(db.JobTitles.ToList());
                        break;

                    case MenuConst.GetWorkersByExperience:
                    {
                        TableGen.DrawFrame(Config.FormWidth, 1);
                        
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 4, Config.PosY + 1);
                        Console.Write("Стаж: ");

                        // Ввід необхідного стажу
                        var exp = Input.ReadNumber().value ?? 0;
                        
                        var workersByExperience = AnimalYard.GetWorkersByExperience(exp);

                        if (workersByExperience.Count > 0)
                            AnimalYard.PrintItemInfo(workersByExperience);

                        break;
                    }
                    
                    case MenuConst.GetAnimalsWithProducts:
                        AnimalYard.PrintItemInfo(AnimalYard.GetAnimalsWithProducts());
                        break;

                    case MenuConst.GetAnimalsWithRation:
                    {
                        TableGen.DrawFrame(Config.FormWidth, 1);
                        
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 4, Config.PosY + 1);
                        Console.Write("Раціон: ");

                        // Ввід необхідного стажу
                        var ration = Input.ReadStringValue().value ?? string.Empty;

                        var animalsWithRation = AnimalYard.GetAnimalsWithRation(ration);

                        if (animalsWithRation.Count > 0)
                            AnimalYard.PrintItemInfo(animalsWithRation);

                        break;
                    }

                    case MenuConst.GetProductivityAnimals:
                    {
                        TableGen.DrawFrame(Config.FormWidth, 1);
                        
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 4, Config.PosY + 1);
                        Console.Write("Продуктивність: ");

                        // Ввід необхідного стажу
                        var productive = Input.ReadNumber().value ?? 0;

                        var productivityAnimals = AnimalYard.GetProductivityAnimals(productive);

                        if (productivityAnimals.Count > 0)
                            AnimalYard.PrintItemInfo(productivityAnimals);

                        break;
                    }

                    case MenuConst.CopyAnimalData:
                    {
                        TableGen.DrawFrame(Config.FormWidth, 1);
                        
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 4, Config.PosY + 1);
                        Console.Write("Тип продукції: ");

                        // Ввід необхідного стажу
                        var productType = Input.ReadStringValue().value ?? string.Empty;
                        
                        if (productType != string.Empty)
                            AnimalYard.CopyAnimalData(productType);

                        break;
                    }

                    case MenuConst.GetAnimalWithMaxWeight:
                    {
                        TableGen.DrawFrame(Config.FormWidth, 1);
                        
                        Console.SetCursorPosition(Config.PosX + Config.FormWidth / 4, Config.PosY + 1);
                        Console.Write(AnimalYard.GetAnimalWithMaxWeight().Info);

                        break;
                    }
                    
                    case MenuConst.GetAnimalWithoutWorker:
                        AnimalYard.PrintItemInfo(AnimalYard.GetAnimalWithoutWorker());
                        break;
                    
                    case MenuConst.GetAnimalWithoutProduct:
                        AnimalYard.PrintItemInfo(AnimalYard.GetAnimalWithoutProduct());
                        break;
                }
            
            Console.Clear();
        }
        
        catch (Exception error)
        {
            Console.Clear();
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            Console.Error.WriteLine($"{Text.Colored("Error: ", Color.Red)}: {error.Message}, \n\n" +
                                    $"{Text.Colored("Analyze: ", Color.Yellow)}: {error.StackTrace}");
        }
    }
}
