using InterviewApp.Models;
using InterviewApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Options;

namespace InterviewApp.Tests;

public class GreetingServiceTests
{
    private readonly FakeLogger<GreetingService> _logger = new();

    private static GreetingSettings Settings(string selectedLanguage) => new()
    {
        SelectedLanguage = selectedLanguage,
        Greetings = new List<GreetingOptions>
        {
            new() { Language = "English", Message = "Welcome!" },
            new() { Language = "Afrikaans", Message = "Welkom!" },
            new() { Language = "Zulu", Message = "Siyakwamukela!" }
        }
    };

    private string GetGreeting(string selectedLanguage)
    {
        var sut = new GreetingService(Options.Create(Settings(selectedLanguage)), _logger);
        return sut.GetGreeting();
    }

    [Theory]
    [InlineData("English", "Welcome!")]
    [InlineData("Afrikaans", "Welkom!")]
    [InlineData("zulu", "Siyakwamukela!")] // case-insensitive
    public void GetGreeting_ReturnsMessageForSelectedLanguage(string language, string expected)
    {
        Assert.Equal(expected, GetGreeting(language));
    }

    [Fact]
    public void GetGreeting_UnsupportedLanguage_FallsBackToEnglishAndLogsWarning()
    {
        var result = GetGreeting("French");

        Assert.Equal("Welcome!", result);
        Assert.Contains(_logger.Collector.GetSnapshot(), r => r.Level == LogLevel.Warning);
    }
}