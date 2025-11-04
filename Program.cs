using ToDoList.UI;
using ToDoList.Services;
using ToDoList.Data;
using Microsoft.EntityFrameworkCore;

var appDbContext = new AppDbContext();
var itemService = new ItemService(appDbContext);
var consoleHelpers = new ConsoleHelpers();
var taskMenu = new TaskMenu(itemService, consoleHelpers);
var userMenu = new UserMenu(taskMenu);
var loginMenu = new LoginMenu(userMenu);
var registerMenu = new RegisterMenu();

var mainMenu = new MainMenu(loginMenu, registerMenu);


using var db = new AppDbContext();
db.Database.Migrate();



mainMenu.Show();
