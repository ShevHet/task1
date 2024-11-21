using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;


class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient { BaseAddress = new Uri("https://localhost:5002/api/Directories/") };


        // Создание корневой директории
        var root = new { Name = "Root", ParentId = (int?)null };
        var response = await client.PostAsJsonAsync("", root);
        var createdRoot = await response.Content.ReadFromJsonAsync<Directory>();

        Console.WriteLine($"Создана директория: {createdRoot?.Name} с Id: {createdRoot?.Id}");

        // Создание дочерней директории
        var child = new { Name = "Child", ParentId = createdRoot?.Id };
        response = await client.PostAsJsonAsync("", child);
        var createdChild = await response.Content.ReadFromJsonAsync<Directory>();

        Console.WriteLine($"Создана дочерняя директория: {createdChild?.Name} с Id: {createdChild?.Id}");

        // Получение всех директорий
        var directories = await client.GetFromJsonAsync<List<Directory>>("https://localhost:5002/api/Directories/");

        Console.WriteLine("Дерево директорий:");
        if (directories != null)
        {
            foreach (var dir in directories)
            {
                Console.WriteLine($"{dir.Name} (Id: {dir.Id}, ParentId: {dir.ParentId})");
            }
        }
        else
        {
            Console.WriteLine("Список директорий пуст.");
        }
    }
}

public class Directory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
}
