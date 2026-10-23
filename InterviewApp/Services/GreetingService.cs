using InterviewApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewApp.Services
{
    public class GreetingService(IOptions<GreetingSettings> options) : IGreetingService
    {
        private const string FallbackLanguage = "English";

        private readonly GreetingSettings _settings = options.Value;

        public void Run()
        {
            var greeting = Find(_settings.SelectedLanguage);

            if (greeting == null)
            {
                greeting = Find(FallbackLanguage);
            }

            if (greeting == null)
            {
                greeting = _settings.Greetings[0];
            }

            Console.WriteLine(greeting.Message);
        }

        private GreetingOptions? Find(string language) =>
            _settings.Greetings.FirstOrDefault(x =>
                string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase));
    }
}