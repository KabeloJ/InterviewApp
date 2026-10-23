using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApp.Services
{
    public class TimeGreetingService : ITimeGreetingService
    {
        public class TimeGreetingService(TimeProvider timeProvider) : ITimeGreetingService
        {
            public string GetGreetingByTime()
            {
                var currentHour = timeProvider.GetLocalNow().Hour;

                if (currentHour < 12)
                {
                    return "Good morning";
                }

                if (currentHour < 18)
                {
                    return "Good afternoon";
                }

                return "Good evening";
            }
        }
    }
}
