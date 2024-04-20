using Microsoft.Extensions.DependencyInjection;

namespace ItMarathon.Service
{
    public static class Startup
    {
        public static IServiceCollection AddServicesScopedMappings(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}
