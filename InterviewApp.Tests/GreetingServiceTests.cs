using InterviewApp.Models;
using InterviewApp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Options;
using Moq;

namespace InterviewApp.Tests;

public class GreetingServiceTests
{
    private readonly Mock<ITimeGreetingService> _time = new();
    private readonly FakeLogger<GreetingService> _logger = new();

    public GreetingServiceTests()
    {
        _time.Setup(t => t.GetGreetingByTime()).Returns("Good morning");
    }

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

    private string RunAndCaptureOutput(string selectedLanguage)
    {
        var sut = new GreetingService(Options.Create(Settings(selectedLanguage)), _logger, _time.Object);

        var original = Console.Out;
        var writer = new StringWriter();
        Console.SetOut(writer);
        try { sut.Run(); }
        finally { Console.SetOut(original); }

        return writer.ToString().Trim();
    }

    [Theory]
    [InlineData("English", "Good morning, Welcome!")]
    [InlineData("Afrikaans", "Good morning, Welkom!")]
    [InlineData("zulu", "Good morning, Siyakwamukela!")] // case-insensitive
    public void Run_ShowsMessageForSelectedLanguage(string language, string expected)
    {
        Assert.Equal(expected, RunAndCaptureOutput(language));
    }

    [Fact]
    public void Run_UnsupportedLanguage_FallsBackToEnglishAndLogsWarning()
    {
        var output = RunAndCaptureOutput("French");

        Assert.Equal("Good morning, Welcome!", output);
        Assert.Contains(_logger.Collector.GetSnapshot(), r => r.Level == LogLevel.Warning);
    }

    [Fact]
    public void Run_UsesTimeGreetingService()
    {
        RunAndCaptureOutput("English");

        _time.Verify(t => t.GetGreetingByTime(), Times.Once);
    }
}