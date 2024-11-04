namespace Main_Practice.Animals;

using DATABASE;
using Tools;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Worker : IAnimalClass
{
    [Required][MaxLength(20)] public string Name {get; set;}
    [Required][MaxLength(20)] public string SurName {get; set;}
    [Required][MaxLength(20)] public string MiddleName {get; set;}
    [Required] public DateTime BirthDay {get; set;}
    [Required] public Gender? Gender {get; set;}
    [Required] public double Salary {get; set;}
    [Required] public int WorkExperience {get; set;}
    [Required][ForeignKey("JobTitleId")] public int? JobTitleId {get; set;}

    // Властивість для отримання ідентифікатора робітника
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    // Конструктор без параметрів
    public Worker()
    {
        Name = string.Empty;
        SurName = string.Empty;
        MiddleName = string.Empty;
        BirthDay = DateTime.Now;
        Gender = null;
        Salary = 0;
        WorkExperience = 0;
        JobTitleId = null;
    }
    
    // Конструктор із параметрами
    public Worker(string name, string surname, string middlename, string birthday, Gender gender, double salary,
        int workExperience, int jobTitleId)
    {
        Name = name;
        SurName = surname;
        MiddleName = middlename;
        BirthDay = DateTime.Parse(birthday);
        Gender = gender;
        Salary = salary;
        WorkExperience = workExperience;
        JobTitleId = jobTitleId;
    }
    
    // Конструктор копіювання
    public Worker(Worker worker)
    {
        Name = worker.Name;
        SurName = worker.SurName;
        MiddleName = worker.MiddleName;
        BirthDay = worker.BirthDay;
        Gender = worker.Gender;
        Salary = worker.Salary;
        WorkExperience = worker.WorkExperience;
        JobTitleId = worker.JobTitleId;
        Id = worker.Id;
    }
    
    // Властивість для повного імені працівника
    public string FullName
    {
        get => $"{Name} {SurName} {MiddleName}";
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Повне імя робітника не може бути пустим, або містити одні пробіли !");
            
            string[] parts = value.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3)
            {
                Name = parts[0].Trim();
                SurName = parts[1].Trim();
                MiddleName = parts[2].Trim();
            }

            else throw new ArgumentException("Невірний формат вхідних даних для імені робітника !");
        }
    }
    
    // Властивість для виводу інформації про працівника
    public string Info
    {
        get
        {
            using var db = new DbController();
            
            StringBuilder info = new StringBuilder();
            info.Append(FullName + " ");
            
            info.Append(db.Workers.Join(db.JobTitles, w => w.JobTitleId, jt => jt.Id, (w, jt) => 
                new { id = w.Id, info = jt.Info }).FirstOrDefault(el => el.id == Id)?.info);
            
            return info.ToString();
        }
    }
}