using ToDoList.Services;

namespace ToDoList.UI;

public class RegisterMenu
{
    public ConsoleHelpers _consoleHelpers;
    public RegisterService _registerService;
    public RegisterMenu(ConsoleHelpers consoleHelpers, RegisterService registerService)
    {
        _consoleHelpers = consoleHelpers;
        _registerService = registerService;
    }
    
    public void Register()
    {
        while (true)
        {
            
            Console.Clear();
            Console.WriteLine("Register");
            Console.WriteLine("Create account\n");

            var username = _consoleHelpers.PromptString("Username");
            var firstName = _consoleHelpers.PromptString("First Name");
            var lastName = _consoleHelpers.PromptString("Last Name");

            var password = _consoleHelpers.PromptPassword("Password");
            var password2 = _consoleHelpers.PromptPassword("Confirm password");

            if (password != password2)
            {
                Console.WriteLine("Passwords do not match. Press a key to continue...");
                Console.ReadLine();
                continue;
            }

            try
            {
                var user = _registerService.Register(username, firstName, lastName, password);
                Console.WriteLine("Account has been created");
                Console.WriteLine("Press a key to continue...");
                Console.ReadLine();
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (!_consoleHelpers.Confirm("Try again?", defaultYes : true)) return;
            }

        }
    }
}