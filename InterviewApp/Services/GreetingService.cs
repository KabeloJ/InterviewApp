using System;
using Microsoft.Extensions.Options;
using InterviewApp.Models;

namespace InterviewApp.Services
{
    public class GreetingService(IOptions<GreetingOptions> options) : IGreetingService
    {
        private readonly GreetingOptions _options = options.Value;

        public void Run()
        {
            Console.WriteLine("Ola");
            Console.WriteLine(_options.Message);
        }
    }
}