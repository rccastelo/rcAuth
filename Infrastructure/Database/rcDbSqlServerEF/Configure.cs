using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace rcDbSqlServerEF
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ManagerDbContext, ManagerDbContext>();
        }
    }
}
