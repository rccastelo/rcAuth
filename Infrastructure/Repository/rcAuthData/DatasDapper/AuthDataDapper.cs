using Dapper;
using Microsoft.Extensions.Configuration;
using rcAuthData.Interfaces;
using rcAuthDomain.Entities;
using rcDbSqlServerDapper;
using System.Linq;

namespace rcAuthData.DatasDapper
{
    public class AuthDataDapper : DataDapper, IAuthData
    {
        public AuthDataDapper(IConfiguration configuration) : base(configuration)
        {
            
        }

        public AuthEntity Login(AuthEntity entity)
        {
            AuthEntity authRet;

            string command = "SELECT l.pk_id_login as id, l.login " +
                "FROM Login l " +
                $"WHERE l.login = '{entity.Login}' " +
                $"AND l.secret = '{entity.Secret}'";

            _database.Open();

            authRet = _conn.Query<AuthEntity>(command).FirstOrDefault();

            _database.Close();

            return authRet;
        }
    }
}
