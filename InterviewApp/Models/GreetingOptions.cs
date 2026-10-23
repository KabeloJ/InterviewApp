using System.ComponentModel.DataAnnotations;

namespace InterviewApp.Models
{
    public class GreetingOptions
    {
        [Required(AllowEmptyStrings = false)]
        public string Message { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Language { get; set; } = string.Empty;
    }
}