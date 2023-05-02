using Microsoft.Extensions.DependencyInjection;
using rcAuthApplication.Application;
using rcAuthApplication.Interfaces;
using diDatabase = rcAuthRepository.DI.Configure;

namespace rcAuthApplication.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthData, AuthData>();
            diDatabase.ConfigureServices(services);
        }
    }
}
