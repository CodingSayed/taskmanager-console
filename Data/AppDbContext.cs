using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoList.Models;


namespace ToDoList.Data;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("Default") ?? "Data Source=app.db";
        optionsBuilder.UseSqlite(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
        .Property(i => i.PriorityLevel)
        .HasConversion<string>();

        modelBuilder.Entity<Item>()
            .Property(i => i.CurrentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Item>()
            .Property(i => i.Kind)
            .HasConversion<string>();
    }
    


}