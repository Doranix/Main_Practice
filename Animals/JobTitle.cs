

namespace Main_Practice.Animals;

public class JobTitle : IAnimalClass
{
    private string _title;
    private int _id;
    
    // Конструктор без параметрів
    public JobTitle()
    {
        _title = string.Empty;
        _id = AccessControl.Security.GenId;
    }
    
    // Конструктор з параметрами
    public JobTitle(string title)
    {
        _title = title;
        _id = AccessControl.Security.GenId;
    }
    
    // Конструктор копіювання
    public JobTitle(JobTitle jobTitle)
    {
        _title = jobTitle._title;
        _id = jobTitle._id;
    }
    
    // Властивість для ідентифікатора посади
    public int Id => _id;
    
    // Властивість для отримання назви Посади
    public string Info
    {
        get => _title;
        set => _title = value;
    }
}