using Microsoft.Extensions.DependencyInjection;
using rcAuthRepository.Interfaces;

namespace rcAuthRepository.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Database, Database>();
            services.AddScoped<IRepository, Repository>();
        }
    }
}
