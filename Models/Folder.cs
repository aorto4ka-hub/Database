namespace Database.Models;

public class Folder
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new();
    
    
    // Ссылка на родительскую папку (может быть null, если это корневая папка)
    public Guid? ParentId { get; set; }
    
}