using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoList.Models;


namespace ToDoList.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Item> Items => Set<Item>();

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
        modelBuilder.Entity<User>()
        .HasIndex(u => u.UserName)
        .IsUnique();

        modelBuilder.Entity<Item>()
        .HasOne(i => i.User)
        .WithMany(u => u.Items)
        .HasForeignKey(i => i.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Item>()
            .HasIndex(i => new { i.UserId, i.UserTaskNumber })
            .IsUnique();

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