using Application.ApplicationServices.IServices;
using Application.ApplicationServices.Services;
using FluentValidation;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DependencyRegister class for registering application services
    /// </summary>
    public static class DependencyRegister
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IStudentService, StudentService>();
        }
    }
}
