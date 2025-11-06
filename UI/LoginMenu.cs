using ToDoList.Services;

namespace ToDoList.UI;

public class LoginMenu
{
    private readonly LoginService _loginService;
    private readonly ConsoleHelpers _consoleHelpers;
    private readonly UserMenu _userMenu;
   
    public LoginMenu(LoginService loginService, ConsoleHelpers consoleHelpers, UserMenu userMenu)
    {
        _loginService = loginService;
        _consoleHelpers = consoleHelpers;
        _userMenu = userMenu; 
    }
    
   public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("== Login ==\n");

            var userName = _consoleHelpers.PromptString("Username");
            var password = _consoleHelpers.PromptPassword("Password");

            if (_loginService.Login(userName, password, out var error))
            {
                Console.WriteLine($"\nWelcome, {userName}!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();

                _userMenu.Show();
                return;
            }

            Console.WriteLine($"\n {error}");
            if (!_consoleHelpers.Confirm("Try again?", defaultYes: true))
                return;
        }
    }
}