namespace ToDoList.UI;


public class UserMenu
{
    public ItemMenu _itemMenu;
   
    public UserMenu(ItemMenu itemMenu)
    {   
        _itemMenu = itemMenu;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome John Doe\n");
            Console.WriteLine("1) Task Management");
            Console.WriteLine("2) User Settings");
            Console.WriteLine("0) Exit");

            var input = (Console.ReadLine() ?? "").Trim();

            switch (input)
            {
                case "1": _itemMenu.Show(); break;
                case "2": Console.WriteLine("Welcome to User Settings"); Console.ReadLine(); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }

    }


}