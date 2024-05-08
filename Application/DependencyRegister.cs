using Application.ApplicationServices.IServices;
using Application.ApplicationServices.Services;
using FluentValidation;
using Infrastructure.DataAccess.IRepositories;
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
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IStudentService, StudentService>();
        }
    }
}
