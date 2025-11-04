namespace ToDoList.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public Priority? PriorityLevel { get; set; }
    public Status? CurrentStatus { get; set; }
    public DateTime? Deadline { get; set; }
    public string? Description { get; set; }
    public ItemKind? Kind { get; set; }


    public enum Priority { None, High, Medium, Low }
    public enum Status { None, Finished, InProgress, OnHold }
    public enum ItemKind { Single, Multi }

}

