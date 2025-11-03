using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Signicat.Dotnet.Interview.Excercise.Api;
using Signicat.Dotnet.Interview.Excercise.Api.Models;
using Signicat.Dotnet.Interview.Exercise.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace Signicat.Dotnet.Interview.Exercise.IntegrationTests;

public class AdminControllerTests(ITestOutputHelper output) : TestBase(output)
{
    [Fact]
    public async Task ListUsers()
    {
        var factory = CreateApiFactory();

        var userService = factory.Services.GetRequiredService<IUserService>();
        
        //Seed 2 users
        for (var i = 0; i < 2; i++)
        {
            userService.CreateUser($"user-{i}", $"password-{i}");
        }
        
        var client = factory.CreateClient();
        
        var res = await client.GetAsync("/admin/users");
        
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        
        //Expect 2 users to be returned

        var users = await res.Content.ReadFromJsonAsync<List<User>>();

        Assert.NotNull(users);
        Assert.Equal(2, users!.Count);
        Assert.Equal("user-1", users[1].UserId);
        Assert.Null(users[1].PasswordLastUpdated);
        Assert.Equal("user-2", users[2].UserId);
        Assert.Null(users[2].PasswordLastUpdated);
    }
}