using Infrastructure.Common.Utilities;
using Infrastructure.Data;
using Infrastructure.DataAccess.IRepositories;
using Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
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
