using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Item> Items => Set<Item>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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