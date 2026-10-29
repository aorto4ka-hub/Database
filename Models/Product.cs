namespace Database.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    // Внешний ключ для связи с папкой, в которой лежит продукт
    public Guid? FolderId { get; set; }
    public Folder? Folder { get; set; }

}