using InterviewApp.Services;
using Microsoft.Extensions.Time.Testing;

namespace InterviewApp.Tests;

public class TimeGreetingServiceTests
{
    [Theory]
    [InlineData(0, "Good morning")]
    [InlineData(11, "Good morning")]
    [InlineData(12, "Good afternoon")]
    [InlineData(17, "Good afternoon")]
    [InlineData(18, "Good evening")]
    [InlineData(23, "Good evening")]
    public void GetGreetingByTime_ReturnsExpectedGreeting(int hour, string expected)
    {
        var clock = new FakeTimeProvider();
        clock.SetLocalTimeZone(TimeZoneInfo.Utc);
        clock.SetUtcNow(new DateTimeOffset(2026, 1, 1, hour, 0, 0, TimeSpan.Zero));

        var sut = new TimeGreetingService(clock);

        Assert.Equal(expected, sut.GetGreetingByTime());
    }
}