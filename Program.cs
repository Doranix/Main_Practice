using Main_Practice.Tools;

namespace Main_Practice;

using AccessControl;

class Program
{
    static void Main(string[] args)
    {
        
        Account account = new Account("Quest", "quest", AccountType.Admin);
        
        Security.Register(account);

        Security.Login();
    }
}