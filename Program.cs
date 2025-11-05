using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoList.Data;
using ToDoList.Services;
using ToDoList.UI;
using Microsoft.Extensions.Logging;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
            logging.ClearProviders();
    })
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>((sp, opts) =>
        {
            opts.UseSqlite("Data Source=app.db");
        });

        services.AddSingleton<Session>();

        services.AddSingleton<ConsoleHelpers>();

        services.AddScoped<RegisterService>();
        services.AddScoped<LoginService>();
        services.AddScoped<ItemService>();

        services.AddTransient<ItemMenu>();
        services.AddTransient<UserMenu>();
        services.AddTransient<LoginMenu>();
        services.AddTransient<RegisterMenu>();
        services.AddTransient<MainMenu>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

var main = host.Services.GetRequiredService<MainMenu>();
main.Show();

