using Main_Practice.Animals;
using Main_Practice.Configuration;
using Main_Practice.DATABASE;

namespace Main_Practice;

using AccessControl;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Підгружаєм конфігурацію
            Config.LoadConfig();
            
            using (var db = new DbController())
            {
                db.Database.EnsureCreated();
            }
            
            var account = new Account("Quest", "quest", AccountType.Admin);
            
            Security.EditAccountInfo(account);
        }
        
        catch (Exception error)
        {
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            Console.WriteLine(error.Message);
        }
    }
}