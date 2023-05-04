using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rcAuthRepository.Interfaces;
using rcAuthRepository.Repository;
using diData = rcAuthData.DI.Configure;

namespace rcAuthRepository.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            diData.ConfigureServices(services, configuration);
        }
    }
}
