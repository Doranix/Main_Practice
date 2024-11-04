using System.ComponentModel.DataAnnotations.Schema;

namespace Main_Practice.Animals;

using System.ComponentModel.DataAnnotations;

public class JobTitle : IAnimalClass
{
    [Required]
    [MaxLength(20)]
    public string Title { get; set; }

    // Властивість для ідентифікатора посади
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    // Конструктор без параметрів
    public JobTitle()
    {
        Title = string.Empty;
    }
    
    // Конструктор з параметрами
    public JobTitle(string title)
    {
        Title = title;
    }
    
    // Конструктор копіювання
    public JobTitle(JobTitle jobTitle)
    {
        Title = jobTitle.Title;
        Id = jobTitle.Id;
    }
    
    // Властивість для отримання назви Посади
    public string Info => $"Посада: {Title}";
}