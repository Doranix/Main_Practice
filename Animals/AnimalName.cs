namespace Main_Practice.Animals;

public class AnimalName : IAnimalClass
{
    private string _name;
    private string _ration;
    private readonly int _id;

    // Конструктор без параметрів
    public AnimalName()
    {
        _name = string.Empty;
        _ration = string.Empty;
        _id = AccessControl.Security.GenId;
    }
    
    // Конструктор з параметрами
    public AnimalName(string name, string ration)
    {
        _name = name;
        _ration = ration;
        _id = AccessControl.Security.GenId;
    }
    
    // Конструктор копіювання
    public AnimalName(AnimalName animalName)
    {
        _name = animalName._name;
        _ration = animalName._ration;
        _id = animalName._id;
    }
    
    // Властивість для інформації про ім'я тварини
    public string Info
    {
        get => $"{_name}, {_ration}";
        set => (_name, _ration) = (value.Split(' ')[0], value.Split(' ')[1]);
    }
    
    // Властивість для ідентифікатора імені тварини
    public int Id
    {
        get => _id;
    }
}