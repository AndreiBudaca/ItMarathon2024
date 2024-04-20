using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ItMarathon.Data.Infrastructure;
using ItMarathon.Core;

namespace ItMarathon.Data
{
    public static class Startup
    {
        public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddDbContext<ItMarathonContext>(options =>
            {
                if (Debugger.IsAttached)
                {
                    options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(AppConfig.ConnectionString);
                }
                else
                {
                    options.UseSqlServer(AppConfig.ConnectionString);
                }
            });

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return serviceCollection;
        }
    }
}
