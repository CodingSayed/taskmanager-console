using ToDoList.UI;
using ToDoList.Services;
using ToDoList.Data;
using Microsoft.EntityFrameworkCore;

var appDbContext = new AppDbContext();
var itemService = new ItemService(appDbContext);
var consoleHelpers = new ConsoleHelpers();
var itemMenu = new ItemMenu(itemService, consoleHelpers);
var userMenu = new UserMenu(itemMenu);
var loginMenu = new LoginMenu(userMenu);
var registerService = new RegisterService(appDbContext);
var registerMenu = new RegisterMenu(consoleHelpers, registerService);

var mainMenu = new MainMenu(loginMenu, registerMenu);


using var db = new AppDbContext();
db.Database.Migrate();



mainMenu.Show();
