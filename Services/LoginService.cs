using ToDoList.Data;
namespace ToDoList.Services;

public class LoginService
{
    private readonly AppDbContext _db;
    private readonly Session _session;
    public LoginService(AppDbContext db, Session session) { _db = db; _session = session; }

    public bool Login(string userName, string password, out string error)
    {
        error = "";
        var user = _db.Users.FirstOrDefault(u => u.UserName == userName);
        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        { error = "Invalid username or password"; return false; }

        _session.Set(user);
        return true;
    }
}