using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rcAuthApplication.Service;
using rcAuthApplication.Interfaces;
using diRepository = rcAuthRepository.DI.Configure;

namespace rcAuthApplication.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            diRepository.ConfigureServices(services, configuration);
        }
    }
}
