using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Models
{
    public class GreetingSettingsValidator : IValidateOptions<GreetingSettings>
    {
        public ValidateOptionsResult Validate(string? name, GreetingSettings options)
        {
            var failures = new List<string>();

            if (string.IsNullOrWhiteSpace(options.SelectedLanguage))
                failures.Add("GreetingSettings:SelectedLanguage must not be empty.");

            if (options.Greetings.Count == 0)
                failures.Add("GreetingSettings:Greetings must contain at least one entry.");

            for (var i = 0; i < options.Greetings.Count; i++)
            {
                var entry = options.Greetings[i];
                var results = new List<ValidationResult>();

                if (!Validator.TryValidateObject(entry, new ValidationContext(entry), results, validateAllProperties: true))
                {
                    foreach (var result in results)
                        failures.Add($"Greetings[{i}]: {result.ErrorMessage}");
                }
            }

            return failures.Count > 0
                ? ValidateOptionsResult.Fail(failures)
                : ValidateOptionsResult.Success;
        }
    }
}
