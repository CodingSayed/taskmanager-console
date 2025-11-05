using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;


namespace ToDoList.Services;


public class RegisterService
{
    private readonly AppDbContext _db;
    public RegisterService(AppDbContext db)
    {
        _db = db;
    }

    public bool UsernameAvailable(string username) => !_db.Users.Any(u => u.UserName == username);



    public User Register(string username, string? firstName, string? lastName, string password)
    {
        username = (username ?? "").Trim();
        firstName = (firstName ?? "").Trim();
        lastName = (lastName ?? "").Trim();

        ValidateUsername(username);
        ValidatePassword(password);


        if (!UsernameAvailable(username)) throw new Exception($"The username ${username} already exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            UserName = username,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = hash
        };

        _db.Users.Add(user);
        _db.SaveChanges();
        return user;
    }

    private void ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) throw new Exception("Username is required");
        if (username.Length < 4 || username.Length > 32) throw new Exception("Username should be 4-32 characters");
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9._-]+$")) throw new Exception("Username may only contain letters, digits, dot, dash, and underscore");
    }
    
    private void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) throw new Exception("Password is required");
        if (password.Length < 6) throw new Exception("Password must be at least 6 characters");
    }

}