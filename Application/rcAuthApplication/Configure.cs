using Microsoft.Extensions.DependencyInjection;
using diDatabase = rcAuthRepository.Configure;

namespace rcAuthApplication
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AuthService, AuthService>();
            services.AddScoped<AuthData, AuthData>();
            diDatabase.ConfigureServices(services);
        }
    }
}
