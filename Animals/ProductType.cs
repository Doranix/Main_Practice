using System.ComponentModel.DataAnnotations.Schema;

namespace Main_Practice.Animals;

using System.ComponentModel.DataAnnotations;

public class ProductType : IAnimalClass
{
    [Required] [MaxLength(20)]
    public string Name { get; set; }

    // Властивість для ідентифікатора Виду продукції
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    // Конструктор без параметрів
    public ProductType()
    {
        Name = string.Empty;
    }
    
    // Конструктор за параметрами
    public ProductType(string name)
    {
        Name = name;
    }
    
    // Конструктор копіювання
    public ProductType(ProductType productType)
    {
        Name = productType.Name;
        Id = productType.Id;
    }
    
    // Властивість для інформації про вид продукції
    public string Info => $"{Name} ({Id})";
}