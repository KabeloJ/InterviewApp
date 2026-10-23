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

    public class GreetUserHandler(IMediator mediator, IGreetingService greetingService)
        : IRequestHandler<GreetUserCommand, string>
    {
        public async Task<string> Handle(GreetUserCommand request, CancellationToken cancellationToken)
        {
            var timeGreeting = await mediator.Send(new GetTimeGreetingQuery(), cancellationToken);
            var message = greetingService.GetGreeting();

            return $"{timeGreeting}, {message}";
        }
    }
}
