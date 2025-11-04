namespace ToDoList.Services;

public class ConsoleHelpers
{
    public string PromptString(string label, string? defaultValue = null)
    {
        Console.Write($"{label}{(defaultValue == null ? "" : defaultValue)}: ");
        var s = Console.ReadLine();
        return string.IsNullOrWhiteSpace(s) ? (defaultValue ?? "") : s.Trim();
    }
}