namespace Main_Practice.Animals;

using System.ComponentModel.DataAnnotations.Schema;
using Configuration;
using DATABASE;
using Tools;
using System.ComponentModel.DataAnnotations;

public partial class AnimalName : IAnimalClass
{
    [MaxLength(20)][Required] public string Name { get; set; }
    [MaxLength(20)][Required] public string Ration { get; set; }

    // Властивість для ідентифікатора імені тварини
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    // Конструктор без параметрів
    public AnimalName()
    {
        Name = string.Empty;
        Ration = string.Empty;
    }
    
    // Конструктор з параметрами
    public AnimalName(string name, string ration)
    {
        Name = name;
        Ration = ration;
    }
    
    // Конструктор копіювання
    public AnimalName(AnimalName animalName)
    {
        Name = animalName.Name;
        Ration = animalName.Ration;
        Id = animalName.Id;
    }
    
    
    
    // Властивість для інформації про ім'я тварини
    [NotMapped]
    public string Info
    {
        get => $"{Name}, {Ration}";
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Вхідна інформація не може бути порожньою, або містити лише пробіли !");
            
            var infoParts = value.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            if (infoParts.Length == 2)
            {
                Name = infoParts[0].Trim();
                Ration = infoParts[1].Trim();
            }

            else throw new ArgumentException("Невірний формат для запису інформації");
        }
    }
}
