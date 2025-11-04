using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.UI;

public class TaskMenu
{
    Item item = new Item();
    private readonly ItemService _itemService;
    

    public TaskMenu(ItemService itemService)
    {
        _itemService = itemService;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            List();
            Console.WriteLine("1) Create task");
            Console.WriteLine("2) Edit task");
            Console.WriteLine("3) Delete task");
            Console.WriteLine("0) Exit");


            var input = (Console.ReadLine() ?? "").Trim();

            switch (input)
            {
                case "1": Create(); break;
                case "2": Console.WriteLine("Edit task PH "); break;
                case "3": Console.WriteLine("Delete task PH"); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }
    }
    
    public string Prompt(string label, string? defaultValue = null)
    {
        Console.WriteLine($"{label}{(defaultValue is null ? "" : $" [{defaultValue}]: ")}");
        var s = Console.ReadLine();
        return string.IsNullOrWhiteSpace(s) ? (defaultValue ?? "") : s.Trim();

    }

    public Item Create()
    {
        var id = int.Parse(Prompt("Id"));
        var name = Prompt("Name");
        var priority = Prompt("Priority (High | Medium | Low)");
        var deadline = DateTime.Now.Date;
        var description = Prompt("Description");
        var type = Prompt("Type (Single | Multi)");

        _itemService.Create(id, name, priority, deadline, description, "On Hold", type);

        return item;
    }

    public void List()
    {
        Console.WriteLine($"Today is {DateTime.Today.DayOfWeek} {DateOnly.FromDateTime(DateTime.Today)}\n");
        Console.WriteLine($"{"#",-4}| {"Name",-30}| {"Priority",-10}| {"Status",-10}| {"Deadline"}");
        Console.WriteLine("------------------------------------------------------------------------------------");

        var items = _itemService.GetAll();
        if (items.Count == 0) Console.WriteLine("No items Found");

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Id,-4}| {item.Name,-30}| {item.Priority,-10}| {item.Status,-10}| {item.Deadline}");

        }
        Console.WriteLine("\n");  
    }
    
}