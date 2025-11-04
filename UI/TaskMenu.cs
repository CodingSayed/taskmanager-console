using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.UI;

public class ItemMenu
{
    Item item = new Item();
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
        var id = _consoleHelpers.PromptInt("Id");
        var name = _consoleHelpers.PromptString("Name");
        var priority = _consoleHelpers.PromptString("Priority (High | Medium | Low)");
        var status  = _consoleHelpers.PromptString("Status (Finished | In Progress | On Hold)");

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
        var type = _consoleHelpers.PromptString("Type (Single | Multi)");


        _itemService.Create(id, name, priority, status, deadline, description, type);

        return item;
    }

    public void List()
    {
        string firstRow = $"| {"#",-4}| {"Name",-30}| {"Priority",-10}| {"Status",-15}| {"Deadline",-15}|";
        Console.WriteLine($"Today is {DateTime.Today.DayOfWeek} {DateOnly.FromDateTime(DateTime.Today)}\n");
        Console.WriteLine(_consoleHelpers.GetDashes(firstRow));
        Console.WriteLine(firstRow);
        Console.WriteLine(_consoleHelpers.GetDashes(firstRow));

        var items = _itemService.GetAll();
        if (items.Count == 0) Console.WriteLine("No items Found");

        foreach (var item in items)
        {
            Console.WriteLine($"| {item.Id,-4}| {item.Name,-30}| {item.Priority,-10}| {item.Status,-15}| {item.Deadline,-15:dd/MM/yyyy}|");

        }
        Console.WriteLine(_consoleHelpers.GetDashes(firstRow));
    }

    public void Update()
    {
        Console.Clear();
        List();
        var id = _consoleHelpers.PromptInt("Id");
        var name = _consoleHelpers.PromptString("Name");
        var priority = _consoleHelpers.PromptString("Priority (High | Medium | Low)");
        var status = _consoleHelpers.PromptString("Status (Finished | In Progress | On Hold)");

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
        var type = _consoleHelpers.PromptString("Type (Single | Multi)");

        _itemService.Update(id, name, priority, status, deadline, description, type);
    }
    
    public void Delete()
    {
        Console.Clear();
        List();
        var id = _consoleHelpers.PromptInt("Id");
        if (!_consoleHelpers.Confirm("Are you sure you want to delete remove this?")) return;
            
        _itemService.Delete(id);
        Console.WriteLine($"Task with id: {id} has been deleted");
        Console.ReadLine();
    }
}