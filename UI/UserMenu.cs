namespace ToDoList.UI;


public class UserMenu
{
    public TaskMenu _taskMenu;
   
    public UserMenu(TaskMenu taskMenu)
    {   
        _taskMenu = taskMenu;
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
                case "1": _taskMenu.Show(); break;
                case "2": Console.WriteLine("Welcome to User Settings"); Console.ReadLine(); break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }

    }


}