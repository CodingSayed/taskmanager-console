using ToDoList.Models;

namespace ToDoList.Services;

public class Session
{
    public User? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;
    public void Set(User u) => CurrentUser = u;
    public void Clear() => CurrentUser = null;
}