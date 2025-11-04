using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoList.Data;
using ToDoList.Models;


namespace ToDoList.Services;

public class ItemService
{

    private readonly AppDbContext _db;

    public ItemService(AppDbContext db)
    {
        _db = db;
    }

    public List<Item> GetAll() => _db.Items.AsNoTracking().ToList();

    public void Create(int id, string name, string priority, DateTime? deadline, string? description, string status, string type)
    {
        if (_db.Items.Any(x => x.Id == id)) throw new Exception("This id already exists.");

        _db.Items.Add(new Item { Id = id, Name = name, Priority = priority, Description = description, Status = status, Deadline = deadline, Type = type });
        _db.SaveChanges();
    }
    
        
    
}