using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rcAuthDomain.Business;
using rcAuthDomain.Interfaces;
using rcAuthDomain.Models;

namespace rcAuthDomain.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenModel, TokenModel>();
            services.AddScoped<IAuthBusiness, AuthBusiness>();
        }
    }
}
