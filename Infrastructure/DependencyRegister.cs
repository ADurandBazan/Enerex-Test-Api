using Infrastructure.Common.Utilities;
using Infrastructure.Data;
using Infrastructure.DataAccess.IRepositories;
using Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DependencyRegister class for registering infrastructure services
    /// </summary>
    public static class DependencyRegister
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddScoped<IStudentRepository, StudentRepository>();
        }
    }
}
