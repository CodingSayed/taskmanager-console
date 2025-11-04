using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.UI;

public class TaskMenu
{
    Item item = new Item();
    private readonly ItemService _itemService;
    private readonly ConsoleHelpers _consoleHelpers;

    public TaskMenu(ItemService itemService, ConsoleHelpers consoleHelpers)
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
                case "3": Console.WriteLine("Delete task PH"); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }
    }

    public Item Create()
    {
        var id = int.Parse(_consoleHelpers.PromptString("Id"));
        var name = _consoleHelpers.PromptString("Name");
        var priority = _consoleHelpers.PromptString("Priority (High | Medium | Low)");
        var status  = _consoleHelpers.PromptString("Status (Finished | In Progress | On Hold)");
        var deadline = DateTime.Now.Date;
        var description = _consoleHelpers.PromptString("Description");
        var type = _consoleHelpers.PromptString("Type (Single | Multi)");

        _itemService.Create(id, name, priority, status, deadline, description, type);

        return item;
    }

    public void List()
    {
        Console.WriteLine($"Today is {DateTime.Today.DayOfWeek} {DateOnly.FromDateTime(DateTime.Today)}\n");
        Console.WriteLine($"{"#",-4}| {"Name",-30}| {"Priority",-10}| {"Status",-15}| {"Deadline"}");
        Console.WriteLine("------------------------------------------------------------------------------------");

        var items = _itemService.GetAll();
        if (items.Count == 0) Console.WriteLine("No items Found");

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Id,-4}| {item.Name,-30}| {item.Priority,-10}| {item.Status,-15}| {item.Deadline}");

        }
        Console.WriteLine("\n");
    }
    
    public void Update()
    {
        Console.Clear();
        List();
        var id = int.Parse(_consoleHelpers.PromptString("Id"));
        var name = _consoleHelpers.PromptString("Name");
        var priority = _consoleHelpers.PromptString("Priority (High | Medium | Low)");
        var status  = _consoleHelpers.PromptString("Status (Finished | In Progress | On Hold)");
        var deadline = DateTime.Now.Date;
        var description = _consoleHelpers.PromptString("Description");
        var type = _consoleHelpers.PromptString("Type (Single | Multi)");

        _itemService.Update(id, name, priority, status, deadline, description, type);
    }
    
}