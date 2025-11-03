using Signicat.Dotnet.Interview.Excercise.Api.Models;

namespace Signicat.Dotnet.Interview.Excercise.Api;

public interface IUserService
{
    UserEntity CreateUser(string userId, string password);
    UserEntity UpdatePassword(string userId, string oldPassword, string newPassword);
    bool Authenticate(string userId, string password);
    List<UserEntity> ListUsers();
}