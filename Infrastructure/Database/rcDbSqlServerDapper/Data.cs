using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace rcDbSqlServerDapper
{
    public class Data : IData
    {
        private IConfiguration _configuration;

        public Data(IConfiguration configuration) 
        {
            this._configuration = configuration;
        }

        public Entity Get<Entity>(string query)
        {
            Entity entityRet;

            using (Database db = new Database(_configuration))
            {
                entityRet = db._dbConnection.Query<Entity>(query).FirstOrDefault();
            }

            return entityRet;
        }
    }
}
