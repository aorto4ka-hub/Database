using Database.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Database.Models;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<DatabaseSeeder>();
//Регистрируем InMemory базу данных в контейнере зависимостей
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
//Подключаем OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

string json = JsonSerializer.Serialize(employee);
File.WriteAllText("employee.json",json);
Console.WriteLine(json);

//Блок автоматического сидинга данных
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}

// Настройка конвеера запросов
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var group = app.MapGroup("/m");

group.MapGet("/hello", () => "Hello, World!");

app.Run();
