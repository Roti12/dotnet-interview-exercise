using Serilog;
using ILogger = Serilog.ILogger;

namespace Signicat.Dotnet.Interview.Excercise.Api.Initialization;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var logger = CreateLogger();
        
        logger.Information("Starting Signicat Interview Exercise API...");
        
        try
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();

            logger.Information("Signicat Interview Exercise API was shut down");
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "Signicat Interview Exercise API terminated unexpectedly");
        }
        finally
        {
            await Log.CloseAndFlushAsync();

            await Task.Delay(2000);
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog(Log.Logger)
            .ConfigureWebHostDefaults(wb => { wb.UseStartup<Startup>(); });
    
    private static ILogger CreateLogger()
    {
        var logger = Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        return logger;
    }
}