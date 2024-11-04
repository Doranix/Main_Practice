using Main_Practice.Animals;

namespace Main_Practice.AccessControl;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("Accounts")]
public class Account : IAnimalClass
{
    [Required]
    [Column("Username")]
    public string Username { get; set; }
    
    [Required]
    [Column("Password")]
    public string HashedPassword { get; private set; }
    
    [NotMapped]
    public string Password
    {
        set => HashedPassword = Security.Hashing(value);
    }
    
    // Властивість для ідентифікатора акаунту
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column("AccountType")]
    public AccountType AccountType { get; set; }

    // Конструктор без параметрів
    public Account()
    {
        Username = string.Empty;
        HashedPassword = string.Empty;
    }
    
    // Звичайний конструктор
    public Account(string username, string password, AccountType type)
    {
        Username = username;
        Password = password;
        AccountType = type;
    }
    
    // Метод для перевірки правильності пароля
    public bool ValidatePassword(string password)
    {
        return Security.Hashing(password) == HashedPassword;
    }
    
    // Властивість для виводу інформації про акаунт
    public string Info => $"{Username} : [ {AccountType} ]";
}