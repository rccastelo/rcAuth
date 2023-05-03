using Microsoft.Extensions.DependencyInjection;
using rcAuthApplication.Application;
using rcAuthApplication.Interfaces;
using diRepository = rcAuthRepository.DI.Configure;

namespace rcAuthApplication.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            diRepository.ConfigureServices(services);
        }
    }
}
