using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InterviewApp.Models;

namespace InterviewApp.Services
{
    public class GreetingService(
        IOptions<GreetingSettings> options,
        ILogger<GreetingService> logger) : IGreetingService
    {
        private const string FallbackLanguage = "English";

        private readonly GreetingSettings _settings = options.Value;

        public string GetGreeting()
        {
            logger.LogInformation(
                "Greeting service started (selected language: {Language})",
                _settings.SelectedLanguage);

            var greeting = Find(_settings.SelectedLanguage);

            if (greeting is null)
            {
                logger.LogWarning(
                    "Unsupported language '{Language}'. Falling back to {Fallback}. Supported: {Supported}",
                    _settings.SelectedLanguage,
                    FallbackLanguage,
                    string.Join(", ", _settings.Greetings.Select(g => g.Language)));

                greeting = Find(FallbackLanguage) ?? _settings.Greetings[0];
            }
            
            logger.LogInformation(
                "Displayed greeting in {Language}: {Message}",
                greeting.Language, greeting.Message);

            return greeting.Message;
        }

        private GreetingOptions? Find(string language) =>
            _settings.Greetings.FirstOrDefault(g =>
                string.Equals(g.Language, language, StringComparison.OrdinalIgnoreCase));
    }
}