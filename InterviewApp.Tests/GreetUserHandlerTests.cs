using InterviewApp.Greeting;
using InterviewApp.Services;
using MediatR;
using Moq;

namespace InterviewApp.Tests;

public class GreetUserHandlerTests
{
    [Fact]
    public async Task Handle_CombinesTimeGreetingWithConfiguredMessage()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetTimeGreetingQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("Good evening");

        var greetingService = new Mock<IGreetingService>();
        greetingService.Setup(g => g.GetGreeting()).Returns("Welcome!");

        var sut = new GreetUserHandler(mediator.Object, greetingService.Object);

        var result = await sut.Handle(new GreetUserCommand(), CancellationToken.None);

        Assert.Equal("Good evening, Welcome!", result);
    }
}