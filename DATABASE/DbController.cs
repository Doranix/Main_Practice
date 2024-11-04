namespace Main_Practice.DATABASE;

using Tools;
using AccessControl;
using Configuration;
using Animals;
using Microsoft.EntityFrameworkCore;

public class DbController : DbContext
{
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalName> AnimalNames { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Account> Accounts { get; set; }

    
    
    
    // Метод для отримання максимальної довжини елемента у масиві
    public static int GetMaxLength<T>(DbSet<T> database) where T : class, IAnimalClass
    {
        return database.AsEnumerable().Max(item => item.Info.Length);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../DATABASE/AnimalDB.sqlite");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}