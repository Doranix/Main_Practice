using System.ComponentModel.DataAnnotations.Schema;

namespace Main_Practice.Animals;

using DATABASE;
using System.Text;
using System.ComponentModel.DataAnnotations;

public partial class Product : IAnimalClass
{
    [ForeignKey("AnimalId")]
    [Required]
    public int AnimalId { get; set; }
    
    [ForeignKey("ProductTypeId")]
    [Required]
    public int ProductTypeId {get; set;}
    
    [Required] 
    public double AveragePerfomance {get; set;}
    
    // Властивість для ідентифікатора продукції
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    
    // Конструктор без параметрів
    public Product()
    {
        AnimalId = -1;
        ProductTypeId = -1;
        AveragePerfomance = 0;
    }
    
    
    // Конструктор з параметрами
    public Product(int animalId, int productTypeId, double averagePerfomance)
    {
        AnimalId = animalId;
        ProductTypeId = productTypeId;
        AveragePerfomance = averagePerfomance;
    }
    
    
    // Конструктор копіювання
    public Product(Product product)
    {
        AnimalId = product.AnimalId;
        ProductTypeId = product.ProductTypeId;
        AveragePerfomance = product.AveragePerfomance;
        Id = product.Id;
    }
    
    // Властивість для інформації про продукцію
    [NotMapped]
    public string Info
    {
        get
        {
            var info = new StringBuilder();

            using (var db = new DbController())
            {
                info.Append(db.Products.Join(db.ProductTypes, product => product.ProductTypeId,
                    productType => productType.Id, (product, productType) =>
                        new { product.Id, productType.Info }).FirstOrDefault(el => el.Id == Id)?.Info);

                info.Append(", Тварина: " + db.Products.Join(db.Animals, product => product.AnimalId, animal => animal.Id, (product, animal) => 
                    new { product.Id, animal.Info }).FirstOrDefault(el => el.Id == Id)?.Info.Split(", Працівник: ")[0]);
                
                info.Append(", Середня продуктивність: " + AveragePerfomance);
            }
            
            return info.ToString();
        }
    }
}
