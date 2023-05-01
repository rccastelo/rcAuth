using Dapper;
using Microsoft.Extensions.Configuration;
using rcAuthRepository.Interfaces;
using System.Linq;

namespace rcAuthRepository
{
    public class Repository : IRepository
    {
        private IConfiguration _configuration;

        public Repository(IConfiguration configuration) 
        {
            this._configuration = configuration;
        }

        public Entity get<Entity>(string query)
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
