using InterviewApp.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InterviewApp.Greeting
{
    public record GetTimeGreetingQuery : IRequest<string>;

    public class GetTimeGreetingQueryHandler(ITimeGreetingService timeGreetingService)
        : IRequestHandler<GetTimeGreetingQuery, string>
    {
        public Task<string> Handle(GetTimeGreetingQuery request, CancellationToken cancellationToken)
            => Task.FromResult(timeGreetingService.GetGreetingByTime());
    }
}