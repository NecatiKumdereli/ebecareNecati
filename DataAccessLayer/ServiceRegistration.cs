using DataAccessLayer.EntityFramework.Abstracts;
using DataAccessLayer.EntityFramework.Concretes;
using DataAccessLayer.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDataAccessLayerServiceRegistration(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("SqlCon"));
            },ServiceLifetime.Scoped);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IModuleRoleRepository, ModuleRoleRepository>();

            return services;
        }
    }
}
