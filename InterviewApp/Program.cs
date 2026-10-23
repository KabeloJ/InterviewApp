using InterviewApp.Models;
using InterviewApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IValidateOptions<GreetingSettings>, GreetingSettingsValidator>();

                services.AddOptions<GreetingSettings>()
                        .Bind(context.Configuration.GetSection(GreetingSettings.SectionName))
                        .ValidateOnStart();

                services.AddTransient<IGreetingService, GreetingService>();
            })
            .Build();

        var logger = host.Services.GetRequiredService<ILogger<Program>>();

        try
        {
            await host.StartAsync();
        }
        catch (OptionsValidationException ex)
        {
            logger.LogError("Invalid configuration: {Failures}", string.Join("; ", ex.Failures));
            Environment.ExitCode = 1;
            return;
        }

        var greetingService = host.Services.GetRequiredService<IGreetingService>();
        greetingService.Run();

        await host.StopAsync();
    }
}