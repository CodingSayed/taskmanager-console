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

    public TEnum ParseEnumOrReprompt<TEnum>(string label, string? defaultValue = null) where TEnum : struct, Enum
    {
        while (true)
        {
            var raw = PromptString(label, defaultValue);
            var s = raw.Trim();

            if (string.IsNullOrWhiteSpace(s))
                return default; // e.g. None

            s = s.Replace(" ", "").Replace("-", "");
            if (Enum.TryParse<TEnum>(s, true, out var value))
                return value;

            Console.WriteLine($"Invalid value. Allowed: {string.Join(" | ", Enum.GetNames(typeof(TEnum)))}");
        }
    }

    public TEnum? ParseNullableEnumOrSkip<TEnum>(string label) where TEnum : struct, Enum
    {
        var raw = PromptString(label);
        var s = raw.Trim();
        if (string.IsNullOrWhiteSpace(s)) return null;

        s = s.Replace(" ", "").Replace("-", "");
        if (Enum.TryParse<TEnum>(s, true, out var value))
            return value;

        Console.WriteLine($"Invalid value. Allowed: {string.Join(" | ", Enum.GetNames(typeof(TEnum)))}");
        return ParseNullableEnumOrSkip<TEnum>(label); // re-prompt
    }

    public string PromptPassword(string label)
    {
        Console.Write($"{label}: ");
        var password = new System.Text.StringBuilder();
        ConsoleKeyInfo key;

        while ((key = Console.ReadKey(intercept: true)).Key != ConsoleKey.Enter)
        {
            if (key.Key == ConsoleKey.Backspace)
            {
                if (password.Length > 0)
                {
                    password.Length--;
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return password.ToString();
    }
}
