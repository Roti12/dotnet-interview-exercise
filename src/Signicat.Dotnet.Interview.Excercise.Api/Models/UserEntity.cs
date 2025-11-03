namespace Signicat.Dotnet.Interview.Excercise.Api.Models;

public class UserEntity
{
    public required string UserId { get; set; }
    public required string Password { get; set; }
    public DateTime? LastModified { get; set; }
}