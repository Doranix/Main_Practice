namespace Main_Practice.DATABASE;

using AccessControl;
using Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tools;

public class DbController : DbContext
{
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Animal> Animals { get; set; }
    public DbSet<AnimalName> AnimalNames { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Animal> AdditionalTable { get; set; }
    
    // Метод для отримання максимальної довжини елемента у масиві
    public static int GetMaxLength<T>(List<T> database) where T : class, IAnimalClass
    {
        return database.Max(item => item.Info.Length);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var loggingPath = AppDomain.CurrentDomain.BaseDirectory + "../../../DATABASE/log.txt";
        var logFileWriter = new StreamWriter(loggingPath, append: true)
        {
            AutoFlush = true // Автоматичне очищення буфера після кожного запису
        };
        
        // Шлях до бази даних
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../DATABASE/AnimalDB.sqlite");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        
        // Увімкнення логування запитів
        optionsBuilder.LogTo(logFileWriter.WriteLine, LogLevel.Error);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>()
            .Property(w => w.BirthDay)
            .HasConversion(v => v.ToString("yyyy-MM-dd"), v => DateTime.Parse(v)); // Формат ISO 8601

        modelBuilder.Entity<Worker>()
            .Property(w => w.Gender)
            .HasConversion(
                v => v.ToString(), 
                v => (Gender?)Enum.Parse(typeof(Gender), v)); // Збереження Gender як TEXT
    }
}
