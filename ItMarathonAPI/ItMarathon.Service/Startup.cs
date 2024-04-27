using ItMarathon.Data;
using ItMarathon.Service.Authentication;
using ItMarathon.Service.Courses;
using ItMarathon.Service.HelloWorldService;
using Microsoft.Extensions.DependencyInjection;

namespace ItMarathon.Service
{
    public static class Startup
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDatabase();

            serviceCollection.AddScoped<IHelloWorldService, HelloWorldService.HelloWorldService>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<ICoursesService, CourseService>();

            return serviceCollection;
        }
    }
}
