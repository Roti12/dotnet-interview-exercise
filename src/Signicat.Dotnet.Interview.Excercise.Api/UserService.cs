using Signicat.Dotnet.Interview.Excercise.Api.Models;

namespace Signicat.Dotnet.Interview.Excercise.Api;

public class UserService : IUserService
{
    //In memory database
    private readonly Dictionary<string, UserEntity> _users = [];
    
    public UserEntity CreateUser(string userId, string password)
    {
        if (!_users.ContainsKey(userId))
        {
            throw new Exception("User with the same ID already exists");
        }
        
        var createdUser = new UserEntity
        {
            UserId = userId,
            Password = password
        };
        
        _users.Add(createdUser.UserId, createdUser);

        return createdUser;
    }

    public UserEntity UpdatePassword(string userId, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public bool Authenticate(string userId, string password)
    {
        throw new NotImplementedException();
    }
    
    public List<UserEntity> ListUsers()
    {
        throw new NotImplementedException();
    }
}