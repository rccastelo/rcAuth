using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rcAuthData.DapperDatas;
using rcAuthData.EFDatas;
using rcAuthData.Interfaces;
using diDbSqlServerDapper = rcDbSqlServerDapper.Configure;
using diDbSqlServerEF = rcDbSqlServerEF.Configure;

namespace rcAuthData.DI
{
    public class Configure
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string dbType = configuration.GetValue<string>("ConnectionType");
 
            switch (dbType) {
                case "ef":
                    services.AddScoped<IAuthData, AuthDataEF>();
                    diDbSqlServerEF.ConfigureServices(services, configuration);
                    break;
                case "dapper":
                default:
                    services.AddScoped<IAuthData, AuthDataDapper>();
                    diDbSqlServerDapper.ConfigureServices(services, configuration);
                    break;
            }
        }
    }
}
