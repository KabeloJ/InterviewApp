using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using InterviewApp.Services;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((_, services) => { services.AddTransient<IGreetingService, GreetingService>(); })
            .Build();

        var greetingService = host.Services.GetRequiredService<IGreetingService>();
        greetingService.Run();

        await host.RunAsync();
    }
}