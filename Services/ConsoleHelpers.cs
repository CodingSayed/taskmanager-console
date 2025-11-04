namespace ToDoList.Services;

public class ConsoleHelpers
{
    public string PromptString(string label, string? defaultValue = null)
    {
        Console.Write($"{label}{(defaultValue == null ? "" : defaultValue)}: ");
        var s = Console.ReadLine();
        return string.IsNullOrWhiteSpace(s) ? (defaultValue ?? "") : s.Trim();
    }

    public int PromptInt(string label, int? defaultValue = null, int? min = null, int? max = null)
    {
        while (true)
        {
            var raw = PromptString(label, defaultValue?.ToString());
            if (int.TryParse(raw, out var value) &&
                (min is null || value >= min) &&
                (max is null || value <= max))
                return value;

            Console.WriteLine("Please enter a valid integer value");
        }
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

    public string GetDashes(string s)
    {
        string line = "";
        for (int i = 0; i < s.Length; i++)
        {
            line += "-";
        }
        return line;
    }
}