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

    public void Create(int id, string name,  Item.Priority priority,  Item.Status status, DateTime? deadline, string? description, Item.ItemKind kind)
    {
        if (_db.Items.Any(x => x.Id == id)) throw new Exception("This id already exists.");

        _db.Items.Add(new Item { Id = id, Name = name, PriorityLevel = priority, CurrentStatus = status, Description = description, Deadline = deadline, Kind = kind });
        _db.SaveChanges();
    }

    public void Update(int id, string? name, Item.Priority? priority, Item.Status? status, DateTime? deadline, string? description, Item.ItemKind? kind)
    {
        var item = _db.Items.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception("Item does not exist");

        if (!string.IsNullOrWhiteSpace(name)) item.Name = name;
        if (priority.HasValue) item.PriorityLevel = priority;
        if (status.HasValue) item.CurrentStatus = status;
        if (deadline.HasValue) item.Deadline = deadline;
        if (!string.IsNullOrWhiteSpace(description)) item.Description = description;
        if (kind.HasValue) item.Kind = kind;
        _db.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var item = _db.Items.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Item not found");
        _db.Items.Remove(item);
        _db.SaveChanges();
    }   
}