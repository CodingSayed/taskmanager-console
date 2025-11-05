namespace ToDoList.UI;


public class MainMenu
{
    public LoginMenu _loginMenu;
    public RegisterMenu _registerMenu;
   

    public MainMenu(LoginMenu loginMenu, RegisterMenu registerMenu)
    {
        _loginMenu = loginMenu;
        _registerMenu = registerMenu;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the To-Do Application");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("1) Login");
            Console.WriteLine("2) Register");
            Console.WriteLine("0) Exit");

            var input = (Console.ReadLine() ?? "").Trim();

            switch (input)
            {
                case "1": _loginMenu.Show(); break;
                case "2": _registerMenu.Register();  break;
                case "0": return;
                default: Console.WriteLine("Invalid Option"); break;
            }
        }

    }


}