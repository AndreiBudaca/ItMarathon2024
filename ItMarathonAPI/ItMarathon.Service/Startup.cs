using ItMarathon.Service.HelloWorldService;
using Microsoft.Extensions.DependencyInjection;

namespace ItMarathon.Service
{
    public static class Startup
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IHelloWorldService, HelloWorldService.HelloWorldService>();

            return serviceCollection;
        }
    }
}
