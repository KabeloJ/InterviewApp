using InterviewApp.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Greeting
{
    public record GreetUserCommand : IRequest<string>;

    public class GreetUserHandler(IGreetingService greetingService)
        : IRequestHandler<GreetUserCommand, string>
    {
        public Task<string> Handle(GreetUserCommand request, CancellationToken cancellationToken)
            => Task.FromResult(greetingService.GetGreeting());
    }
}
