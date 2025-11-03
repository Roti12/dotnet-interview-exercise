using Signicat.Dotnet.Interview.Excercise.Api.Middleware;

namespace Signicat.Dotnet.Interview.Excercise.Api.Initialization;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton<IUserService, UserService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseBasicAuthentication();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.Map("/", async context => await context.Response.WriteAsync("Welcome to this dotnet interview exercise"));
        });
    }
}