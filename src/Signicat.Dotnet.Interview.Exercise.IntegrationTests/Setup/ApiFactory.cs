using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Signicat.Dotnet.Interview.Excercise.Api.Initialization;
using Xunit.Abstractions;

namespace Signicat.Dotnet.Interview.Exercise.IntegrationTests.Setup;

public class ApiFactory(ITestOutputHelper output) : WebApplicationFactory<Startup>
{
    public ITestOutputHelper Output => output;
    
    protected override IHostBuilder CreateHostBuilder()
    {
        var builder = base.CreateHostBuilder();

        if (builder == null)
        {
            throw new ArgumentException("HostBuilder is null");
        }

        return builder.ConfigureAppConfiguration(
            c =>
            {
                c.Sources.Clear();
                c.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["auth:adminApiUser"] = "test-user",
                    ["auth:adminApiPassword"] = "t3st-@passW0rd",
                    ["security:hmacSecret"] = "wkbUH+^3qJXIQWzSk9yt-cS<iBD1Y3,"
                });
            }
        ).UseSerilog(
            (_, logging) =>
            {
                logging.MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.TestOutput(output)
                    ;
            }
        );
    }
}