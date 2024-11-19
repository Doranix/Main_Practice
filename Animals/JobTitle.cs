using System.ComponentModel.DataAnnotations.Schema;
using Main_Practice.Configuration;
using Main_Practice.DATABASE;
using Main_Practice.Tools;

namespace Main_Practice.Animals;

using System.ComponentModel.DataAnnotations;

public partial class JobTitle : IAnimalClass
{
    [MaxLength(20)][Required]
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
    [NotMapped]
    public string Info => $"Посада: {Title}";
}
