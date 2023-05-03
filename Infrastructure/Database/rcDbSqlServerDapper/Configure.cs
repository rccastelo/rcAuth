using Microsoft.Extensions.DependencyInjection;

namespace rcDbSqlServerDapper
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Database, Database>();
            services.AddScoped<IData, Data>();
        }
    }
}
