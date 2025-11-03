namespace Signicat.Dotnet.Interview.Excercise.Api.Db;

public class InMemoryDb
{
    private readonly Dictionary<string, UserDbEntity> _users = [];

    public IEnumerable<UserDbEntity> ListUsers()
    {
        return _users.Values;
    }

    public UserDbEntity? GetUser(string userId)
    {
        var userExists = _users.TryGetValue(userId, out var user);

        return userExists ? user : null;
    }
    
    public void AddUser(string userId, string password)
    {
        var user = new UserDbEntity
        {
            UserId = userId,
            Password = password
        };
        
        _users.Add(userId, user);
    }

    public void UpdatePassword(string userId, string password)
    {
        _users[userId] = new UserDbEntity
        {
            UserId = userId,
            Password = password
        };
    }

    public bool RemoveUser(string userId)
    {
        return _users.Remove(userId);
    }
}

public class UserDbEntity
{
    public required string UserId { get; set; }
    public required string Password { get; set; }
}