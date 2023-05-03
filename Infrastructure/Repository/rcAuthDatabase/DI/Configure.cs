using Microsoft.Extensions.DependencyInjection;
using rcAuthRepository.Interfaces;
using rcAuthRepository.Repository;
using diDatabase = rcDbSqlServerDapper.Configure;

namespace rcAuthRepository.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            diDatabase.ConfigureServices(services);
        }
    }
}
