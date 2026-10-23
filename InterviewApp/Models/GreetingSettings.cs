using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Models
{
    public class GreetingSettings
    {
        public const string SectionName = "GreetingSettings";

        public string SelectedLanguage { get; set; } = string.Empty;
        public List<GreetingOptions> Greetings { get; set; } = new();
    }
}
