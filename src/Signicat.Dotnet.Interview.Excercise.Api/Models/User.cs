namespace Signicat.Dotnet.Interview.Excercise.Api.Models;

public class User
{
    public string? UserId { get; set; }
    public string? Password { get; set; }
    public DateTime? PasswordLastUpdated { get; set; }
}