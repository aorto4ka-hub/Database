using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data;

public class DatabaseSeeder
{
    private readonly DatabaseContext _db;

    public DatabaseSeeder(DbContextOptions<DatabaseContext> options)
    {
        _db = new DatabaseContext(options);
    }

    public async Task SeedData()
    {
        //Не проверяем(Не добавляем) данные повторно
        if (_db.Folders.Any())
        {
            return;
        }

        var folder1 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Склад1"
        };

        var folder2 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Склад2",
            ParentId = folder1.Id
        };

        var product1 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Ноутбук",
            Price = 75000,
            FolderId = folder1.Id
        };

        var product2 = new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Мышь",
            Price = 1500,
            FolderId = folder2.Id
        };

        _db.Folders.AddRange(folder1, folder2);
        _db.Products.AddRange(product1, product2);

        await _db.SaveChangesAsync();
    }
}