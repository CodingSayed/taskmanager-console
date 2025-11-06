using ToDoList.Services;

namespace ToDoList.UI;

public class UserMenu
{
    private readonly ItemMenu _itemMenu;
    private readonly Session _session;
   
    public UserMenu(ItemMenu itemMenu, Session session)
    {
        _itemMenu = itemMenu;
        _session = session;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            var user = _session.CurrentUser?.UserName ?? "Unknown";
            Console.WriteLine($"== Welcome {user} ==\n");
            Console.WriteLine("1) Task Management");
            Console.WriteLine("2) User Settings");
            Console.WriteLine("0) Exit");

            var input = (Console.ReadLine() ?? "").Trim();

            switch (input)
            {
                case "1": _itemMenu.Show(); break;
                case "2": Console.WriteLine("Welcome to User Settings (TO BE IMPLEMENTED)"); Console.ReadLine(); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }
    }
}