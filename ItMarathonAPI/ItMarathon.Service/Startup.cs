using ItMarathon.Data;
using ItMarathon.Service.Authentication;
using ItMarathon.Service.Courses;
using ItMarathon.Service.StudentGrades;
using ItMarathon.Service.StudentOptionalPreferences;
using ItMarathon.Service.StudentOptionals;
using Microsoft.Extensions.DependencyInjection;

namespace ItMarathon.Service
{
    public static class Startup
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDatabase();

            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<ICoursesService, CourseService>();
            serviceCollection.AddScoped<IStudentGradesService, StudentGradeService>();
            serviceCollection.AddScoped<IStudentOptionalPreferenceService, StudentOptionalPreferenceService>();
            serviceCollection.AddScoped<IStudentOptionalsService, StudentOptionalsService>();

            return serviceCollection;
        }
    }
}
