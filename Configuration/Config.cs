namespace Main_Practice.Configuration;

using System.Text.Json;

public static class Config
{
    private struct ConfigData
    {
        public short FormWidth { get; set; }            // Ширина форми
        public short FormHeight { get; set; }           // Висота форми
        public string AllowedCharacters { get; set; }   // Дозволені символи для вводу
        public short MaxLength { get; set; }            // Максимальна довжина значення для вводу
        public byte PosX { get; set; }                  // Позиція Форми по горизонталі
        public byte PosY { get; set; }                  // Позиція Форми по вертикалі
        public byte MaxElToForm { get; set; }           // Максимальна кількість елементів у формі

        public ConfigData()
        {
            FormWidth = 0;
            FormHeight = 0;
            AllowedCharacters = string.Empty;
            MaxLength = 0;
            MaxElToForm = 0;
        }
        
        public ConfigData(short formWidth, short formHeight, string allowedCharacters, short maxLength, byte maxElToForm)
        {
            FormWidth = formWidth;
            FormHeight = formHeight;
            AllowedCharacters = allowedCharacters;
            MaxLength = maxLength;
            MaxElToForm = maxElToForm;
        }
    };
    
    
    // Змінна для зберігання поточних конфігураційних даних
    private static ConfigData _configData;
    
    
    // Шлях до конфігураційного файлу
    private static readonly string ConfigPath = "../../../Configuration/Config.json";
    
    
    // Завантаження конфігураційних даних
    public static void LoadConfig()
    {
        var jsonString = File.ReadAllText(ConfigPath);
        _configData = JsonSerializer.Deserialize<ConfigData>(jsonString);
    }
    
    
    // Збереження конфігураційних даних
    private static void SaveConfig()
    {
        var jsonString = JsonSerializer.Serialize(_configData, new JsonSerializerOptions() { WriteIndented = true });
        File.WriteAllText(ConfigPath, jsonString);
    }
    
    
    // Властивість для роботи із шириною форми
    public static short FormWidth
    {
        get => _configData.FormWidth;
        set
        {
            _configData.FormWidth = value;
            SaveConfig();
        }
    }
    
    
    // Властивість для роботи із висотою форми
    public static short FormHeight
    {
        get => _configData.FormHeight;
        set
        {
            _configData.FormHeight = value;
            SaveConfig();
        }
    }
    
    
    // Властивість для роботи із дозволеними символами для вводу
    public static string AllowedCharacters
    {
        get => _configData.AllowedCharacters;
        set
        {
            _configData.AllowedCharacters = value;
            SaveConfig();
        }
    }
    
    
    // Властивість для роботи із максимальною довжиною для вводу
    public static short MaxLength
    {
        get => _configData.MaxLength;
        set
        {
            _configData.MaxLength = value;
            SaveConfig();
        }
    }
    
    
    // Властивість для роботи із позицією X
    public static byte PosX
    {
        get => _configData.PosX;
        set
        {
            _configData.PosX = value;
            SaveConfig();
        }
    }
    
    
    // Властивість для роботи із позицією Y
    public static byte PosY
    {
        get => _configData.PosY;
        set
        {
            _configData.PosY = value;
            SaveConfig();
        }
    }
    
    
    // Властивість для роботи із максимальною кількістю елементів у формі
    public static byte MaxElToForm
    {
        get => _configData.MaxElToForm;
        set
        {
            _configData.MaxElToForm = value;
            SaveConfig();
        }
    }
}