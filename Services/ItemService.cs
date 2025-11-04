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

    public void Create(int id, string name, string priority,  string status, DateTime? deadline, string? description, string type)
    {
        if (_db.Items.Any(x => x.Id == id)) throw new Exception("This id already exists.");

        _db.Items.Add(new Item { Id = id, Name = name, Priority = priority, Status = status, Description = description, Deadline = deadline, Type = type });
        _db.SaveChanges();
    }

    public void Update(int id, string? name, string? priority, string? status, DateTime? deadline, string? description, string? type)
    {
        var item = _db.Items.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception("Item does not exist");

        if (!string.IsNullOrWhiteSpace(name)) item.Name = name;
        if (!string.IsNullOrWhiteSpace(priority)) item.Priority = priority;
        if (!string.IsNullOrWhiteSpace(status)) item.Status = status;
        if (deadline.HasValue) item.Deadline = deadline;
        if (!string.IsNullOrWhiteSpace(description)) item.Description = description;
        if (!string.IsNullOrWhiteSpace(type)) item.Type = type;
        _db.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var item = _db.Items.FirstOrDefault(x => x.Id == id) ?? throw new Exception("Item not found");
        _db.Items.Remove(item);
        _db.SaveChanges();
    }   
}