using Main_Practice.Tools;

namespace Main_Practice.Animals;

public class Worker : IAnimalClass
{
    private string _name;
    private string _surName;
    private string _middleName;
    private DateTime _birthDay;
    private Gender? _gender;
    private readonly int _id;
    private double _salary;
    private int _workExperience;
    private readonly int? _jobTitleId;

    // Конструктор без параметрів
    public Worker()
    {
        _name = string.Empty;
        _surName = string.Empty;
        _middleName = string.Empty;
        _birthDay = DateTime.Now;
        _gender = null;
        _id = AccessControl.Security.GenId;
        _salary = 0;
        _workExperience = 0;
        _jobTitleId = null;
    }
    
    // Конструктор із параметрами
    public Worker(string name, string surname, string middlename, string birthday, Gender gender, double salary,
        int workExperience, int jobTitleId)
    {
        _name = name;
        _surName = surname;
        _middleName = middlename;
        _birthDay = DateTime.Parse(birthday);
        _gender = gender;
        _id = AccessControl.Security.GenId;
        _salary = salary;
        _workExperience = workExperience;
        _jobTitleId = jobTitleId;
    }
    
    // Конструктор копіювання
    public Worker(Worker worker)
    {
        _name = worker._name;
        _surName = worker._surName;
        _middleName = worker._middleName;
        _birthDay = worker._birthDay;
        _gender = worker._gender;
        _id = worker._id;
        _salary = worker._salary;
        _workExperience = worker._workExperience;
        _jobTitleId = worker._jobTitleId;
    }
    
    // Властивість для повного імені працівника
    public string FullName
    {
        get => $"{_name} {_surName} {_middleName}";
        set
        {
            string[] parts = value.Split(" ");
            _name = parts[0];
            _surName = parts[1];
            _middleName = parts[2];
        }
    }
    
    // Властивість для дня народження робітника
    public string BirthDay
    {
        get => _birthDay.ToString("dd/MM/yyyy");
        set => _birthDay = DateTime.Parse(value);
    }
    
    // Властивість для статі
    public Gender Gender
    {
        get => _gender ?? Gender.Male;
        set => _gender = value;
    }
    
    // Властивість для отримання ідентифікатора робітника
    public int Id
    {
        get => _id;
    }

    // Властивість для заробітної плати робітника
    public double Salary
    {
        get => _salary;
        set => _salary = value;
    }
    
    // Властивість для досвіду роботи працівника
    public int WorkExperience
    {
        get => _workExperience;
        set => _workExperience = value;
    }
    
    // Властивість для коду посади
    public int JobTitleId
    {
        get => _jobTitleId ?? -1;
    }
    
    // Властивість для виводу інформації про працівника
    public string Info
    {
        get => $"{FullName}, Посада: {AnimalYard.Workers.Join(AnimalYard.JobTitles, worker => worker._jobTitleId, jobTitle => jobTitle.Id, (worker, jobtitle) => (worker._id, jobtitle.Info)).ToList().Find(el => _id == Id).Info}";
    }
}