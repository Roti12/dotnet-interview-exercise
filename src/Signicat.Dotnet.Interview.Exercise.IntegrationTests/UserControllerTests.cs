using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Signicat.Dotnet.Interview.Excercise.Api;
using Signicat.Dotnet.Interview.Excercise.Api.Models;
using Signicat.Dotnet.Interview.Exercise.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace Signicat.Dotnet.Interview.Exercise.IntegrationTests;

public class UserControllerTests(ITestOutputHelper output) : TestBase(output)
{
    private const string UserId = "newTestUser";
    private const string Password = "my-secure-password";
    
    [Fact]
    public async Task CreateSameUserTwiceFails()
    {
        var factory = CreateApiFactory();

        var client = factory.CreateClient();

        var dto = new User
        {
            UserId = UserId,
            Password = Password
        };
        
        var response = await client.PostAsJsonAsync("/users", dto);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var user = await response.Content.ReadFromJsonAsync<User>();

        Assert.NotNull(user);
        Assert.Equal(dto.UserId, user!.UserId);
        Assert.Null(user.PasswordLastUpdated);
        
        var anotherCreateResponse = await client.PostAsJsonAsync("/users", dto);
        
        var content = await anotherCreateResponse.Content.ReadAsStringAsync();
        Assert.Contains("User with the same ID already exists", content);
    }
    
    [Fact]
    public async Task CreateUserSuccess()
    {
        var factory = CreateApiFactory();

        var client = factory.CreateClient();

        var dto = new User
        {
            UserId = UserId,
            Password = Password
        };
        
        var response = await client.PostAsJsonAsync("/users", dto);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var user = await response.Content.ReadFromJsonAsync<User>();

        Assert.NotNull(user);
        Assert.Equal(dto.UserId, user!.UserId);
        Assert.Null(user.PasswordLastUpdated);
    }
    
    [Fact]
    public async Task AuthenticateSuccess()
    {
        var factory = CreateApiFactory();

        SeedUser(factory, UserId, Password);
        
        var dto = new User
        {
            UserId = UserId,
            Password = Password
        };
        
        var client = factory.CreateClient();
        
        var authResponse = await client.PostAsJsonAsync("/users/authenticate", dto);
        Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
    }
    
    [Theory]
    [InlineData("myUser", "myP4SsWoRd!", "myUser1", "myP4SsWoRd!")]
    [InlineData("myUser", "myP4SsWoRd!", "myUser", "myP4SsWoRd!s")]
    public async Task AuthenticateFails(string existingUserId, string existingPassword, string attemptedUserId, string attemptedPassword)
    {
        var factory = CreateApiFactory();

        SeedUser(factory, existingUserId, existingPassword);
        
        var authDto = new User
        {
            UserId = attemptedUserId,
            Password = attemptedPassword
        };
        
        var client = factory.CreateClient();
        
        var authResponse = await client.PostAsJsonAsync("/users/authenticate", authDto);
        Assert.Equal(HttpStatusCode.Unauthorized, authResponse.StatusCode);
    }
    
    private void SeedUser(ApiFactory factory, string userId, string password)
    {
        var userService = factory.Services.GetRequiredService<IUserService>();

        var userEntity = userService.CreateUser(userId, password);
        
        Assert.Equal(userId, userEntity.UserId);
    }
}