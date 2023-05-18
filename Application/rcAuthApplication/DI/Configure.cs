using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rcAuthApplication.Service;
using rcAuthApplication.Interfaces;
using diDomain = rcAuthDomain.DI.Configure;
using diRepository = rcAuthRepository.DI.Configure;

namespace rcAuthApplication.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            diDomain.ConfigureServices(services, configuration);
            diRepository.ConfigureServices(services, configuration);
        }
    }
}
