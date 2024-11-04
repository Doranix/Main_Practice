using Main_Practice.AccessControl;

namespace Main_Practice;

using Configuration;
using Animals;
using Tools;

public static class AnimalYard
{
    // Отримати список об'єктів
    public static void PrintItemList<T>(List<T> database, short x = 1, short y = 1, byte distance = 1, int width = 0, int startIndex = 0) where T : class, IAnimalClass
    {
        Console.SetCursorPosition(Config.PosX + x, Config.PosY + y);

        // Вивід елементів
        for (int i = startIndex; 
             i < (database.Count > Config.MaxElToForm
                 ? (startIndex + Config.MaxElToForm)
                 : database.Count); i++)
        {
            // Динамічне центрування виводимих даних
            Console.Write(Text.AlignCenter(database[i].Info, width));
            
            Console.SetCursorPosition(Config.PosX + x, Console.CursorTop + 1 + distance);
        }
    }
    
    
    // Отримати інформацію про об'єкт
    public static void PrintItemInfo(Account account)
    {
        TableGen.DrawFrame(Config.FormWidth, account.AccountType == AccountType.Admin ? 16 : 14);
        
        // Виводимо всі елементи
        Console.SetCursorPosition(Config.PosX + 1, Config.PosY + 1);
    }
}