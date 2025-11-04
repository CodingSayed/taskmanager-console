namespace ToDoList.Services;

public class ConsoleHelpers
{
    public string PromptString(string label, string? defaultValue = null)
    {
        Console.Write($"{label}{(defaultValue == null ? "" : defaultValue)}: ");
        var s = Console.ReadLine();
        return string.IsNullOrWhiteSpace(s) ? (defaultValue ?? "") : s.Trim();
    }

    public bool Confirm(string label, bool defaultYes = true)
    {
        var suffix = defaultYes ? " [Y/n]" : " [y/N]";
        while (true)
        {

            Console.WriteLine($"{label}{suffix}: ");
            var s = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(s)) return defaultYes;
            if (s == "y" | s == "yes") return true;
            if (s == "n" | s == "no") return false;
        }
    }
}