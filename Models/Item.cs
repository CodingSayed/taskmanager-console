namespace ToDoList.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Priority { get; set; } = ""; // High | Medium | Low | {empty}
    public string? Status { get; set; } = "On Hold";  // Finished | In Progress | On Hold | {empty}
    public DateTime? Deadline { get; set; }
    public string? Description { get; set; } = "";

    public string Type { get; set;} =""; // Single | Multi -> Enum???
    //public Item[]? Items{ get; set; } // For multi implement later
}