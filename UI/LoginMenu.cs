namespace ToDoList.UI;

public class LoginMenu
{
    public UserMenu _userMenu;
    public LoginMenu(UserMenu userMenu)
    {
        _userMenu = userMenu;
    }
    
    public void Show()
    {
        Console.Clear();
        Console.WriteLine("Login Page (TO DO)\n");
        Console.WriteLine("1) for continue");
        Console.WriteLine("0) Exit");

        var input = (Console.ReadLine() ?? "").Trim();

            switch (input)
            {
                case "1": _userMenu.Show(); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }

    }
}