using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Main_Practice.DATABASE;

namespace Main_Practice.Animals;

using System.ComponentModel.DataAnnotations;

[Table("Animals")]
public class Animal
{
    [ForeignKey("AnimalNameId")]
    [Required]
    public int AnimalNameId { get; set; }
    
    [ForeignKey("WorkerId")]
    [Required]
    public int WorkerId { get; set; }
    
    [Required]
    public int Age { get; set; }
    
    [Required]
    public double Weight { get; set; }

    // Властивість для ідентифікатора тварини
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    // Конструктор без параметрів
    public Animal()
    {
        AnimalNameId = -1;
        WorkerId = -1;
        Age = 0;
        Weight = 0;
    }
    
    // Конструктор з параметрами
    public Animal(int animalNameId, int workerId, int age, double weight)
    {
        AnimalNameId = animalNameId;
        WorkerId = workerId;
        Age = age;
        Weight = weight;
    }
    
    // Конструктор копіювання
    public Animal(Animal animal)
    {
        AnimalNameId = animal.AnimalNameId;
        WorkerId = animal.WorkerId;
        Age = animal.Age;
        Weight = animal.Weight;
        Id = animal.Id;
    }
    
    // Властивість для отримання інформації про тварину
    public string Info
    {
        get
        {
            StringBuilder info = new StringBuilder();

            using (var db = new DbController())
            {
                info.Append(db.Animals.Join(db.AnimalNames, animal => animal.AnimalNameId, animalName => animalName.Id, (animal, animalName) =>
                    new { animal.Id, animalName.Info }).FirstOrDefault(el => el.Id == Id)?.Info + ", ");

                info.Append("Працівник: " + db.Animals.Join(db.Workers, animal => animal.WorkerId, worker => worker.Id, (animal, worker) => 
                    new { animal.Id, worker.FullName }).FirstOrDefault(el => el.Id == Id)?.FullName);
            }

            return info.ToString();
        }
    }
}