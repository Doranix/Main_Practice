namespace Main_Practice.Animals;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Configuration;
using Tools;
using DATABASE;

public partial class ProductType : IAnimalClass
{
    [MaxLength(20)] [Required]
    public string Name { get; set; }

    // Властивість для ідентифікатора Виду продукції
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    [NotMapped]
    public string Info
    {
        get => Name;
        set => Name = value;
    }
}
