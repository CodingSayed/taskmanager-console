using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoList.Data;
using ToDoList.Models;


namespace ToDoList.Services;

public class ItemService
{

    private readonly AppDbContext _db;
    private readonly Session _session;

    public ItemService(AppDbContext db, Session session)
    {
        _db = db;
        _session = session;
    }

    private int RequireUserId() => _session.CurrentUser?.Id ?? throw new Exception("Not logged in");


    public List<Item> GetAll()
    {
        var userId = RequireUserId();
        return _db.Items
            .AsNoTracking()
            .Where(i => i.UserId == userId)
            .OrderBy(i => i.UserTaskNumber)
            .ToList();
    } 

    public Item Create(string name,  Item.Priority priority,  Item.Status status, DateTime? deadline, string? description, Item.ItemKind kind)
    {
        var userId = RequireUserId();

        var nextNumber = (_db.Items
           .Where(i => i.UserId == userId)
           .Select(i => (int?)i.UserTaskNumber)
           .Max() ?? 0) + 1;


        var item = new Item
        {
            UserId = userId,
            UserTaskNumber = nextNumber,
            Name = name,
            PriorityLevel = priority,
            CurrentStatus = status,
            Deadline = deadline,
            Description = description,
            Kind = kind
        };

        _db.Items.Add(item);
        _db.SaveChanges();
        return item;
    }

    public bool Update(int userTaskNumber, string? name, Item.Priority? priority, Item.Status? status, DateTime? deadline, string? description, Item.ItemKind? kind)
    {
        var userId = RequireUserId();

        var item = _db.Items.FirstOrDefault(x => x.UserId == userId && x.UserTaskNumber == userTaskNumber);
        if (item == null) return false;

        if (!string.IsNullOrWhiteSpace(name)) item.Name = name;
        if (priority.HasValue) item.PriorityLevel = priority.Value;
        if (status.HasValue) item.CurrentStatus = status.Value;
        if (deadline.HasValue) item.Deadline = deadline;
        if (!string.IsNullOrWhiteSpace(description)) item.Description = description;
        if (kind.HasValue) item.Kind = kind.Value;

        _db.SaveChanges();
        return true;
    }
    
    public bool Delete(int userTaskNumber)
    {
        var userId = RequireUserId();

        var item = _db.Items.FirstOrDefault(i => i.UserId == userId && i.UserTaskNumber == userTaskNumber);
        if (item == null) return false;

        _db.Items.Remove(item);
        _db.SaveChanges();
        return true;
    }   
}