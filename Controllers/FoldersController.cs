using Database.Data;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Database.Controllers;

// [HttpPost] - Create
//     [HttpGet] - Read
//     [HttpPut] - Update
//     [HttpDelete] - Delete
//Этот атрибут говорит .Net, что данный класс - это контроллер для API
[ApiController]
//Задает базовый адрес ля запроса. [controller] автоматически превратится в имя класса 
//без слова Controller
[Route("api/[controller]")]

public class FoldersController : ControllerBase
{
    private readonly DatabaseContext _context;
    
    //Через конструктор просим .NET дать нам контекст базы данных
    //(Dependency Injection(Внедрение зависимостей))
    public FoldersController(DatabaseContext context)
    {
        _context = context;
    }
    
    //Этот метод сработает, когда пошлет обычный Get-запрос на адресс /api/folders
    [HttpGet]
    public async Task<IActionResult> GetTreeData()
    {
        //загружаем из базы данных все папки
        var folders = await _context.Folders.ToListAsync();
        
        //загружаем из базы данных все продукты
        var products = await _context.Products.ToListAsync();
        
        // Формируем анонимный объект ответа, который .NET автоматически превратит в JSON.
        // Передаем отдельно плоский список папок и плоский список продуктов.
        // Клиент сам соберет их в дерево с помощью своего TreeBuilder.
        return Ok(new
        {
            Folders = folders,
            Products = products
        });
    }

    [HttpGet("Hello")]
    public string SayHello([FromQuery] string val)
    {
        return "Hello";
    }

    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create([FromBody] CreateFolderRequest request)
    {
        return "";
    }
    
}