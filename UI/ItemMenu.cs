using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.UI;

public class ItemMenu
{
    private readonly ItemService _itemService;
    private readonly ConsoleHelpers _consoleHelpers;

    public ItemMenu(ItemService itemService, ConsoleHelpers consoleHelpers)
    {
        _itemService = itemService;
        _consoleHelpers = consoleHelpers;
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
                case "2": Update(); break;
                case "3": Delete(); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }
    }

    public Item Create()
    {
        Console.Clear();
        List();

        var name = _consoleHelpers.PromptString("Name");
        var priority = _consoleHelpers.ParseEnumOrReprompt<Item.Priority>("Priority (High | Medium | Low | None)");
        var status = _consoleHelpers.ParseEnumOrReprompt<Item.Status>("Status (Finished | In Progress | On Hold | None)");

        var deadlineInput = _consoleHelpers.PromptString("Deadline (yyyy-MM-dd)");
        DateTime? deadline = null;
        if (!string.IsNullOrEmpty(deadlineInput))
        {
            DateTime parsedDate;
            while (!DateTime.TryParse(deadlineInput, out parsedDate))
            {
                Console.WriteLine("Invalid date format. Use (yyyy-MM-dd)");
                deadlineInput = _consoleHelpers.PromptString("Deadline (yyyy-MM-dd)");
                if (string.IsNullOrWhiteSpace(deadlineInput)) break;
            }

            if (!string.IsNullOrWhiteSpace(deadlineInput))
            {
                deadline = parsedDate.Date;
            }
            
        }

        var description = _consoleHelpers.PromptString("Description");
        var type = _consoleHelpers.ParseEnumOrReprompt<Item.ItemKind>("Type (Single | Multi)");

        var created = _itemService.Create(name, priority, status, deadline, description, type);
        Console.WriteLine($"\nCreated task #{created.UserTaskNumber}");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        return created;
    }

    public void List()
    {
        string firstRow = $"| {"#",-4}| {"Name",-40}| {"Priority",-10}| {"Status",-15}| {"Deadline",-15}|";
        Console.WriteLine($"== Today is {DateTime.Today.DayOfWeek} {DateOnly.FromDateTime(DateTime.Today)} ==\n");
        Console.WriteLine(_consoleHelpers.GetDashes(firstRow));
        Console.WriteLine(firstRow);
        Console.WriteLine(_consoleHelpers.GetDashes(firstRow));

        var items = _itemService.GetAll();
        if (items.Count == 0) Console.WriteLine("No items Found\n");

        foreach (var item in items)
        {
            Console.WriteLine($"| {item.UserTaskNumber,-4}| {item.Name,-40}| {item.PriorityLevel,-10}| {item.CurrentStatus,-15}| {item.Deadline,-15:dd/MM/yyyy}|");

        }
        Console.WriteLine(_consoleHelpers.GetDashes(firstRow) + "\n");
    }

    public void Update()
    {
        Console.Clear();
        List();
        int taskNumber = _consoleHelpers.PromptInt("Task #");
        string name = _consoleHelpers.PromptString("Name");
        Item.Priority? priority = _consoleHelpers.ParseNullableEnumOrSkip<Item.Priority>("Priority (High | Medium | Low | None) — leave empty to keep");
        Item.Status? status = _consoleHelpers.ParseNullableEnumOrSkip<Item.Status>("Status (Finished | In Progress | On Hold | None) — leave empty to keep");

        string? deadlineInput = _consoleHelpers.PromptString("Deadline (yyyy-MM-dd)");
        DateTime? deadline = null;
        if (!string.IsNullOrEmpty(deadlineInput))
        {
            DateTime parsedDate;
            while (!DateTime.TryParse(deadlineInput, out parsedDate))
            {
                Console.WriteLine("Invalid date format. Use (yyyy-MM-dd)");
                deadlineInput = _consoleHelpers.PromptString("Deadline (yyyy-MM-dd)");
                if (string.IsNullOrWhiteSpace(deadlineInput)) break;
            }

            if (!string.IsNullOrWhiteSpace(deadlineInput))
            {
                deadline = parsedDate.Date;
            }
            
        }
        
        string? description = _consoleHelpers.PromptString("Description");
        Item.ItemKind? type = _consoleHelpers.ParseNullableEnumOrSkip<Item.ItemKind>("Type (Single | Multi) — leave empty to keep");

        bool updated = _itemService.Update(taskNumber, name, priority, status, deadline, description, type);
        Console.WriteLine(updated ? "\nUpdated" : "\nTask not found");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
    
    public void Delete()
    {
        Console.Clear();
        List();
        int taskNumber = _consoleHelpers.PromptInt("Task #");
        if (!_consoleHelpers.Confirm("Are you sure you want to delete remove this?")) return;
            
        bool deleted = _itemService.Delete(taskNumber);
        Console.WriteLine(deleted ? $"Task #{taskNumber} has been deleted" : "Task not found");
        Console.ReadLine();
    }
}